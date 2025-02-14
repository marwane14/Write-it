using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using TMPro;

public class WordGameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI wordDisplay;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI letterFeedback;
    [SerializeField] private TextMeshProUGUI scoreText; // ✅ Affichage du score
    [SerializeField] private Button validateButton;
    [SerializeField] private Color correctColor = Color.green;
    [SerializeField] private Color wrongColor = Color.red;
    [SerializeField] private Color defaultColor = Color.white;

    private Image timeBar;
    private string fullWord;
    private char[] currentWordState;
    private List<string> wordsList = new List<string>();
    private float timer = 150f;
    private bool gameActive = true;
    private int score = 0; // ✅ Système de score
    private List<char> guessedLetters = new List<char>();
    private float maxTimeBarWidth;

    private void Start()
    {
        // 🔥 Trouver automatiquement `TimeBar`
        timeBar = GameObject.Find("TimeBar")?.GetComponent<Image>();

        if (timeBar == null)
        {
            Debug.LogError("❌ TimeBar introuvable ! Vérifie que l'objet s'appelle bien `TimeBar` dans la scène.");
        }
        else
        {
            maxTimeBarWidth = timeBar.rectTransform.sizeDelta.x;
        }

        LoadWords();
        GenerateNewWord();
        validateButton.onClick.AddListener(ValidateWord);

        // ✅ Modifier la taille du texte dans `InputField`
        inputField.textComponent.fontSize = 60;
        inputField.textComponent.alignment = TextAlignmentOptions.Center;

        UpdateScoreDisplay();
    }

    private void Update()
    {
        if (gameActive)
        {
            timer -= Time.deltaTime * 5f;
            UpdateTimeBar();

            if (timer <= 0)
            {
                Debug.Log("💀 Temps écoulé, perdu !");
                GenerateNewWord();
            }
        }
    }

    void LoadWords()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "mots.txt");

        if (File.Exists(filePath))
        {
            wordsList = File.ReadAllLines(filePath).ToList();
            Debug.Log($"✅ {wordsList.Count} mots chargés.");
        }
        else
        {
            Debug.LogError($"❌ Fichier mots.txt non trouvé : {filePath}");
        }
    }

    void GenerateNewWord()
    {
        if (wordsList.Count == 0) return;

        fullWord = wordsList[Random.Range(0, wordsList.Count)].ToUpper();
        currentWordState = fullWord.ToCharArray();
        guessedLetters.Clear();

        for (int i = 0; i < currentWordState.Length; i++)
        {
            if (Random.value > 0.5f)
                currentWordState[i] = '_';
        }

        if (!currentWordState.Contains(fullWord[0]))
        {
            currentWordState[0] = fullWord[0];
        }

        Debug.Log($"🔠 **Mot à deviner : {fullWord}**");

        UpdateWordDisplay();
        UpdateLetterFeedback();
        timer = 150f;
        UpdateTimeBar();
        gameActive = true;

        inputField.text = "";
        inputField.onEndEdit.RemoveAllListeners();
        inputField.onEndEdit.AddListener(CheckLetter);

        inputField.ActivateInputField(); // ✅ Force le focus sur l'InputField au début du mot
    }

    void UpdateWordDisplay()
    {
        string displayedWord = "";
        for (int i = 0; i < currentWordState.Length; i++)
        {
            Color letterColor = (currentWordState[i] == '_') ? defaultColor : correctColor;
            displayedWord += $"<color=#{ColorUtility.ToHtmlStringRGB(letterColor)}>{currentWordState[i]}</color> ";
        }
        wordDisplay.text = displayedWord;
    }

    void UpdateTimeBar()
    {
        if (timeBar != null)
        {
            float percent = timer / 150f;
            float newWidth = maxTimeBarWidth * percent;
            timeBar.rectTransform.sizeDelta = new Vector2(newWidth, timeBar.rectTransform.sizeDelta.y);
            timeBar.color = Color.Lerp(Color.red, Color.green, percent);
        }
    }

    void CheckLetter(string letterInput)
    {
        if (!gameActive || letterInput.Length == 0) return;

        char letter = char.ToUpper(letterInput[0]);

        if (guessedLetters.Contains(letter)) return;
        guessedLetters.Add(letter);

        bool found = false;

        for (int i = 0; i < fullWord.Length; i++)
        {
            if (fullWord[i] == letter && currentWordState[i] == '_')
            {
                currentWordState[i] = letter;
                found = true;
            }
        }

        UpdateWordDisplay();
        UpdateLetterFeedback();

        if (found)
        {
            Debug.Log($"✅ Lettre correcte : {letter}");
        }
        else
        {
            score -= 1; // ✅ -1 point pour une mauvaise lettre
            Debug.Log($"❌ Mauvaise lettre : {letter} | Score : {score}");
        }

        if (!new string(currentWordState).Contains("_")) // ✅ Si le mot est complété
        {
            Debug.Log("🎉 **Mot complété !**");
            score += 5; // ✅ +5 points pour un mot complet
            UpdateScoreDisplay();
            gameActive = false;
            Invoke("GenerateNewWord", 2f);
        }

        UpdateScoreDisplay();

        inputField.text = "";  // ✅ Efface l'entrée précédente
        inputField.ActivateInputField();  // ✅ Garde le focus sur l'InputField
    }

    void UpdateLetterFeedback()
    {
        string feedback = "";
        foreach (char letter in guessedLetters)
        {
            bool isCorrect = fullWord.Contains(letter);
            Color letterColor = isCorrect ? correctColor : wrongColor;
            feedback += $"<color=#{ColorUtility.ToHtmlStringRGB(letterColor)}>{letter}</color> ";
        }
        letterFeedback.text = feedback;
    }

    void ValidateWord()
    {
        if (!gameActive) return;

        string enteredWord = inputField.text.Trim().ToUpper();

        if (enteredWord == fullWord)
        {
            Debug.Log("🎉 **Mot complété avec succès !**");
            score += 5; // ✅ +5 points pour un mot correct
            gameActive = false;
            UpdateScoreDisplay();
            Invoke("GenerateNewWord", 2f);
        }
        else
        {
            Debug.Log("❌ **Mot incorrect, réessayez !**");
            score -= 1; // ✅ -1 point pour une erreur
        }

        UpdateScoreDisplay();

        inputField.text = "";  // ✅ Efface l'entrée précédente
        inputField.ActivateInputField();  // ✅ Garde le focus sur l'InputField
    }

    void UpdateScoreDisplay()
    {
        scoreText.text = $"Score : {score}";
    }
}

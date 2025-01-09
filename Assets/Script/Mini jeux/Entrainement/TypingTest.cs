using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class TypingSpeedTestTMP : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject menuUI;         // Menu principal
    public GameObject typingTestUI;    // UI pour le test
    public GameObject resultatUI;      // UI pour afficher les résultats

    [Header("UI Elements")]
    public TMP_Text phraseText;        // Affiche le mot ou la phrase à taper
    public TMP_InputField userInput;  // Champ de saisie de l'utilisateur
    public TMP_Text mpmText;          // Affiche les mots par minute (MPM)
    public TMP_Text timerText;        // Affiche le temps restant

    [Header("Result Elements")]
    public TMP_Text finalMpmText;     // Affiche le MPM final
    public TMP_Text typedWordsText;   // Affiche les mots tapés
    public TMP_Text accuracyText;     // Affiche la précision
    public Button restartButton;      // Bouton pour recommencer le test

    [Header("Menu Buttons")]
    public Button syllabeButton;      // Bouton pour sélectionner le mode syllabe
    public Button motButton;          // Bouton pour sélectionner le mode mot
    public Button phraseButton;       // Bouton pour sélectionner le mode phrase
    public Button duration10sButton;  // Bouton pour sélectionner 10 secondes
    public Button duration20sButton;  // Bouton pour sélectionner 20 secondes
    public Button duration30sButton;  // Bouton pour sélectionner 30 secondes

    public float selectedDuration = 0f;  // Durée sélectionnée pour le test

    private List<string[]> syllablesList = new List<string[]>();  // Liste des syllabes
    private List<string> wordsList = new List<string>();          // Liste des mots
    private List<string> phrasesList = new List<string>();        // Liste des phrases

    private int wordsTypedCorrectly = 0;   // Nombre de mots tapés correctement
    private float startTime;                // Heure de départ du test
    private bool testActive = false;       // Statut du test (actif ou non)
    private string typedWordsList = "";    // Liste des mots tapés
    private int correctLettersCount = 0;   // Nombre de lettres correctes
    private int totalLettersTyped = 0;     // Nombre total de lettres tapées

    private string currentWord;            // Mot ou phrase actuel à taper
    private string currentTestType;        // Type de test actuel (syllabe, mot, phrase)

    void Start()
    {
        // Configure les boutons
        restartButton.onClick.AddListener(RestartTest);
        syllabeButton.onClick.AddListener(() => StartTestFromMenu("syllabe"));
        motButton.onClick.AddListener(() => StartTestFromMenu("mot"));
        phraseButton.onClick.AddListener(() => StartTestFromMenu("phrase"));

        // Configure les boutons de durée
        duration10sButton.onClick.AddListener(() => SelectDuration(10f));
        duration20sButton.onClick.AddListener(() => SelectDuration(20f));
        duration30sButton.onClick.AddListener(() => SelectDuration(30f));

        mpmText.text = "MPM : 0";
        timerText.text = "Temps : Non défini";

        ShowMenu(); // Affiche le menu au démarrage

        LoadSyllables(); // Charge les syllabes depuis le fichier
        LoadWords();     // Charge les mots depuis le fichier
        LoadPhrases();   // Charge les phrases depuis le fichier
    }

    // Charge les syllabes depuis le fichier "syllabes.txt"
    void LoadSyllables()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "syllabes.txt");
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                syllablesList.Add(line.Split(' '));
            }
        }
        else
        {
            Debug.LogError("Fichier syllabes.txt introuvable !");
        }
    }

    // Charge les mots depuis le fichier "mots.txt"
    void LoadWords()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "mots.txt");
        if (File.Exists(filePath))
        {
            wordsList = new List<string>(File.ReadAllLines(filePath));
        }
    }

    // Charge les phrases depuis le fichier "phrases.txt"
    void LoadPhrases()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "phrases.txt");
        if (File.Exists(filePath))
        {
            phrasesList = new List<string>(File.ReadAllLines(filePath));
        }
    }

    // Sélectionne la durée du test
    void SelectDuration(float duration)
    {
        selectedDuration = duration;
        timerText.text = $"Temps : {selectedDuration:F1} s";
    }

    // Démarre le test depuis le menu avec le type de test choisi (syllabe, mot, phrase)
    void StartTestFromMenu(string testType)
    {
        if (selectedDuration <= 0)
        {
            Debug.LogError("Durée non sélectionnée !");
            timerText.text = "Erreur : Durée non sélectionnée";
            return;
        }

        currentTestType = testType;
        ShowTestUI();  // Affiche l'UI du test
        StartTest(testType);  // Démarre le test
    }

    // Affiche l'UI du menu
    void ShowMenu()
    {
        menuUI.SetActive(true);
        typingTestUI.SetActive(false);
        resultatUI.SetActive(false);
    }

    // Affiche l'UI du test
    void ShowTestUI()
    {
        menuUI.SetActive(false);
        typingTestUI.SetActive(true);
        resultatUI.SetActive(false);
    }

    // Affiche l'UI des résultats
    void ShowResultUI()
    {
        menuUI.SetActive(false);
        typingTestUI.SetActive(false);
        resultatUI.SetActive(true);
    }

    // Démarre le test en fonction du type
    void StartTest(string testType)
    {
        userInput.text = "";
        mpmText.text = "MPM : 0";   // Désactive le MPM au début
        timerText.text = $"Temps : {selectedDuration:F1} s";
        wordsTypedCorrectly = 0;
        typedWordsList = "";
        correctLettersCount = 0;
        totalLettersTyped = 0;

        currentWord = GetNextWord(testType);  // Récupère le prochain mot
        DisplayWordProgress();  // Affiche le mot à taper

        startTime = Time.time;
        testActive = true;

        userInput.interactable = true;
        userInput.ActivateInputField();
    }

    // Met à jour l'état du test (temps, MPM, etc.)
    void Update()
    {
        if (testActive)
        {
            float elapsedTime = Time.time - startTime;
            float remainingTime = selectedDuration - elapsedTime;
            timerText.text = $"Temps : {remainingTime:F1} s";

            if (remainingTime <= 0)
            {
                EndTest();  // Arrête le test lorsque le temps est écoulé
                return;
            }

            string input = userInput.text;
            DisplayWordProgress(input);

            // Lorsque l'utilisateur a tapé un mot complet
            if (input.Length >= currentWord.Length)
            {
                typedWordsList += FormatTypedWord(input) + " ";
                totalLettersTyped += currentWord.Length;

                // Calcul du nombre de lettres correctes
                for (int i = 0; i < currentWord.Length; i++)
                {
                    if (i < input.Length && input[i] == currentWord[i])
                    {
                        correctLettersCount++;
                    }
                }

                userInput.text = "";  // Réinitialise le champ de saisie
                currentWord = GetNextWord(currentTestType);  // Récupère le prochain mot
                DisplayWordProgress();  // Met à jour l'affichage du mot
            }

            // Si on est en mode "mot", calcule et affiche le MPM
            if (currentTestType == "mot")
            {
                float wordsPerMinute = (wordsTypedCorrectly / elapsedTime) * 60;
                mpmText.text = $"MPM : {wordsPerMinute:F2}";
            }
        }
    }

    // Termine le test lorsque le temps est écoulé
    void EndTest()
    {
        testActive = false;
        userInput.interactable = false;

        float elapsedTime = Time.time - startTime;
        float wordsPerMinute = (wordsTypedCorrectly / elapsedTime) * 60;
        mpmText.text = $"MPM final : {wordsPerMinute:F2}";
        timerText.text = $"Temps : 0.0 s";

        finalMpmText.text = $"MPM : {wordsPerMinute:F2} en {selectedDuration} s";
        typedWordsText.text = $"Mots tapés :\n{typedWordsList}";

        // Calcul de la précision
        if (totalLettersTyped == 0)
        {
            accuracyText.text = "Précision : 0%";
        }
        else
        {
            float accuracy = (correctLettersCount / (float)totalLettersTyped) * 100;
            accuracyText.text = $"Précision : {accuracy:F2}%";
        }

        ShowResultUI();  // Affiche l'UI des résultats
    }

    // Redémarre le test après avoir afficher les résultats
    void RestartTest()
    {
        ShowMenu();  // Retourne au menu principal
    }

    // Récupère le prochain mot en fonction du type de test
    string GetNextWord(string testType)
    {
        switch (testType)
        {
            case "syllabe":
                int randomSyllableIndex = Random.Range(0, syllablesList.Count);
                return string.Join(" ", syllablesList[randomSyllableIndex]);
            case "mot":
                int randomWordIndex = Random.Range(0, wordsList.Count);
                return wordsList[randomWordIndex];
            case "phrase":
                int randomPhraseIndex = Random.Range(0, phrasesList.Count);
                return phrasesList[randomPhraseIndex];
            default:
                return "";
        }
    }

    // Affiche l'état actuel du mot à taper (syllabe, mot, ou phrase)
    void DisplayWordProgress(string input = "")
    {
        phraseText.text = FormatWordToDisplay(input);
    }

    // Formatte le mot ou la phrase à afficher (par exemple, ajoute les caractères à l'endroit où l'utilisateur est)
    string FormatWordToDisplay(string input = "")
    {
        string displayText = currentWord;

        // Colorie les lettres tapées en vert et les lettres incorrectes en rouge
        string result = "";
        for (int i = 0; i < displayText.Length; i++)
        {
            if (i < input.Length)
            {
                if (input[i] == displayText[i])
                {
                    result += $"<color=green>{displayText[i]}</color>";
                }
                else
                {
                    result += $"<color=red>{displayText[i]}</color>";
                }
            }
            else
            {
                result += displayText[i];
            }
        }

        return result;
    }

    // Formate le mot pour l'affichage dans la liste des mots tapés
    string FormatTypedWord(string word)
    {
        return word;
    }
}
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class TypingSpeedTestTMP : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject menuUI;         // Menu principal
    public GameObject typingTestUI;   // UI pour le test
    public GameObject resultatUI;     // UI pour afficher les résultats
    public Slider durationSlider;     // Slider pour sélectionner la durée
    public TMP_Text durationText;     // Affiche la durée sélectionnée

    [Header("Test Elements")]
    public TMP_Text phraseText;       // Affiche le mot à taper
    public TMP_Text timeRemainingText; // Affiche le temps restant
    public TMP_InputField userInput;  // Champ de saisie de l'utilisateur
    public TMP_Text mpmText;          // Affiche les mots par minute (MPM)

    [Header("Result Elements")]
    public TMP_Text finalMpmText;     // Affiche le MPM final
    public TMP_Text typedWordsText;   // Affiche les mots tapés
    public TMP_Text accuracyText;     // Affiche la précision
    public UnityEngine.UI.Button restartButton;  // Bouton pour recommencer le test

    [Header("Menu Buttons")]
    public UnityEngine.UI.Button motButton;      // Bouton pour sélectionner le mode mot

    public int selectedDuration = 30;  // Durée par défaut pour le test

    private List<string> wordsList = new List<string>(); // Liste des mots
    private int wordsTypedCorrectly = 0;   // Nombre de mots tapés correctement
    private float startTime;               // Heure de départ du test
    private bool testActive = false;       // Statut du test (actif ou non)
    private string typedWordsList = "";    // Liste des mots tapés
    private int correctLettersCount = 0;   // Nombre de lettres correctes
    private int totalLettersTyped = 0;     // Nombre total de lettres tapées
    private string currentWord;            // Mot actuel à taper

    void Start()
    {
        // Configure les boutons
        restartButton.onClick.AddListener(RestartTest);
        motButton.onClick.AddListener(StartTest);

        mpmText.text = "";  // Efface le texte MPM par défaut
        ShowMenu();         // Affiche le menu au démarrage

        LoadWords();        // Charge les mots depuis le fichier

        durationSlider.value = selectedDuration;
        durationSlider.onValueChanged.AddListener(UpdateDuration);
        UpdateDuration(selectedDuration); // Met à jour le texte au lancement
    }

    // Charge les mots depuis le fichier "mots.txt"
    void LoadWords()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "mots.txt");
        if (File.Exists(filePath))
        {
            wordsList = new List<string>(File.ReadAllLines(filePath));
        }
        else
        {
            Debug.LogError("Fichier mots.txt introuvable !");
        }
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

    // Démarre un test avec une limite de temps
    void StartTest()
    {
        ShowTestUI();
        userInput.text = "";
        mpmText.text = "MPM : 0";
        wordsTypedCorrectly = 0;
        typedWordsList = "";
        correctLettersCount = 0;
        totalLettersTyped = 0;

        currentWord = GetNextWord();
        DisplayWordProgress();

        startTime = Time.time;
        testActive = true;

        userInput.interactable = true;
        userInput.ActivateInputField();
    }

    // Récupère le prochain mot
    string GetNextWord()
    {
        int randomWordIndex = Random.Range(0, wordsList.Count);
        return wordsList[randomWordIndex];
    }

    // Met à jour l'état du test
    void Update()
    {
        if (testActive)
        {
            float elapsedTime = Time.time - startTime;
            float remainingTime = selectedDuration - elapsedTime;

            // Met à jour le temps restant
            timeRemainingText.text = $"Temps restant : {Mathf.CeilToInt(remainingTime)} s";

            if (remainingTime <= 0)
            {
                EndTest();
                return;
            }

            string input = userInput.text;
            DisplayWordProgress(input);

            if (input.Length >= currentWord.Length)
            {
                typedWordsList += input + " ";
                totalLettersTyped += currentWord.Length;

                for (int i = 0; i < currentWord.Length; i++)
                {
                    if (i < input.Length && input[i] == currentWord[i])
                    {
                        correctLettersCount++;
                    }
                }

                wordsTypedCorrectly++;
                userInput.text = "";
                currentWord = GetNextWord();
                DisplayWordProgress();
            }

            float wordsPerMinute = (wordsTypedCorrectly / elapsedTime) * 60;
            mpmText.text = $"MPM : {wordsPerMinute:F2}";
        }
    }

    // Termine le test
    void EndTest()
    {
        testActive = false;
        userInput.interactable = false;

        float elapsedTime = Time.time - startTime;
        float wordsPerMinute = (wordsTypedCorrectly / elapsedTime) * 60;

        finalMpmText.text = $"MPM : {wordsPerMinute:F2} en {selectedDuration} s";
        typedWordsText.text = $"Mots tapés :\n{typedWordsList}";

        ShowResultUI();
    }

    // Affiche l'UI des résultats
    void ShowResultUI()
    {
        menuUI.SetActive(false);
        typingTestUI.SetActive(false);
        resultatUI.SetActive(true);
    }

    // Redémarre le test
    void RestartTest()
    {
        ShowMenu();
    }

    // Met à jour le mot affiché
    void DisplayWordProgress(string input = "")
    {
        phraseText.text = currentWord;
    }

    // Met à jour la durée
    void UpdateDuration(float value)
    {
        selectedDuration = Mathf.RoundToInt(value);
        durationText.text = $"{selectedDuration} s";
    }
}

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
    public GameObject resultatUI;      // UI pour le résultat

    [Header("UI Elements")]
    public TMP_Text phraseText;
    public TMP_InputField userInput;
    public TMP_Text mpmText;
    public TMP_Text timerText;

    [Header("Result Elements")]
    public TMP_Text finalMpmText;
    public TMP_Text typedWordsText;
    public TMP_Text accuracyText;
    public Button restartButton;

    [Header("Menu Buttons")]
    public Button syllabeButton;
    public Button motButton;
    public Button phraseButton;

    [Header("Test Settings")]
    public float testDuration = 20f;

    private List<string[]> syllablesList = new List<string[]>();
    private List<string> wordsList = new List<string>();
    private List<string> phrasesList = new List<string>();

    private int wordsTypedCorrectly = 0;
    private float startTime;
    private bool testActive = false;
    private string typedWordsList = "";
    private int correctLettersCount = 0;
    private int totalLettersTyped = 0;

    private string currentWord;
    private string currentTestType;  // Nouveau : pour stocker le type de test (syllabe, mot, phrase)

    void Start()
    {
        restartButton.onClick.AddListener(RestartTest);

        // Configure les boutons du menu pour lancer le test avec le bon type
        syllabeButton.onClick.AddListener(() => StartTestFromMenu("syllabe"));
        motButton.onClick.AddListener(() => StartTestFromMenu("mot"));
        phraseButton.onClick.AddListener(() => StartTestFromMenu("phrase"));

        mpmText.text = "MPM : 0";
        timerText.text = $"Temps : {testDuration:F1} s";

        ShowMenu();  // Affiche l'UI du menu au démarrage

        LoadSyllables();
        LoadWords();
        LoadPhrases();
    }

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


    void LoadWords()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "mots.txt");
        if (File.Exists(filePath))
        {
            wordsList = new List<string>(File.ReadAllLines(filePath));
        }
    }

    void LoadPhrases()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "phrases.txt");
        if (File.Exists(filePath))
        {
            phrasesList = new List<string>(File.ReadAllLines(filePath));
        }
    }

    void StartTestFromMenu(string testType)
    {
        currentTestType = testType;
        ShowTestUI();
        StartTest(testType);
    }

    void ShowMenu()
    {
        menuUI.SetActive(true);
        typingTestUI.SetActive(false);
        resultatUI.SetActive(false);
    }

    void ShowTestUI()
    {
        menuUI.SetActive(false);
        typingTestUI.SetActive(true);
        resultatUI.SetActive(false);
    }

    void ShowResultUI()
    {
        menuUI.SetActive(false);
        typingTestUI.SetActive(false);
        resultatUI.SetActive(true);
    }

    void StartTest(string testType)
    {
        userInput.text = "";
        mpmText.text = "MPM : 0";
        timerText.text = $"Temps : {testDuration:F1} s";
        wordsTypedCorrectly = 0;
        typedWordsList = "";
        correctLettersCount = 0;
        totalLettersTyped = 0;

        if (testType == "syllabe")
        {
            currentWord = GetRandomSyllable();
        }
        else if (testType == "mot")
        {
            currentWord = GetRandomWord();
        }
        else if (testType == "phrase")
        {
            currentWord = GetRandomPhrase();
        }

        DisplayWordProgress();

        startTime = Time.time;
        testActive = true;

        userInput.interactable = true;
        userInput.ActivateInputField();
    }

    void Update()
    {
        if (testActive)
        {
            float elapsedTime = Time.time - startTime;
            float remainingTime = testDuration - elapsedTime;
            timerText.text = $"Temps : {remainingTime:F1} s";

            if (remainingTime <= 0)
            {
                EndTest();
                return;
            }

            string input = userInput.text;
            DisplayWordProgress(input);

            if (input.Length >= currentWord.Length)
            {
                typedWordsList += FormatTypedWord(input) + " ";
                wordsTypedCorrectly++;
                totalLettersTyped += currentWord.Length;

                for (int i = 0; i < currentWord.Length; i++)
                {
                    if (i < input.Length && input[i] == currentWord[i])
                    {
                        correctLettersCount++;
                    }
                }

                userInput.text = "";
                if (currentTestType == "syllabe")
                    currentWord = GetRandomSyllable();
                else if (currentTestType == "mot")
                    currentWord = GetRandomWord();
                else if (currentTestType == "phrase")
                    currentWord = GetRandomPhrase();

                DisplayWordProgress();
            }

            float wordsPerMinute = (wordsTypedCorrectly / elapsedTime) * 60;
            mpmText.text = $"MPM : {wordsPerMinute:F2}";
        }
    }

    void EndTest()
    {
        float finalTime = Time.time - startTime;
        float wordsPerMinute = (wordsTypedCorrectly / finalTime) * 60;
        mpmText.text = $"MPM final : {wordsPerMinute:F2}";
        timerText.text = $"Temps : 0.0 s";

        testActive = false;
        userInput.interactable = false;

        finalMpmText.text = $"MPM : {wordsPerMinute:F2} en {testDuration} s";
        typedWordsText.text = $"Mots tapés :\n{typedWordsList}";

        float accuracy = (float)correctLettersCount / totalLettersTyped * 100;
        accuracyText.text = $"Précision : {accuracy:F2}%";

        ShowResultUI();
    }
    // Fonction pour afficher le progrès de l'utilisateur sur le mot actuel
    void DisplayWordProgress(string input = "")
    {
        string displayText = "";
        for (int i = 0; i < currentWord.Length; i++)
        {
            if (i < input.Length)
            {
                if (input[i] == currentWord[i])
                    displayText += $"<color=green>{input[i]}</color>";
                else
                    displayText += $"<color=red>{input[i]}</color>";
            }
            else
            {
                displayText += currentWord[i];
            }
        }
        phraseText.text = displayText;
    }

    // Fonction pour formater le mot tapé avec des couleurs
    string FormatTypedWord(string input)
    {
        string formattedWord = "";
        for (int i = 0; i < currentWord.Length; i++)
        {
            if (i < input.Length)
            {
                if (input[i] == currentWord[i])
                    formattedWord += $"<color=green>{input[i]}</color>";
                else
                    formattedWord += $"<color=red>{input[i]}</color>";
            }
            else
            {
                formattedWord += currentWord[i];
            }
        }
        return formattedWord;
    }

    public void RestartTest()
    {
        ShowMenu();
    }

    private string GetRandomSyllable() => syllablesList[Random.Range(0, syllablesList.Count)][Random.Range(0, syllablesList[0].Length)];
    private string GetRandomWord() => wordsList[Random.Range(0, wordsList.Count)];
    private string GetRandomPhrase() => phrasesList[Random.Range(0, phrasesList.Count)];
}
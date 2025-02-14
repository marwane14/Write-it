using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class TypingSpeedTestTMP : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject menuUI;
    public GameObject typingTestUI;
    public GameObject resultatUI;

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
    public Button motButton;

    [Header("Duration Slider")]
    public Slider timeSlider;
    public TMP_Text timeText;
    private readonly float[] durations = { 10f, 20f, 30f, 40f, 50f };
    private float selectedDuration = 10f;

    private List<string> wordsList = new List<string>();

    private int wordsTypedCorrectly = 0;
    private float startTime;
    private bool testActive = false;
    private string typedWordsList = "";
    private int correctLettersCount = 0;
    private int totalLettersTyped = 0;

    private string currentWord;

    void Start()
    {
        restartButton.onClick.AddListener(RestartTest);
        motButton.onClick.AddListener(StartTestFromMenu);
        LoadWords();

        if (timeSlider != null)
        {
            timeSlider.value = 0f;
            UpdateTimerDisplay(timeSlider.value);
            timeSlider.onValueChanged.AddListener(UpdateTimerDisplay);
        }
    }

    private void UpdateTimerDisplay(float sliderValue)
    {
        int index = Mathf.RoundToInt(sliderValue * 4f);
        index = Mathf.Clamp(index, 0, 4);
        selectedDuration = durations[index];
        timerText.text = $"Temps : {selectedDuration} s";
    }

    private void LoadWords()
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

    // Démarre le test uniquement en mode "mot"
    private void StartTestFromMenu()
    {
        if (selectedDuration <= 0)
        {
            Debug.LogError("Durée non sélectionnée !");
            timerText.text = "Erreur : Durée non sélectionnée";
            return;
        }

        ShowTestUI();
        StartTest();
    }

    private void ShowMenu()
    {
        menuUI.SetActive(true);
        typingTestUI.SetActive(false);
        resultatUI.SetActive(false);
    }

    private void ShowTestUI()
    {
        menuUI.SetActive(false);
        typingTestUI.SetActive(true);
        resultatUI.SetActive(false);
    }

    private void ShowResultUI()
    {
        menuUI.SetActive(false);
        typingTestUI.SetActive(false);
        resultatUI.SetActive(true);
    }

    // Démarre le test avec la durée sélectionnée
    private void StartTest()
    {
        userInput.text = "";
        mpmText.text = "MPM : 0";
        timerText.text = $"Temps : {selectedDuration:F1} s";
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

    private void Update()
    {
        if (testActive)
        {
            float elapsedTime = Time.time - startTime;
            float remainingTime = selectedDuration - elapsedTime;
            timerText.text = $"Temps : {remainingTime:F1} s";

            if (remainingTime <= 0)
            {
                EndTest();
                return;
            }

            string input = userInput.text;
            DisplayWordProgress(input);

            // Si l'utilisateur tape le mot en entier
            if (input.Length >= currentWord.Length)
            {
                typedWordsList += FormatTypedWord(input) + " ";
                totalLettersTyped += currentWord.Length;

                // Calcul de la précision
                for (int i = 0; i < currentWord.Length; i++)
                {
                    if (i < input.Length && input[i] == currentWord[i])
                    {
                        correctLettersCount++;
                    }
                }

                userInput.text = "";
                currentWord = GetNextWord();
                DisplayWordProgress();
            }

            // Calcul du MPM en temps réel
            float wordsPerMinute = (wordsTypedCorrectly / elapsedTime) * 60;
            mpmText.text = $"MPM : {wordsPerMinute:F2}";
        }
    }

    // Fin du test
    private void EndTest()
    {
        testActive = false;
        userInput.interactable = false;

        float elapsedTime = Time.time - startTime;
        float wordsPerMinute = (wordsTypedCorrectly / elapsedTime) * 60;
        mpmText.text = $"MPM final : {wordsPerMinute:F2}";
        timerText.text = $"Temps : 0.0 s";

        finalMpmText.text = $"MPM : {wordsPerMinute:F2} en {selectedDuration} s";
        typedWordsText.text = $"Mots tapés :\n{typedWordsList}";

        float accuracy = (totalLettersTyped == 0) ? 0 : (correctLettersCount / (float)totalLettersTyped) * 100;
        accuracyText.text = $"Précision : {accuracy:F2}%";

        ShowResultUI();
    }

    private void RestartTest()
    {
        ShowMenu();
    }

    private string GetNextWord()
    {
        if (wordsList.Count > 0)
        {
            return wordsList[Random.Range(0, wordsList.Count)];
        }
        return "";
    }

    private void DisplayWordProgress(string input = "")
    {
        phraseText.text = FormatWordToDisplay(input);
    }

    private string FormatWordToDisplay(string input = "")
    {
        string displayText = currentWord;
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

    private string FormatTypedWord(string word)
    {
        return word;
    }
}

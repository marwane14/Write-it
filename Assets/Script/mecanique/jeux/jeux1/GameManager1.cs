using UnityEngine;
using TMPro;  // Nécessaire pour manipuler du texte TextMeshPro
using UnityEngine.UI; // Nécessaire pour manipuler les boutons

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI sentenceText;
    public Button[] choiceButtons; // Tableau pour stocker vos boutons de choix

    // Phrase à compléter et options
    private string incompleteSentence = "J’ai un … magnifique chez moi.";
    private string correctAnswer = "château";
    private string[] possibleAnswers = { "cheval", "château", "cahier" };

    void Start()
    {
        // Configure l’interface de départ
        instructionText.text = "Choisis le mot qui complète la phrase correctement.";
        sentenceText.text = incompleteSentence;

        // Assigne le texte de chaque bouton
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            // Vérifie qu'on a bien des textes correspondants
            TextMeshProUGUI btnText = choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (btnText != null && i < possibleAnswers.Length)
            {
                btnText.text = possibleAnswers[i];
            }

            // Inscrit la méthode OnChoiceSelected au clic du bouton
            int index = i; // variable locale pour éviter les problèmes de closure
            choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(index));
        }
    }

    // Méthode appelée quand on clique sur un bouton
    private void OnChoiceSelected(int choiceIndex)
    {
        // Récupère la réponse choisie
        string chosenWord = possibleAnswers[choiceIndex];

        // Vérifie si la réponse est correcte
        if (chosenWord == correctAnswer)
        {
            instructionText.text = "Bravo ! La réponse est correcte.";
        }
        else
        {
            instructionText.text = "Essaie encore ! Ce n’est pas la bonne réponse.";
        }
    }
}

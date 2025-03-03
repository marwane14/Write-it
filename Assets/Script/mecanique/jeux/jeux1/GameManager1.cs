using UnityEngine;
using TMPro;  // N�cessaire pour manipuler du texte TextMeshPro
using UnityEngine.UI; // N�cessaire pour manipuler les boutons

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI sentenceText;
    public Button[] choiceButtons; // Tableau pour stocker vos boutons de choix

    // Phrase � compl�ter et options
    private string incompleteSentence = "J�ai un � magnifique chez moi.";
    private string correctAnswer = "ch�teau";
    private string[] possibleAnswers = { "cheval", "ch�teau", "cahier" };

    void Start()
    {
        // Configure l�interface de d�part
        instructionText.text = "Choisis le mot qui compl�te la phrase correctement.";
        sentenceText.text = incompleteSentence;

        // Assigne le texte de chaque bouton
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            // V�rifie qu'on a bien des textes correspondants
            TextMeshProUGUI btnText = choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (btnText != null && i < possibleAnswers.Length)
            {
                btnText.text = possibleAnswers[i];
            }

            // Inscrit la m�thode OnChoiceSelected au clic du bouton
            int index = i; // variable locale pour �viter les probl�mes de closure
            choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(index));
        }
    }

    // M�thode appel�e quand on clique sur un bouton
    private void OnChoiceSelected(int choiceIndex)
    {
        // R�cup�re la r�ponse choisie
        string chosenWord = possibleAnswers[choiceIndex];

        // V�rifie si la r�ponse est correcte
        if (chosenWord == correctAnswer)
        {
            instructionText.text = "Bravo ! La r�ponse est correcte.";
        }
        else
        {
            instructionText.text = "Essaie encore ! Ce n�est pas la bonne r�ponse.";
        }
    }
}

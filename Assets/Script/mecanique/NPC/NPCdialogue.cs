using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [Header("UI pour Dialogue")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Texte du PNJ")]
    [TextArea(3, 10)]
    [SerializeField] private string npcDialogue;

    private void Start()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // On vérifie juste que c'est le joueur
        if (other.CompareTag("Player"))
        {
            ShowDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Quand le joueur sort de la zone, on ferme
        if (other.CompareTag("Player"))
        {
            HideDialogue();
        }
    }

    private void ShowDialogue()
    {
        if (dialoguePanel != null && dialogueText != null)
        {
            dialogueText.text = npcDialogue;
            dialoguePanel.SetActive(true);
        }
    }

    private void HideDialogue()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }
}

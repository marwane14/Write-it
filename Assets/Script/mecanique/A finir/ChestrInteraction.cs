using UnityEngine;
using UnityEngine.UI;

public class ChestInteraction : MonoBehaviour
{
    public Transform player; // Référence au joueur
    public GameObject pressEMessage; // UI "Appuyez sur E"
    public GameObject dialoguePanel; // Fenêtre de dialogue
    public float interactionDistance = 2.5f; // Distance d'interaction

    private bool isPlayerNearby = false;
    private bool hasInteracted = false; // Empêche l'interaction après la première fois

    private void Start()
    {
        if (!player)
        {
            Debug.LogError("[ERROR] Aucun joueur assigné !");
        }

        if (pressEMessage)
        {
            pressEMessage.SetActive(false); // Masquer "Appuyez sur E"
            Debug.Log("[START] Message 'Appuyez sur E' masqué.");
        }
        else
        {
            Debug.LogError("[ERROR] Aucun `pressEMessage` assigné !");
        }

        if (dialoguePanel)
        {
            dialoguePanel.SetActive(false); // Masquer la fenêtre de dialogue
            Debug.Log("[START] Fenêtre de dialogue masquée.");
        }
        else
        {
            Debug.LogError("[ERROR] Aucun `dialoguePanel` assigné !");
        }
    }

    private void Update()
    {
        if (!player || hasInteracted) return; // Stopper toute interaction après la première fois

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= interactionDistance && !isPlayerNearby)
        {
            isPlayerNearby = true;
            ShowPressEMessage(true);
        }
        else if (distance > interactionDistance && isPlayerNearby)
        {
            isPlayerNearby = false;
            ShowPressEMessage(false);
        }

        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("[INPUT] Touche E pressée - Interaction validée.");
            Interact();
        }
    }

    private void ShowPressEMessage(bool show)
    {
        if (pressEMessage && !hasInteracted) // Ne montre pas le message si l'interaction est déjà faite
        {
            pressEMessage.SetActive(show);
            Debug.Log(show ? "[UI] Affichage du message 'Appuyez sur E'." : "[UI] Message 'Appuyez sur E' caché.");
        }
    }

    private void Interact()
    {
        if (!dialoguePanel) return;

        hasInteracted = true; // Désactive toute interaction future
        dialoguePanel.SetActive(true);
        ShowPressEMessage(false);

        Debug.Log("[DIALOGUE] Fenêtre affichée - Interaction bloquée après ça.");

        // Supprimer le message "Appuyez sur E" définitivement
        if (pressEMessage)
        {
            pressEMessage.SetActive(false);
            Debug.Log("[UI] Message 'Appuyez sur E' supprimé définitivement.");
        }

        // Fermer la fenêtre après 3 secondes et désactiver définitivement le script
        Invoke("CloseDialogue", 3f);
    }

    private void CloseDialogue()
    {
        if (dialoguePanel)
        {
            dialoguePanel.SetActive(false);
            Debug.Log("[DIALOGUE] Fenêtre de dialogue fermée définitivement.");
        }

        // Désactive ce script définitivement
        this.enabled = false;
    }
}

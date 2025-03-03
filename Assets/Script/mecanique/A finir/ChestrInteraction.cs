using UnityEngine;
using UnityEngine.UI;

public class ChestInteraction : MonoBehaviour
{
    public Transform player; // R�f�rence au joueur
    public GameObject pressEMessage; // UI "Appuyez sur E"
    public GameObject dialoguePanel; // Fen�tre de dialogue
    public float interactionDistance = 2.5f; // Distance d'interaction

    private bool isPlayerNearby = false;
    private bool hasInteracted = false; // Emp�che l'interaction apr�s la premi�re fois

    private void Start()
    {
        if (!player)
        {
            Debug.LogError("[ERROR] Aucun joueur assign� !");
        }

        if (pressEMessage)
        {
            pressEMessage.SetActive(false); // Masquer "Appuyez sur E"
            Debug.Log("[START] Message 'Appuyez sur E' masqu�.");
        }
        else
        {
            Debug.LogError("[ERROR] Aucun `pressEMessage` assign� !");
        }

        if (dialoguePanel)
        {
            dialoguePanel.SetActive(false); // Masquer la fen�tre de dialogue
            Debug.Log("[START] Fen�tre de dialogue masqu�e.");
        }
        else
        {
            Debug.LogError("[ERROR] Aucun `dialoguePanel` assign� !");
        }
    }

    private void Update()
    {
        if (!player || hasInteracted) return; // Stopper toute interaction apr�s la premi�re fois

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
            Debug.Log("[INPUT] Touche E press�e - Interaction valid�e.");
            Interact();
        }
    }

    private void ShowPressEMessage(bool show)
    {
        if (pressEMessage && !hasInteracted) // Ne montre pas le message si l'interaction est d�j� faite
        {
            pressEMessage.SetActive(show);
            Debug.Log(show ? "[UI] Affichage du message 'Appuyez sur E'." : "[UI] Message 'Appuyez sur E' cach�.");
        }
    }

    private void Interact()
    {
        if (!dialoguePanel) return;

        hasInteracted = true; // D�sactive toute interaction future
        dialoguePanel.SetActive(true);
        ShowPressEMessage(false);

        Debug.Log("[DIALOGUE] Fen�tre affich�e - Interaction bloqu�e apr�s �a.");

        // Supprimer le message "Appuyez sur E" d�finitivement
        if (pressEMessage)
        {
            pressEMessage.SetActive(false);
            Debug.Log("[UI] Message 'Appuyez sur E' supprim� d�finitivement.");
        }

        // Fermer la fen�tre apr�s 3 secondes et d�sactiver d�finitivement le script
        Invoke("CloseDialogue", 3f);
    }

    private void CloseDialogue()
    {
        if (dialoguePanel)
        {
            dialoguePanel.SetActive(false);
            Debug.Log("[DIALOGUE] Fen�tre de dialogue ferm�e d�finitivement.");
        }

        // D�sactive ce script d�finitivement
        this.enabled = false;
    }
}

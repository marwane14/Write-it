using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerMinijeu : MonoBehaviour
{
    // Référence à un collider spécifique (par exemple, le collider du minijeu)
    [SerializeField] private Collider minijeuCollider;

    private bool isPlayerInTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie que c'est bien le collider qui nous intéresse qui a été activé.
        // Par exemple, on peut comparer si ce collider correspond à celui attendu :
        if (other == minijeuCollider && other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            Debug.Log("Le joueur est dans la zone de trigger du minijeu. Appuyez sur E pour lancer le minijeu.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == minijeuCollider && other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            Debug.Log("Le joueur a quitté la zone de trigger du minijeu.");
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Touche E appuyée, chargement de la scène Minijeu...");
            SceneManager.LoadScene("Minijeu");
        }
    }
}

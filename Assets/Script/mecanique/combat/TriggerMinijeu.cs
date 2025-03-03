using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerMinijeu : MonoBehaviour
{
    // R�f�rence � un collider sp�cifique (par exemple, le collider du minijeu)
    [SerializeField] private Collider minijeuCollider;

    private bool isPlayerInTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        // V�rifie que c'est bien le collider qui nous int�resse qui a �t� activ�.
        // Par exemple, on peut comparer si ce collider correspond � celui attendu :
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
            Debug.Log("Le joueur a quitt� la zone de trigger du minijeu.");
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Touche E appuy�e, chargement de la sc�ne Minijeu...");
            SceneManager.LoadScene("Minijeu");
        }
    }
}

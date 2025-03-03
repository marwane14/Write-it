using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Assurez-vous que cette ligne est pr�sente

public class StairsTransition : MonoBehaviour
{
    [Header("Transition Settings")]
    public string sceneToLoad = "Salon"; // Nom de la sc�ne � charger
    public Animator transitionAnimator; // Animator pour l'effet de transition
    public float transitionDuration = 1f; // Temps de l'animation en secondes

    private void OnTriggerEnter2D(Collider2D other)
    {
        // V�rifie si l'objet entrant est le joueur
        if (other.CompareTag("Player"))
        {
            // D�marrer la transition via une coroutine
            StartCoroutine(TransitionToScene());
        }
    }

    private IEnumerator TransitionToScene()
    {
        // Lancer l'animation de transition si un Animator est assign�
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("Start");
        }

        // Attendre la fin de l'animation (ou un d�lai d�fini)
        yield return new WaitForSeconds(transitionDuration);

        // Charger la nouvelle sc�ne
        SceneManager.LoadScene(sceneToLoad);
    }
}

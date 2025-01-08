using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Assurez-vous que cette ligne est présente

public class StairsTransition : MonoBehaviour
{
    [Header("Transition Settings")]
    public string sceneToLoad = "Salon"; // Nom de la scène à charger
    public Animator transitionAnimator; // Animator pour l'effet de transition
    public float transitionDuration = 1f; // Temps de l'animation en secondes

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifie si l'objet entrant est le joueur
        if (other.CompareTag("Player"))
        {
            // Démarrer la transition via une coroutine
            StartCoroutine(TransitionToScene());
        }
    }

    private IEnumerator TransitionToScene()
    {
        // Lancer l'animation de transition si un Animator est assigné
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("Start");
        }

        // Attendre la fin de l'animation (ou un délai défini)
        yield return new WaitForSeconds(transitionDuration);

        // Charger la nouvelle scène
        SceneManager.LoadScene(sceneToLoad);
    }
}

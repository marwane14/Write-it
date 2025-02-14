using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameObjectTransitionManager : MonoBehaviour
{
    public Image fadeImage; // Image noire pour le fondu
    public float fadeDuration = 1f; // Durée du fondu en secondes
    public GameObject targetObject; // GameObject à activer après la transition

    void Start()
    {
        if (fadeImage != null)
        {
            StartCoroutine(FadeIn()); // Commence avec un fondu d'entrée
        }
    }

    // Méthode pour lancer la transition et activer un GameObject
    public void TransitionToGameObject()
    {
        if (fadeImage != null && targetObject != null)
        {
            StartCoroutine(FadeOutAndActivateObject());
        }
    }

    // Fondu d'entrée (image noire disparaît progressivement)
    private IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        Color color = fadeImage.color;
        float t = fadeDuration;

        while (t > 0)
        {
            t -= Time.deltaTime;
            color.a = t / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0;
        fadeImage.color = color;
        fadeImage.gameObject.SetActive(false); // Désactive l'image après le fondu
    }

    // Fondu de sortie (image noire apparaît) puis activation du GameObject
    private IEnumerator FadeOutAndActivateObject()
    {
        fadeImage.gameObject.SetActive(true);
        Color color = fadeImage.color;
        float t = 0;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = t / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1;
        fadeImage.color = color;

        // Une fois le fondu terminé, active le GameObject
        targetObject.SetActive(true);

        // Optionnel : Masquer l'image noire après la transition
        StartCoroutine(FadeIn());
    }
}

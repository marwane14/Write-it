using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove : MonoBehaviour
{
    public string targetScene; // Nom de la scène cible
    public string spawnPoint; // Identifiant du point de spawn dans la scène cible
    public float fadeDuration = 1f; // Durée du fondu

    private Texture2D fadeTexture; // Texture pour l'effet de fondu
    private float fadeAlpha = 0f; // Niveau de transparence
    private bool isFading = false; // Indicateur de transition en cours

    private void Awake()
    {
        // Crée une texture noire d'une taille minimale
        fadeTexture = new Texture2D(1, 1);
        fadeTexture.SetPixel(0, 0, Color.black);
        fadeTexture.Apply();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFading)
        {
            StartCoroutine(LoadSceneWithFade(targetScene, spawnPoint));
        }
    }

    private IEnumerator LoadSceneWithFade(string sceneName, string spawnPointName)
    {
        // Mémorise la scène actuelle et le point de spawn
        PlayerPrefs.SetString("SpawnPoint", spawnPointName);

        // Démarre l'effet de fondu (écran devient noir)
        yield return StartCoroutine(Fade(1f));

        // Charge la nouvelle scène
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

        // Réduit l'effet de fondu après le chargement
        yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        isFading = true;

        while (!Mathf.Approximately(fadeAlpha, targetAlpha))
        {
            fadeAlpha = Mathf.MoveTowards(fadeAlpha, targetAlpha, Time.deltaTime / fadeDuration);
            yield return null;
        }

        isFading = false;
    }

    private void OnGUI()
    {
        if (isFading || fadeAlpha > 0)
        {
            // Applique la texture noire avec transparence
            Color color = GUI.color;
            color.a = fadeAlpha;
            GUI.color = color;

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
        }
    }
}

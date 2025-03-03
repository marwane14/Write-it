using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove : MonoBehaviour
{
    public string targetScene; // Nom de la sc�ne cible
    public string spawnPoint; // Identifiant du point de spawn dans la sc�ne cible
    public float fadeDuration = 1f; // Dur�e du fondu

    private Texture2D fadeTexture; // Texture pour l'effet de fondu
    private float fadeAlpha = 0f; // Niveau de transparence
    private bool isFading = false; // Indicateur de transition en cours

    private void Awake()
    {
        // Cr�e une texture noire d'une taille minimale
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
        // M�morise la sc�ne actuelle et le point de spawn
        PlayerPrefs.SetString("SpawnPoint", spawnPointName);

        // D�marre l'effet de fondu (�cran devient noir)
        yield return StartCoroutine(Fade(1f));

        // Charge la nouvelle sc�ne
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

        // R�duit l'effet de fondu apr�s le chargement
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

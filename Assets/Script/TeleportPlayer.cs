using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Transition de Scène")]
    public int targetSceneIndex; // Index de la scène cible
    public float fadeDuration = 1f; // Durée du fondu

    [Header("Points de Spawn")]
    public Transform spawnPoint1; // Premier point de spawn
    public Transform spawnPoint2; // Deuxième point de spawn

    [Header("Téléportation")]
    public Transform targetSpawnPoint; // Point de téléportation dans la même scène

    [Header("Configuration")]
    public int previousSceneIndex; // Index de la scène précédente (paramètre)

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

    private void Start()
    {
        // Gère le spawn en fonction de l'index de la scène précédente
        int spawnIndex = previousSceneIndex % 2; // Alterne entre 0 et 1
        Transform spawnPoint = spawnIndex == 0 ? spawnPoint1 : spawnPoint2;
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            player.transform.position = spawnPoint.position;
            player.transform.rotation = spawnPoint.rotation;
            Debug.Log($"Player spawné à {spawnPoint.name} (scene précédente: {previousSceneIndex})");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (targetSceneIndex >= 0)
            {
                // Transition vers une nouvelle scène
                StartCoroutine(LoadSceneWithFade(targetSceneIndex));
            }
            else if (targetSpawnPoint != null)
            {
                // Téléportation dans la même scène
                TeleportPlayer(other.transform);
            }
        }
    }

    private IEnumerator LoadSceneWithFade(int sceneIndex)
    {
        // Démarre l'effet de fondu (écran devient noir)
        yield return StartCoroutine(Fade(1f));

        // Charge la nouvelle scène
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);

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

    private void TeleportPlayer(Transform player)
    {
        player.position = targetSpawnPoint.position;
        player.rotation = targetSpawnPoint.rotation;

        Debug.Log("Player téléporté à " + targetSpawnPoint.name);
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

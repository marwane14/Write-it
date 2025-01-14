using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public string spawnPointTag = "SpawnPoint"; // Tag des points de spawn
    public List<SpawnMapping> spawnMappings; // Mappings entre mapName (map précédente) et spawnIndex
    public GameObject playerPrefab; // Objet à instancier

    private List<Transform> spawnPoints = new List<Transform>(); // Points de spawn récupérés dynamiquement
    private static string previousMapName; // Nom de la map précédente (statique pour persister entre les scènes)

    void Start()
    {
        Debug.Log("Début du SpawnManager: Initialisation des points de spawn et des mappings.");

        // Récupérer dynamiquement les points de spawn via leur tag
        GameObject[] spawnObjects = GameObject.FindGameObjectsWithTag(spawnPointTag);
        if (spawnObjects.Length == 0)
        {
            Debug.LogError($"Aucun point de spawn trouvé avec le tag '{spawnPointTag}'. Assurez-vous que les points de spawn sont bien taggés dans la scène.");
            return;
        }

        // Ajouter les points de spawn à la liste et les afficher
        spawnPoints.Clear();
        foreach (GameObject obj in spawnObjects)
        {
            spawnPoints.Add(obj.transform);
        }
        Debug.Log($"Nombre de points de spawn trouvés : {spawnPoints.Count}");
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Debug.Log($"SpawnPoint[{i}] position : {spawnPoints[i].position}");
        }

        // Vérifier si le nom de la map précédente est défini
        if (string.IsNullOrEmpty(previousMapName))
        {
            Debug.LogWarning("Le nom de la map précédente n'a pas été défini. Utilisation du SpawnPoint par défaut (index 0).");
            SpawnUsingIndex(0);
            return;
        }

        Debug.Log($"Nom de la map précédente : {previousMapName}");

        // Trouver le mapping correspondant
        SpawnMapping mapping = spawnMappings.Find(m => m.mapName == previousMapName);

        if (mapping != null)
        {
            Debug.Log($"Mapping trouvé : MapName = {mapping.mapName}, SpawnIndex = {mapping.spawnIndex}");
            SpawnUsingIndex(mapping.spawnIndex);
        }
        else
        {
            Debug.LogWarning($"Aucun mapping trouvé pour la map précédente ({previousMapName}). Utilisation du SpawnPoint par défaut (index 0).");
            SpawnUsingIndex(0); // Par défaut, utiliser le premier point
        }
    }

    private void SpawnUsingIndex(int spawnIndex)
    {
        if (spawnIndex >= 0 && spawnIndex < spawnPoints.Count)
        {
            Debug.Log($"Spawn du joueur au point d'index {spawnIndex} : Position = {spawnPoints[spawnIndex].position}");
            Instantiate(playerPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
        }
        else
        {
            Debug.LogError($"Index de spawn invalide ({spawnIndex}). Assurez-vous que l'index est compris entre 0 et {spawnPoints.Count - 1}.");
        }
    }

    public void LoadScene(string sceneName)
    {
        // Définir la scène actuelle comme map précédente
        previousMapName = SceneManager.GetActiveScene().name;
        Debug.Log($"Nom de la map actuelle défini comme map précédente : {previousMapName}");

        // Charger la nouvelle scène
        SceneManager.LoadScene(sceneName);
    }
}

[System.Serializable]
public class SpawnMapping
{
    public string mapName; // Nom de la map précédente
    public int spawnIndex; // Point de spawn associé
}

using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Points")]
    public Transform[] spawnPoints; // Liste des points de spawn disponibles dans la scène

    [Header("Player Settings")]
    public GameObject playerPrefab; // Prefab du joueur à instancier

    private void Start()
    {
        // Récupère le nom du point de spawn à utiliser
        string spawnPointName = PlayerPrefs.GetString("SpawnPoint", "");
        Transform spawnPoint = GetSpawnPointByName(spawnPointName);

        if (spawnPoint != null)
        {
            // Instancie le joueur au point de spawn spécifié
            Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning($"Point de spawn '{spawnPointName}' introuvable. Assurez-vous qu'il existe dans la scène.");
        }
    }

    // Trouve un point de spawn par son nom
    private Transform GetSpawnPointByName(string name)
    {
        foreach (Transform point in spawnPoints)
        {
            if (point.name == name)
                return point;
        }
        return null;
    }
}

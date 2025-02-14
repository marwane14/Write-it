using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private string saveFilePath;

    private void Awake()
    {
        // Définir le chemin du fichier de sauvegarde
        saveFilePath = Application.persistentDataPath + "/savefile.json";
    }

    public void SaveGame(GameData data)
    {
        try
        {
            // Convertir les données en JSON
            string jsonData = JsonUtility.ToJson(data, true);

            // Écrire les données dans un fichier
            File.WriteAllText(saveFilePath, jsonData);
            Debug.Log("Partie sauvegardée avec succès !");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Erreur lors de la sauvegarde : " + e.Message);
        }
    }

    public GameData LoadGame()
    {
        try
        {
            // Vérifier si le fichier de sauvegarde existe
            if (File.Exists(saveFilePath))
            {
                // Lire les données du fichier
                string jsonData = File.ReadAllText(saveFilePath);

                // Convertir les données JSON en objet GameData
                GameData data = JsonUtility.FromJson<GameData>(jsonData);
                Debug.Log("Partie chargée avec succès !");
                return data;
            }
            else
            {
                Debug.LogWarning("Aucun fichier de sauvegarde trouvé.");
                return null;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Erreur lors du chargement : " + e.Message);
            return null;
        }
    }

    public void DeleteSaveFile()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Fichier de sauvegarde supprimé.");
        }
        else
        {
            Debug.LogWarning("Aucun fichier de sauvegarde à supprimer.");
        }
    }
}

[System.Serializable]
public class GameData
{
    // Exemples de données que tu peux sauvegarder
    public Vector3 playerPosition; // Position du joueur
    public int playerHealth; // Santé du joueur
    public int level; // Niveau actuel
    public string currentScene; // Scène actuelle
}

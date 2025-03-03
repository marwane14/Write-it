using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private string saveFilePath;

    private void Awake()
    {
        // D�finir le chemin du fichier de sauvegarde
        saveFilePath = Application.persistentDataPath + "/savefile.json";
    }

    public void SaveGame(GameData data)
    {
        try
        {
            // Convertir les donn�es en JSON
            string jsonData = JsonUtility.ToJson(data, true);

            // �crire les donn�es dans un fichier
            File.WriteAllText(saveFilePath, jsonData);
            Debug.Log("Partie sauvegard�e avec succ�s !");
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
            // V�rifier si le fichier de sauvegarde existe
            if (File.Exists(saveFilePath))
            {
                // Lire les donn�es du fichier
                string jsonData = File.ReadAllText(saveFilePath);

                // Convertir les donn�es JSON en objet GameData
                GameData data = JsonUtility.FromJson<GameData>(jsonData);
                Debug.Log("Partie charg�e avec succ�s !");
                return data;
            }
            else
            {
                Debug.LogWarning("Aucun fichier de sauvegarde trouv�.");
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
            Debug.Log("Fichier de sauvegarde supprim�.");
        }
        else
        {
            Debug.LogWarning("Aucun fichier de sauvegarde � supprimer.");
        }
    }
}

[System.Serializable]
public class GameData
{
    // Exemples de donn�es que tu peux sauvegarder
    public Vector3 playerPosition; // Position du joueur
    public int playerHealth; // Sant� du joueur
    public int level; // Niveau actuel
    public string currentScene; // Sc�ne actuelle
}

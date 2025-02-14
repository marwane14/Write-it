using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public string sceneNom = "TitleScreen"; // Nom de la scène à charger

    void Update()
    {
        // Vérifie si la touche "Échap" est pressée
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            RetournerMenu();
        }
    }

    public void RetournerMenu()
    {
        // Charge la scène de l'écran de choix des modes de jeu
        SceneManager.LoadScene(sceneNom);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public string sceneNom = "TitleScreen"; // Nom de la sc�ne � charger

    void Update()
    {
        // V�rifie si la touche "�chap" est press�e
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            RetournerMenu();
        }
    }

    public void RetournerMenu()
    {
        // Charge la sc�ne de l'�cran de choix des modes de jeu
        SceneManager.LoadScene(sceneNom);
    }
}

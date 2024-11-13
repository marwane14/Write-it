using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingsWindow; // Fenêtre des paramètres à activer.
    public GameObject Menu;

    public void StartMenu()
    {
        Menu.SetActive(true);
    } 
    public void QuitMenu()
    {
        Menu.SetActive(false);
    } 


    public void SettingsButton()
    {
        SettingsWindow.SetActive(true); // Active la fenêtre des paramètres.
    }

    public void QuitSettingsWindow()
    {
        SettingsWindow.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

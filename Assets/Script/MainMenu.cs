using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadTestScene()
    {
        // changement de scene
        SceneManager.LoadScene("TestMPM");
    }
}

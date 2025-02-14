using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoaderEntrainement : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("TestMPM");
    }
}

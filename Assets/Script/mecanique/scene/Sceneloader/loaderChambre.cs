using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoaderChambre: MonoBehaviour
{
    public float delayBeforeLoad = 0f; 

    public void LoadGameScene()
    {
        StartCoroutine(LoadSceneWithDelay("Chambre")); 
    }

    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        yield return new WaitForSeconds(delayBeforeLoad);
        SceneManager.LoadScene(sceneName);
    }
}

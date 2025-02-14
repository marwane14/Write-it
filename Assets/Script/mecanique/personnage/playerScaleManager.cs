using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScaleManager : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Map 1")
        {
            transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        }
    }
}
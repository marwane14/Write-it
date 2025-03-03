using UnityEngine;
using UnityEngine.UI;

public class BossManager1 : MonoBehaviour
{
    [Header("R�f�rences")]
    public GameObject bossPrefab;       // Pr�fab du Boss
    public Slider bossSlider;           // Slider (barre de vie) dans la sc�ne
    public Transform bossSpawnPoint;    // Point de spawn du Boss

    private BossHP currentBoss;         // R�f�rence vers le script BossHP sur le Boss instanci�

    void Start()
    {
        // V�rifications de base
        if (bossPrefab == null)
        {
            Debug.LogError("Aucun prefab de Boss n'est assign� dans l'inspecteur.");
            return;
        }

        if (bossSpawnPoint == null)
        {
            // Si aucun spawnPoint n'est assign�, on prend la position de ce Manager
            Debug.LogWarning("Aucun point de spawn n'est assign�. Le boss sera instanci� � la position (0,0,0).");
            bossSpawnPoint = this.transform;
        }

        // Instancie le Boss
        GameObject bossInstance = Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);

        // R�cup�re le script BossHP
        currentBoss = bossInstance.GetComponent<BossHP>();
        if (currentBoss == null)
        {
            Debug.LogError("Le Pr�fab du Boss n'a pas de script BossHP attach� !");
            return;
        }

        // Initialise le Boss en lui passant la barre de vie (Slider)
        currentBoss.Setup(bossSlider);
    }

    void Update()
    {
        // Touche P = enlever 10 PV
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("P pressed => Boss takes damage");
            currentBoss?.TakeDamage(10);
        }

        // Touche M = donner 10 PV
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("M pressed => Boss heals");
            currentBoss?.Heal(10);
        }
    }
}

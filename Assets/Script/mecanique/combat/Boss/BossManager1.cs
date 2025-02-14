using UnityEngine;
using UnityEngine.UI;

public class BossManager1 : MonoBehaviour
{
    [Header("Références")]
    public GameObject bossPrefab;       // Préfab du Boss
    public Slider bossSlider;           // Slider (barre de vie) dans la scène
    public Transform bossSpawnPoint;    // Point de spawn du Boss

    private BossHP currentBoss;         // Référence vers le script BossHP sur le Boss instancié

    void Start()
    {
        // Vérifications de base
        if (bossPrefab == null)
        {
            Debug.LogError("Aucun prefab de Boss n'est assigné dans l'inspecteur.");
            return;
        }

        if (bossSpawnPoint == null)
        {
            // Si aucun spawnPoint n'est assigné, on prend la position de ce Manager
            Debug.LogWarning("Aucun point de spawn n'est assigné. Le boss sera instancié à la position (0,0,0).");
            bossSpawnPoint = this.transform;
        }

        // Instancie le Boss
        GameObject bossInstance = Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);

        // Récupère le script BossHP
        currentBoss = bossInstance.GetComponent<BossHP>();
        if (currentBoss == null)
        {
            Debug.LogError("Le Préfab du Boss n'a pas de script BossHP attaché !");
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

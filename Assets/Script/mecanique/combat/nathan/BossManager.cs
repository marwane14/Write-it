using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Image bossHealthBar; // Optionnel : UI pour afficher la santé

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        Debug.Log("BossManager démarré. Santé initiale: " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;

        Debug.Log($"BossManager: Boss a reçu {damage} dégâts. Santé actuelle: {currentHealth}");
        UpdateHealthBar();

        // Vérifiez la défaite du boss
        if (currentHealth == 0)
        {
            Debug.Log("Boss vaincu !");
            // Déclencher l’animation ou la transition de fin de combat ici
        }
    }

    void UpdateHealthBar()
    {
        if (bossHealthBar != null)
        {
            bossHealthBar.fillAmount = (float)currentHealth / maxHealth;
            Debug.Log($"Mise à jour de la barre de vie: {bossHealthBar.fillAmount * 100}%");
        }
    }
}

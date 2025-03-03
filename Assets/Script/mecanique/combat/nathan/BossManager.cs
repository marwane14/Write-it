using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Image bossHealthBar; // Optionnel : UI pour afficher la sant�

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        Debug.Log("BossManager d�marr�. Sant� initiale: " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;

        Debug.Log($"BossManager: Boss a re�u {damage} d�g�ts. Sant� actuelle: {currentHealth}");
        UpdateHealthBar();

        // V�rifiez la d�faite du boss
        if (currentHealth == 0)
        {
            Debug.Log("Boss vaincu !");
            // D�clencher l�animation ou la transition de fin de combat ici
        }
    }

    void UpdateHealthBar()
    {
        if (bossHealthBar != null)
        {
            bossHealthBar.fillAmount = (float)currentHealth / maxHealth;
            Debug.Log($"Mise � jour de la barre de vie: {bossHealthBar.fillAmount * 100}%");
        }
    }
}

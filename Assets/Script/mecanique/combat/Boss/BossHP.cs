using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;
    private SpriteRenderer spriteRend;
    private Slider hpSlider;

    public void Setup(Slider slider)
    {
        currentHP = maxHP;
        spriteRend = GetComponent<SpriteRenderer>();
        hpSlider = slider;
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = currentHP;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP < 0) currentHP = 0;

        if (hpSlider != null)
            hpSlider.value = currentHP;

        if (spriteRend != null)
            spriteRend.color = Color.red;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP) currentHP = maxHP;

        if (hpSlider != null)
            hpSlider.value = currentHP;

        if (spriteRend != null)
            spriteRend.color = Color.green;
    }
}

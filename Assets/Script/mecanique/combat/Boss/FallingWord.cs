using UnityEngine;

public class FallingWord : MonoBehaviour
{
    public float fallSpeed = 2f;  // Vitesse de chute

    void Update()
    {
        // Le mot tombe vers le bas
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }
}

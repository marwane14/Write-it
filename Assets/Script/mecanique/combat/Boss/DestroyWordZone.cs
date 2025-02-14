using UnityEngine;

public class DestroyWordZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Tout ce qui entre dans ce trigger est détruit
        Destroy(other.gameObject);
    }
}

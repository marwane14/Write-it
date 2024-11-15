using UnityEngine;
using UnityEngine.EventSystems;

public class ParticleTextEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ParticleSystem textParticles;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (textParticles != null)
            textParticles.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (textParticles != null)
            textParticles.Stop();
    }
}

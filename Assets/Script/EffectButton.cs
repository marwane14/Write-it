using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Vector3 originalScale;
    public Color hoverColor = Color.yellow; // Couleur de lumière pour le survol
    public Color defaultColor = Color.white; // Couleur par défaut
    public Image buttonImage; // Image du bouton
    public float pressScale = 0.9f; // Taille réduite lors de l'enfoncement
    public float hoverScale = 1.1f; // Taille augmentée au survol

    void Start()
    {
        // Initialisation
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;

        if (buttonImage == null)
        {
            buttonImage = GetComponent<Image>();
        }

        // Assurez-vous qu'une couleur par défaut est appliquée
        if (buttonImage != null)
        {
            buttonImage.color = defaultColor;
        }
    }

    // Survol avec la souris
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            buttonImage.color = hoverColor; // Change la couleur
        }
        rectTransform.localScale = originalScale * hoverScale; // Agrandit le bouton
    }

    // Sortie du survol
    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            buttonImage.color = defaultColor; // Restaure la couleur par défaut
        }
        rectTransform.localScale = originalScale; // Restaure la taille d'origine
    }

    // Lorsqu'on clique sur le bouton
    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.localScale = originalScale * pressScale; // Réduit la taille pour simuler l'enfoncement
    }

    // Lorsqu'on relâche le bouton
    public void OnPointerUp(PointerEventData eventData)
    {
        rectTransform.localScale = originalScale * hoverScale; // Revient à la taille de survol
    }
}


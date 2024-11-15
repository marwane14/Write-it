using UnityEngine;
using UnityEngine.EventSystems;
using TMPro; // Nécessaire pour TextMeshPro

public class TextButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public TMP_Text buttonText; // Le texte du bouton
    public Color hoverColor = Color.white; // Couleur au survol
    public Color defaultColor = Color.black; // Couleur par défaut
    public float pressScale = 0.9f; // Taille réduite au clic
    public float hoverScale = 1.1f; // Taille augmentée au survol
    public float yOffset = -15f; // Décalage vertical au survol

    private Vector3 originalScale;
    private Vector3 originalPosition;

    void Start()
    {
        // Initialisation
        if (buttonText == null)
        {
            buttonText = GetComponent<TMP_Text>();
        }

        if (buttonText != null)
        {
            buttonText.color = defaultColor; // Applique la couleur par défaut
        }

        originalScale = transform.localScale; // Sauvegarde la taille initiale
        originalPosition = transform.localPosition; // Sauvegarde la position initiale
    }

    // Survol avec la souris
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = hoverColor; // Change la couleur du texte
        }

        transform.localScale = originalScale * hoverScale; // Agrandit le bouton
        transform.localPosition = originalPosition + new Vector3(0, yOffset, 0); // Décale la position Y
    }

    // Sortie du survol
    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = defaultColor; // Restaure la couleur par défaut
        }

        transform.localScale = originalScale; // Restaure la taille d'origine
        transform.localPosition = originalPosition; // Restaure la position d'origine
    }

    // Lorsqu'on clique sur le bouton
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = originalScale * pressScale; // Réduit la taille pour simuler l'enfoncement
    }

    // Lorsqu'on relâche le bouton
    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = originalScale * hoverScale; // Revient à la taille de survol
    }
}

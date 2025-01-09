using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color hoverColor = Color.cyan; // Couleur survolée.
    public float hoverScale = 1.1f; // Facteur d’agrandissement.
    public float transitionSpeed = 10f; // Vitesse d'animation.

    private Color originalColor;
    private Vector3 originalScale;
    private Image buttonImage;

    private bool isHovering = false;

    void Start()
    {
        // Récupérer l’image du bouton et ses propriétés initiales.
        buttonImage = GetComponent<Image>();
        if (buttonImage != null)
        {
            originalColor = buttonImage.color;
        }
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    void Update()
    {
        // Transition fluide vers l'état survolé.
        if (isHovering)
        {
            // Changer la couleur et agrandir.
            if (buttonImage != null)
                buttonImage.color = Color.Lerp(buttonImage.color, hoverColor, Time.deltaTime * transitionSpeed);

            transform.localScale = Vector3.Lerp(transform.localScale, originalScale * hoverScale, Time.deltaTime * transitionSpeed);
        }
        else
        {
            // Revenir à la couleur et taille d’origine.
            if (buttonImage != null)
                buttonImage.color = Color.Lerp(buttonImage.color, originalColor, Time.deltaTime * transitionSpeed);

            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * transitionSpeed);
        }
    }
}

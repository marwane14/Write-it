using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableLetter : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string letter; // La lettre à afficher
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        Debug.Log($"DraggableLetter ({letter}) awake: Canvas trouvé = {canvas.name}");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.position;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        Debug.Log($"OnBeginDrag: Début du drag de la lettre {letter}");
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        Debug.Log($"OnEndDrag: Fin du drag de la lettre {letter}");
        // Si la lettre n'a pas été déposée dans une zone de slot, revenir à sa position d'origine
        if (!eventData.pointerEnter || eventData.pointerEnter.GetComponent<LetterSlot>() == null)
        {
            rectTransform.position = originalPosition;
            Debug.Log($"La lettre {letter} n'a pas été déposée sur un slot, retour à la position d'origine.");
        }
    }
}

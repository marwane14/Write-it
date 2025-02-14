using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LetterSlot : MonoBehaviour, IDropHandler
{
    public string currentLetter = "";

    // Cette fonction est appelée lorsque quelque chose est déposé sur ce slot
    public void OnDrop(PointerEventData eventData)
    {
        DraggableLetter letter = eventData.pointerDrag.GetComponent<DraggableLetter>();
        if (letter != null)
        {
            RectTransform letterRect = letter.GetComponent<RectTransform>();
            letterRect.position = GetComponent<RectTransform>().position;
            currentLetter = letter.letter;
            Debug.Log($"Lettre {letter.letter} déposée sur le slot {gameObject.name}");
        }
        else
        {
            Debug.LogWarning("OnDrop: Aucun composant DraggableLetter trouvé sur l'objet déposé !");
        }
    }

    // Méthode pour récupérer la lettre déposée dans ce slot
    public string GetLetter()
    {
        return currentLetter;
    }

    // Optionnel : méthode pour réinitialiser le slot
    public void ClearSlot()
    {
        currentLetter = "";
        Debug.Log($"Slot {gameObject.name} réinitialisé.");
    }
}

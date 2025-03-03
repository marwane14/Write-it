using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LetterSlot : MonoBehaviour, IDropHandler
{
    public string currentLetter = "";

    // Cette fonction est appel�e lorsque quelque chose est d�pos� sur ce slot
    public void OnDrop(PointerEventData eventData)
    {
        DraggableLetter letter = eventData.pointerDrag.GetComponent<DraggableLetter>();
        if (letter != null)
        {
            RectTransform letterRect = letter.GetComponent<RectTransform>();
            letterRect.position = GetComponent<RectTransform>().position;
            currentLetter = letter.letter;
            Debug.Log($"Lettre {letter.letter} d�pos�e sur le slot {gameObject.name}");
        }
        else
        {
            Debug.LogWarning("OnDrop: Aucun composant DraggableLetter trouv� sur l'objet d�pos� !");
        }
    }

    // M�thode pour r�cup�rer la lettre d�pos�e dans ce slot
    public string GetLetter()
    {
        return currentLetter;
    }

    // Optionnel : m�thode pour r�initialiser le slot
    public void ClearSlot()
    {
        currentLetter = "";
        Debug.Log($"Slot {gameObject.name} r�initialis�.");
    }
}

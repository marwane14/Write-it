using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordPuzzleManager : MonoBehaviour
{
    public string targetWord = "NOOBI";
    public List<LetterSlot> letterSlots; // Assignez vos slots dans l�inspecteur
    public BossManager bossManager;      // R�f�rence au boss pour appliquer des d�g�ts
    public Text feedbackText;

    public void CheckWord()
    {
        if (letterSlots == null || letterSlots.Count == 0)
        {
            Debug.LogError("WordPuzzleManager: La liste des letterSlots est vide ou non assign�e !");
            return;
        }

        if (bossManager == null)
        {
            Debug.LogError("WordPuzzleManager: bossManager non assign� !");
            return;
        }

        string formedWord = "";
        foreach (var slot in letterSlots)
        {
            if (slot == null)
            {
                Debug.LogWarning("Un des slots dans letterSlots est null !");
                continue;
            }

            string letterInSlot = slot.GetLetter();
            formedWord += letterInSlot;
            Debug.Log($"Slot {slot.gameObject.name} contient: {letterInSlot}");
        }

        Debug.Log($"Mot form�: {formedWord} -- Mot cible: {targetWord}");

        if (string.Equals(formedWord, targetWord, StringComparison.OrdinalIgnoreCase))
        {
            feedbackText.text = "Correct !";
            Debug.Log("Mot correct ! D�g�ts inflig�s au boss.");
            bossManager.TakeDamage(10);
        }
        else
        {
            feedbackText.text = "R�essayez !";
            Debug.Log("Mot incorrect, r�essayez.");
        }
    }
}

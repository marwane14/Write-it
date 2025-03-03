using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordGameManagerTMP : MonoBehaviour
{
    [Header("R�f�rences Boss")]
    public BossHP bossHP;               // R�f�rence au script du boss (pour TakeDamage)

    [Header("R�f�rences UI (TMP)")]
    public TMP_InputField wordInput;     // Le champ de saisie (TextMeshPro)
    public TMP_Text phraseAffichee;      // TMP_Text pour afficher "vous avez gagn�"

    [Header("Param�tres de spawn")]
    public GameObject wordPrefab;        // Prefab qui contient un TMP_Text + FallingWord
    public Transform spawnZone;          // Point ou zone en haut d�o� spawnent les mots
    public float spawnInterval = 2f;     // Temps en secondes entre chaque spawn

    // La phrase � deviner
    private List<string> phrase = new List<string> { "vous", "avez", "gagn�" };
    private int currentWordIndex = 0; // Index du mot actuel � trouver

    // Variantes possibles pour chaque mot (fautes, synonymes, etc.)
    private Dictionary<string, List<string>> variantes = new Dictionary<string, List<string>>()
    {
        {
            "vous", new List<string>() { "vou", "voux", "vont", "vons", "vous" }
        },
        {
            "avez", new List<string>() { "ave", "avez", "avex", "avezz", "avez" }
        },
        {
            "gagn�", new List<string>() { "gagn", "gagn�e", "gagn�", "gan�", "gagner" }
        }
    };

    void Start()
    {
        // Affiche la phrase compl�te au-dessus du boss
        if (phraseAffichee != null)
            phraseAffichee.text = "Phrase : " + string.Join(" ", phrase);

        // Force le focus sur l'InputField
        if (wordInput != null)
        {
            wordInput.Select();
            wordInput.ActivateInputField();
        }

        // Lance la coroutine qui fait spawner des mots
        StartCoroutine(SpawnWordsCoroutine());
    }

    void Update()
    {
        // Emp�che de quitter l'InputField
        if (wordInput != null && !wordInput.isFocused)
        {
            wordInput.Select();
            wordInput.ActivateInputField();
        }

        // Quand on appuie sur Entr�e, on valide le mot
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ValiderMot();
        }
    }

    /// <summary>
    /// V�rifie si le mot tap� correspond au mot correct de la phrase.
    /// </summary>
    private void ValiderMot()
    {
        if (wordInput == null || bossHP == null) return;
        if (currentWordIndex >= phrase.Count) return; // Phrase d�j� termin�e

        // R�cup�re le mot tap� (en minuscules)
        string motTape = wordInput.text.Trim().ToLower();
        // Mot correct � l'index actuel
        string motCorrect = phrase[currentWordIndex].ToLower();

        if (motTape == motCorrect)
        {
            Debug.Log("Mot correct : " + motTape);
            // Infliger 10 PV de d�g�ts au boss
            bossHP.TakeDamage(10);

            // Passe au mot suivant
            currentWordIndex++;
            if (currentWordIndex >= phrase.Count)
            {
                Debug.Log("Tous les mots de la phrase ont �t� trouv�s !");
                // Ici, tu peux stopper le spawn, afficher un message final, etc.
                StopAllCoroutines();
            }
        }
        else
        {
            Debug.Log("Mot incorrect : " + motTape);
        }

        // Vide le champ de saisie et re-focus
        wordInput.text = "";
        wordInput.Select();
        wordInput.ActivateInputField();
    }

    /// <summary>
    /// Coroutine qui spawn des mots al�atoires selon le mot actuel � trouver.
    /// </summary>
    private IEnumerator SpawnWordsCoroutine()
    {
        while (true)
        {
            if (currentWordIndex >= phrase.Count)
            {
                yield break; // On a termin� la phrase, on arr�te
            }

            // Mot cible actuel
            string motCible = phrase[currentWordIndex];
            // On r�cup�re la liste des variantes pour ce mot
            List<string> listeVariantes = variantes[motCible];

            // On choisit une variante al�atoire
            int randIndex = Random.Range(0, listeVariantes.Count);
            string randomVar = listeVariantes[randIndex];

            // On spawn ce mot
            SpawnWord(randomVar);

            // Attend avant de spawner le prochain
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    /// <summary>
    /// Instancie un "wordPrefab" pour afficher le mot choisi.
    /// </summary>
    private void SpawnWord(string textToDisplay)
    {
        if (wordPrefab == null || spawnZone == null) return;

        // Position de spawn
        Vector3 spawnPos = spawnZone.position;
        // L�g�re variation horizontale
        float offsetX = Random.Range(-3f, 3f);
        spawnPos.x += offsetX;

        // Instancie le prefab
        GameObject wordGO = Instantiate(wordPrefab, spawnPos, Quaternion.identity);

        // R�cup�re le composant TMP_Text pour y mettre le mot
        TMP_Text textComponent = wordGO.GetComponentInChildren<TMP_Text>();
        if (textComponent != null)
        {
            textComponent.text = textToDisplay;
        }
        else
        {
            Debug.LogWarning("Le prefab de mot n'a pas de composant TMP_Text !");
        }
    }
}

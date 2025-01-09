using UnityEngine;
using TMPro;

public class GestionJeu : MonoBehaviour
{
    public static GestionJeu Instance; // Singleton

    public TMP_InputField champSaisie;
    public TMP_Text texteScore;
    public TMP_Text texteVies;

    private int score = 0;
    private int vies = 3;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        MettreAJourInterface();
        champSaisie.onValueChanged.AddListener(VerifierMot);

        // Assure que le champ de saisie est actif au démarrage
        champSaisie.ActivateInputField();
    }

    void Update()
    {
        // Vérifie si le champ de saisie a perdu le focus
        if (!champSaisie.isFocused)
        {
            champSaisie.ActivateInputField();
        }
    }

    void VerifierMot(string motTape)
    {
        foreach (ComportementMot mot in FindObjectsOfType<ComportementMot>())
        {
            if (mot.ObtenirMot().Equals(motTape, System.StringComparison.OrdinalIgnoreCase))
            {
                Destroy(mot.gameObject);
                AjouterScore();
                champSaisie.text = ""; // Efface la saisie
                break;
            }
        }
    }

    public void AjouterScore()
    {
        score += 10;
        MettreAJourInterface();
    }

    public void PerdreVie()
    {
        vies--;
        MettreAJourInterface();

        if (vies <= 0)
        {
            FinDeJeu();
        }
    }

    void MettreAJourInterface()
    {
        texteScore.text = $"Score : {score}";
        texteVies.text = $"Vies : {vies}";
    }

    void FinDeJeu()
    {
        Debug.Log("Fin de la partie !");
        // Arrêter le jeu ou afficher un écran de fin
    }
}
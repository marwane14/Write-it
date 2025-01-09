using UnityEngine;
using TMPro;

public class ComportementMot : MonoBehaviour
{
    public float vitesseChute = 2f; // Vitesse de chute
    private string mot;
    public TMP_Text texteMot;

    void Start()
    {
        texteMot = GetComponent<TMP_Text>();
    }

    void Update()
    {
        // Fait descendre le mot
        transform.Translate(Vector3.down * vitesseChute * Time.deltaTime);

        // Détruit le mot s'il atteint le bas de l'écran
        if (transform.position.y < -5)
        {
            GestionJeu.Instance.PerdreVie();
            Destroy(gameObject);
        }
    }

    public void DefinirMot(string nouveauMot)
    {
        mot = nouveauMot;
        if (texteMot != null)
        {
            texteMot.text = mot;
        }
    }

    public string ObtenirMot()
    {
        return mot;
    }
}
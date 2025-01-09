using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateurMots : MonoBehaviour
{
    public GameObject prefabMot; // Prefab pour les mots
    public RectTransform zoneDeSpawn; // Zone où les mots apparaissent
    public float intervalleSpawn = 2f; // Intervalle entre chaque mot

    private List<string> listeMots = new List<string> { "chat", "chien", "soleil", "maison", "arbre" };

    void Start()
    {
        StartCoroutine(GenererMots());
    }

    IEnumerator GenererMots()
    {
        while (true)
        {
            CreerMot();
            yield return new WaitForSeconds(intervalleSpawn);
        }
    }

    void CreerMot()
    {
        // Récupère une position aléatoire dans le RectTransform
        Vector2 positionAleatoire = ObtenirPositionAleatoire();

        // Instancie le prefab et le place à la position calculée
        GameObject nouveauMot = Instantiate(prefabMot, zoneDeSpawn);
        nouveauMot.transform.localPosition = positionAleatoire;
        nouveauMot.GetComponent<ComportementMot>().DefinirMot(listeMots[Random.Range(0, listeMots.Count)]);
    }

    Vector2 ObtenirPositionAleatoire()
    {
        // Taille et position de la zone de spawn
        Vector2 tailleZone = zoneDeSpawn.rect.size;
        Vector2 positionCentre = zoneDeSpawn.rect.center;

        // Calcul de la position aléatoire locale
        float x = Random.Range(positionCentre.x - tailleZone.x / 2, positionCentre.x + tailleZone.x / 2);
        float y = Random.Range(positionCentre.y - tailleZone.y / 2, positionCentre.y + tailleZone.y / 2);

        return new Vector2(x, y);
    }
}
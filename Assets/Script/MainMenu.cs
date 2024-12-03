using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingsWindow; // Fenêtre des paramètres à activer.
    public GameObject Menu; // Menu principal.
    public GameObject EcranTitre; // Écran titre.
    public TextMeshProUGUI hintText; // Texte "Appuyez sur Échap" avec animation.

    private Vector2 initialPosition; // Position initiale du texte pour l'animation.

    private void Start()
    {
        // Sauvegarde la position initiale du texte.
        if (hintText != null)
        {
            initialPosition = hintText.rectTransform.anchoredPosition;
        }
    }

    private void Update()
    {
        // Animation du texte (oscillation sur l'axe Y).
        AnimateHintText();

        // Retour à l'écran titre lorsque l'utilisateur appuie sur Échap.
        if (Input.GetKeyDown(KeyCode.Escape) && !EcranTitre.activeSelf)
        {
            BackToTitle();
        }
    }

    public void StartMenu()
    {
        // Affiche le menu et cache l'écran titre.
        Menu.SetActive(true);
        EcranTitre.SetActive(false);
    }

    public void QuitMenu()
    {
        // Cache le menu et affiche l'écran titre.
        Menu.SetActive(false);
        EcranTitre.SetActive(false);
    }

    public void SettingsButton()
    {
        // Active la fenêtre des paramètres.
        SettingsWindow.SetActive(true);
    }

    public void QuitSettingsWindow()
    {
        // Désactive la fenêtre des paramètres.
        SettingsWindow.SetActive(false);
    }

    public void QuitGame()
    {
        // Quitte l'application.
        Application.Quit();
    }

    private void AnimateHintText()
    {
        if (hintText != null)
        {
            // Oscillation uniquement sur Y, sans modifier la position X.
            float offsetY = Mathf.Sin(Time.time * 2f) * 10f; // Oscillation.
            hintText.rectTransform.anchoredPosition = new Vector2(initialPosition.x, initialPosition.y + offsetY);
        }
    }

    private void BackToTitle()
    {
        // Retour à l'écran titre.
        EcranTitre.SetActive(true);
        Menu.SetActive(false);
    }
    public void LoadEntrainementScene()
    {
        SceneManager.LoadScene("TestMPM");
    }
}

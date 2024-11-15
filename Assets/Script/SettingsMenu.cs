using UnityEngine;
using UnityEngine.Audio;
using TMPro;  // Importer TextMeshPro
using System.Collections.Generic;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdownMenu;  // Nouveau nom pour éviter l'ambiguïté
    public AudioMixer mainAudioMixer;  // Nouveau nom pour éviter l'ambiguïté
    private Resolution[] resolution;

    public void Start()
    {
        // Récupère toutes les résolutions disponibles
        resolution = Screen.resolutions.Select(res => new Resolution { width = res.width, height = res.height }).Distinct().ToArray();

        // Vide les options existantes dans le dropdown
        resolutionDropdownMenu.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        // Ajoute chaque résolution à la liste des options
        for (int i = 0; i < resolution.Length; i++)
        {
            string option = resolution[i].width + " x " + resolution[i].height;
            options.Add(option);

            if (resolution[i].width == Screen.width && resolution[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Ajoute les options de résolutions au dropdown
        resolutionDropdownMenu.AddOptions(options);
        resolutionDropdownMenu.value = currentResolutionIndex;
        resolutionDropdownMenu.RefreshShownValue();
    }

    public void SetVolume(float volume)
    {
        mainAudioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);  // Ajustez pour l'échelle dB
        Debug.Log(volume);
    }
    public void FullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolutions = resolution[resolutionIndex];
        Screen.SetResolution(resolutions.width, resolutions.height, Screen.fullScreen);
    }
}

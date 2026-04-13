using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    private const string filename = "GameSettings";

    [SerializeField] private TextMeshProUGUI resText;
    private int[,] resolutions = { { 1280, 720 }, { 1920, 1080 }, { 3840, 2160} };
    private int resolutionIndex = 1;

    [SerializeField] private Slider audioSlider;
    AudioSource[] audioSources;
    private int volume = 50;

    [SerializeField] private InputActionReference[] actionAssets;
    private string[] overrides;

    [SerializeField] private Slider sensSlider;
    private int sensitivity = 50;

    private Language language = Language.en;

    public void Init()
    {
        audioSources = FindObjectsByType<AudioSource>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
        
        LoadSettings();
    }
    public void SaveSettings()
    {
        var newSettings = new GameSettingsJSON();
        newSettings.resolutionIndex = resolutionIndex;
        newSettings.volume = volume;
        newSettings.bindings = overrides;
        newSettings.sensitivity = sensitivity;
        newSettings.language = language;

        string json = JsonUtility.ToJson(newSettings, true);
        SaveSystem.Save(filename, json);
    }
    private void LoadSettings()
    {
        string saveString = SaveSystem.Load(filename);
        if (saveString != null)
        {
            var data = JsonUtility.FromJson<GameSettingsJSON>(saveString);

            SetResolution(data.resolutionIndex);
            SetVolume(data.volume);
            SetBindings(data.bindings);
            SetSensitivity(data.sensitivity);
            SetLanguage(data.language);
        }
    }

    public void ChangeResolution()
    {
        if (resolutionIndex == 2) resolutionIndex = 0;
        else resolutionIndex++;

        SetResolution(resolutionIndex);
    }
    private void SetResolution(int index)
    {
        resolutionIndex = index;
        int w = resolutions[resolutionIndex, 0];
        int h = resolutions[resolutionIndex, 1];

        Screen.SetResolution(w, h, FullScreenMode.ExclusiveFullScreen);

        resText.text = w + ":" + h;
    }

    public void ChangeVolume(float value)
    {
        foreach (var audioSource in audioSources)
        {
            audioSource.volume = (int)value;
        }
        volume = (int)value;
    }
    private void SetVolume(int value)
    {
        foreach (var audioSource in audioSources)
        {
            audioSource.volume = value;
        }
        volume = value;
        audioSlider.value = value;
    }

    public void ChangeBindings()
    {
        List<string> ovrs = new List<string>();
        for (int i = 0; i < actionAssets.Length; i++)
        {
            var asset = actionAssets[i];
            string json = asset.action.SaveBindingOverridesAsJson();
            ovrs.Add(json);
        };
        overrides = ovrs.ToArray();
    }
    private void SetBindings(string[] bindings)
    {
        if (bindings != null)
        {
            for (int i = 0; i < actionAssets.Length; i++)
            {
                var asset = actionAssets[i];
                asset.action.LoadBindingOverridesFromJson(bindings[i]);
            }
            overrides = bindings;
        }
    }

    public void ChangeSensitivity(float value)
    {
        sensitivity = (int)value;
    }
    private void SetSensitivity(int value)
    {
        sensitivity = value;
        sensSlider.value = value;
    }

    public void ChangeLanguage()
    {
        GameManager.instance.localisationManager.ChangeLanguage();
        language = GameManager.instance.localisationManager.GetLanguage();
    }
    private void SetLanguage(Language lang)
    {
        language = lang;
        GameManager.instance.localisationManager.ChangeLanguage(language);
    }
}


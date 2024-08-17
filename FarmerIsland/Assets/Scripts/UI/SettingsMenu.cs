using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider = null, _sfxSlider = null;
    
    public static event System.Action<float> OnChangedMusicSettingSliderValue = null;
    public static event System.Action<float> OnChangedSFXSettingSliderValue = null;

    private void OnEnable()
    {
        if (SaveSystem.Instance)
            _musicSlider.value = SaveSystem.Instance.GetMusicVolume();
    }

    public void HandleOnChangedSoundSliderValue(float newValue)
    {
        OnChangedMusicSettingSliderValue?.Invoke(newValue);
    }
    public void HandleOnChangedSFXSliderValue(float newValue)
    {
        OnChangedSFXSettingSliderValue?.Invoke(newValue);
    }
    
    private void Start()
    {
        _musicSlider.value = SaveSystem.Instance.GetMusicVolume();
        _sfxSlider.value = SaveSystem.Instance.GetSFXVolume();
    }
}
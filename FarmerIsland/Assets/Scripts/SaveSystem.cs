using UnityEngine;

public class SaveSystem : Singleton<SaveSystem>
{
    private const string PLAYER_PREFS_MUSIC_VOLUME_KEY = "Volume";
    private const string PLAYER_PREFS_SFX_VOLUME_KEY = "Sfx";

    private void OnEnable()
    {
        SoundManager.OnChangedMusicVolume += HandleOnChangedMusicVolume;
        SoundManager.OnChangeSFXVolume += HandleOnChangedSFXVolume;
    }

    private void OnDisable()
    {
        SoundManager.OnChangedMusicVolume -= HandleOnChangedMusicVolume;
        SoundManager.OnChangeSFXVolume -= HandleOnChangedSFXVolume;
    }
    
    private void HandleOnChangedMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME_KEY, volume);
    }
    
    private void HandleOnChangedSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat(PLAYER_PREFS_SFX_VOLUME_KEY, volume);
    }

    public float GetMusicVolume() => PlayerPrefs.HasKey(PLAYER_PREFS_MUSIC_VOLUME_KEY) ? PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME_KEY) : 1.00f;
    public float GetSFXVolume() => PlayerPrefs.HasKey(PLAYER_PREFS_SFX_VOLUME_KEY) ? PlayerPrefs.GetFloat(PLAYER_PREFS_SFX_VOLUME_KEY) : 1.00f;
}
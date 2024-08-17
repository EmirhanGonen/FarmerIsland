using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string IN_GAME_BACKGROUND_MUSIC_RESOURCES_PATH = "Audios/InGameBackgroundMusic";
    private const string FRUIT_BUSH_COLLECT_SOUND_RESOURCES_PATH = "Audios/FruitBushCollectedSound";

    private AudioClip _backgroundMusicClip = null;
    private AudioSource _backgroundMusicSource = null;

    private AudioClip _fruitBushCollectSoundClip = null;
    private AudioSource _fruitBushCollectSoundSource = null;

    public static event System.Action<float> OnChangedMusicVolume = null;
    public static event System.Action<float> OnChangeSFXVolume = null;

    private void OnEnable()
    {
        SettingsMenu.OnChangedMusicSettingSliderValue += HandleOnMusicValueChanged;
        SettingsMenu.OnChangedSFXSettingSliderValue += HandleOnSFXValueChanged;
        FruitBush.OnCollected += HandleOnFruitBushCollected;
    }

    private void OnDisable()
    {
        SettingsMenu.OnChangedMusicSettingSliderValue -= HandleOnMusicValueChanged;
        SettingsMenu.OnChangedSFXSettingSliderValue -= HandleOnSFXValueChanged;
        FruitBush.OnCollected -= HandleOnFruitBushCollected;
    }

    private void Awake()
    {
        _backgroundMusicClip = Resources.Load<AudioClip>(IN_GAME_BACKGROUND_MUSIC_RESOURCES_PATH);
        _fruitBushCollectSoundClip = Resources.Load<AudioClip>(FRUIT_BUSH_COLLECT_SOUND_RESOURCES_PATH);
    }


    private void Start()
    {
        /*GameObject backgroundMusicGameObject = Instantiate(new GameObject());
        backgroundMusicGameObject.name = "BackgroundMusic";
        _backgroundMusicSource = backgroundMusicGameObject.AddComponent<AudioSource>();
        _backgroundMusicSource.clip = _backgroundMusicClip;
        _backgroundMusicSource.loop = true;
        _backgroundMusicSource.volume = SaveSystem.Instance.GetMusicVolume();
        _backgroundMusicSource.Play();*/
        SpawnSoundElement("BackgroundMusic", _backgroundMusicClip, true, true, out _backgroundMusicSource);
        SpawnSoundElement("CollectSoundObject", _fruitBushCollectSoundClip, false, false, out _fruitBushCollectSoundSource);
    }

    private void SpawnSoundElement(string objectName, AudioClip _audioClip, bool isLoop, bool autoStart, out AudioSource outSource)
    {
        GameObject backgroundMusicGameObject = new GameObject
        {
            name = objectName
        };
        
        outSource = backgroundMusicGameObject.AddComponent<AudioSource>();
        outSource.clip = _audioClip;
        outSource.loop = isLoop;
        outSource.volume = SaveSystem.Instance.GetMusicVolume();
        
        if (autoStart)
            outSource.Play();
    }


    private void HandleOnMusicValueChanged(float newSoundLevelValue)
    {
        _backgroundMusicSource.volume = newSoundLevelValue;
        OnChangedMusicVolume?.Invoke(_backgroundMusicSource.volume);
    }

    private void HandleOnSFXValueChanged(float newSoundLevelValue)
    {
        _fruitBushCollectSoundSource.volume = newSoundLevelValue;
        OnChangeSFXVolume?.Invoke(_fruitBushCollectSoundSource.volume);
    }

    private void HandleOnFruitBushCollected()
    {
        _fruitBushCollectSoundSource.volume = SaveSystem.Instance.GetSFXVolume();
        _fruitBushCollectSoundSource.PlayOneShot(_fruitBushCollectSoundClip);
        
        //AudioSource spawnedFruitBushCollectedSoundSource = Instantiate(_fruitBushCollectSoundClip).GetComponent<AudioSource>();
       // spawnedFruitBushCollectedSoundSource.volume = SaveSystem.Instance.GetMusicVolume();
    }
}
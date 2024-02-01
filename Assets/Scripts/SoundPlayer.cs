using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioData
{
    public SFX audioName;
    public AudioClip audioClip;

}

public class SoundPlayer : MonoBehaviour
{
    public AudioData[] audioDataArray;

    [SerializeField] private AudioSource _musicSource, _effectsSource;

    private Dictionary<SFX, AudioClip> audioClips;

    private void Start()
    {
        if (_musicSource != null) _musicSource.gameObject.SetActive(true);
        if (_effectsSource != null) _effectsSource.gameObject.SetActive(true);

        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        audioClips = new Dictionary<SFX, AudioClip>();

        foreach (var audioData in audioDataArray)
        {
            audioClips.Add(audioData.audioName, audioData.audioClip);
        }
    }

    public void PlaySound(SFX sfx)
    {
        if (audioClips.ContainsKey(sfx))
        {
            _effectsSource.PlayOneShot(audioClips[sfx]);
        }
    }

    #region ObserverSubscription
    private void OnEnable()
    {
        Collectable.collectedActionSFX += PlaySound;
        PlayerLife.playerDeathSFX += PlaySound;
        PlayerMovement.jumpActionSFX += PlaySound;
        CheckPoint.checkPointActionSFX += PlaySound;
        InitLevelLoad.playerEnterActionSFX += PlaySound;
        LoadNextLevel.finishLevelActionSFX += PlaySound;
        Trampoline.trampolineActionSFX += PlaySound;
        UpDownCollider.trampolineActionSFX += PlaySound;
    }

    private void OnDisable()
    {
        Collectable.collectedActionSFX -= PlaySound;
        PlayerLife.playerDeathSFX -= PlaySound;
        PlayerMovement.jumpActionSFX -= PlaySound;
        CheckPoint.checkPointActionSFX -= PlaySound;
        InitLevelLoad.playerEnterActionSFX -= PlaySound;
        LoadNextLevel.finishLevelActionSFX -= PlaySound;
        Trampoline.trampolineActionSFX -= PlaySound;
        UpDownCollider.trampolineActionSFX -= PlaySound;
    }
    #endregion

}

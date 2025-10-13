using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;
    public AudioSource musicSource; // 🎵 Thêm nguồn phát nhạc nền

    [Header("Audio Clips ENEMY")]
    public AudioClip ghostAttackClip;
    public AudioClip skeleton1AttackClip;
    public AudioClip skeleton2AttackClip;

    [Header("Audio Clips PLAYER")]
    public AudioClip playerAttackClip;
    public AudioClip playerDeathClip;

    [Header("Audio Clips ITEM")]
    public AudioClip pickupItem;
    public AudioClip coinClip;

    [Header("Audio Clips BUTTON")]
    public AudioClip buttonClickClip;

    [Header("Music Clips")]
    public AudioClip backgroundMusic;

    private float sfxVolume = 1f;
    private float musicVolume = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (backgroundMusic != null)
            PlayMusic(backgroundMusic);
    }
    // ------------------ SFX --------------------- //
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    // ------------------ PLAYER ------------------ //
    public void PlayPlayerAttack() => PlaySFX(playerAttackClip);
    public void PlayPlayerDeath() => PlaySFX(playerDeathClip);

    // ------------------ ENEMY ------------------- //
    public void PlayEnemyDeath(AudioClip audioClip)
    {
        PlaySFX(audioClip);
    }
    public void PlayGhostAttack() => PlaySFX(ghostAttackClip);
    public void PlaySkeleton1Attack() => PlaySFX(skeleton1AttackClip);
    public void PlaySkeleton2Attack() => PlaySFX(skeleton2AttackClip);

    // ------------------ ITEM -------------------- //
    public void PlayPickupItem() => PlaySFX(pickupItem);
    public void PlayCoin() => PlaySFX(coinClip);

    // ------------------ BUTTON ------------------ //
    public void PlayButtonClick()
    {
        sfxSource.PlayOneShot(buttonClickClip, sfxVolume);
    }

    // ------------------ MUSIC ------------------ //
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        musicSource.volume = musicVolume;
    }
    public void SetSfxVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        sfxSource.volume = sfxVolume;
    }
    public void IncreaseMusicVolume(float amount = 0.1f)
    {
        SetMusicVolume(musicVolume + amount);
    }

    public void DecreaseMusicVolume(float amount = 0.1f)
    {
        SetMusicVolume(musicVolume - amount);
    }
    public float GetMusicVolume()
    {
        return musicVolume;
    }

    public float GetSfxVolume()
    {
        return sfxVolume;
    }
}
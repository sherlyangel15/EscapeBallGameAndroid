using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Game Music")]
    [SerializeField] AudioClip gameMusic;
    [SerializeField] [Range(0f, 1f)] float gameMusicvolume = 1f;

    [Header("Click Sound")]
    [SerializeField] AudioClip clickSound;
    [SerializeField] [Range(0f, 1f)] float clickSoundvolume = 1f;

    [Header("Coin Sound")]
    [SerializeField] AudioClip coinSound;
    [SerializeField] [Range(0f, 1f)] float coinSoundvolume = 1f;

    [Header("Heart Sound")]
    [SerializeField] AudioClip heartSound;
    [SerializeField] [Range(0f, 1f)] float heartSoundvolume = 1f;

    [Header("Dead Sound")]
    [SerializeField] AudioClip deadSound;
    [SerializeField] [Range(0f, 1f)] float deadSoundvolume = 1f;

    [Header("Explosion Crunch Sound")]
    [SerializeField] AudioClip explosionCrunchSound;
    [SerializeField] [Range(0f, 1f)] float explosionCrunchSoundvolume = 1f;

    [Header("Survival Mode")]
    [SerializeField] AudioClip SurvivalMode;
    [SerializeField] [Range(0f, 1f)] float SurvivalModevolume = 1f;
    
    [Header("Victory Sound")]
    [SerializeField] AudioClip VictorySound;
    [SerializeField] [Range(0f, 1f)] float VictorySoundvolume = 1f;

    AudioSource myAudioSource;

    //singleton pattern instance
    static AudioPlayer instance;
    void Awake()
    {
        ManageSingleton();
        myAudioSource = GetComponent<AudioSource>();
    }
    void ManageSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        myAudioSource.loop = true;
        myAudioSource.Play();
    }

    public void PlayGameMusic() => PlayClip(gameMusic, gameMusicvolume);
    public void PlayClickSound() =>PlayClip(clickSound, clickSoundvolume);
    public void PlayCoinSound() =>PlayClip(coinSound, coinSoundvolume);
    public void PlayHeartSound() => PlayClip(heartSound, heartSoundvolume);
    public void PlayDieSound() => PlayClip(deadSound, deadSoundvolume);
    public void PlayExplosionCrunchSound() => PlayClip(explosionCrunchSound, explosionCrunchSoundvolume);
    public void PlaySurvivalMode() => PlayClip(SurvivalMode, SurvivalModevolume);
    public void PlayVictoryMusic() => PlayClip(VictorySound, VictorySoundvolume);

    void PlayClip(AudioClip clip, float volume)
    {
        if(clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }

    public void MuteAllSound() => AudioListener.pause = true;
    public void UnMuteAllSound() => AudioListener.pause = false;

}

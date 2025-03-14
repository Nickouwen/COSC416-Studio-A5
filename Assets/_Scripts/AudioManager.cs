using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header ("Audio Source")]
    public AudioSource sfxSource;

    [Header ("Audio Clips")]
    public AudioClip impactBrickClip;
    public AudioClip impactPaddleClip;
    public AudioClip impactWallClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else 
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}

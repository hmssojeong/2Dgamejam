using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public enum ESfx
    {
        SFXEnemyExplosion,
        SFXGetItem,
        SFXSpecialSkill,
        SFXGameOver,
    }

    [SerializeField] AudioClip[] sfxs;
    [SerializeField] AudioSource audioSfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(ESfx esfx)
    {
        audioSfx.PlayOneShot(sfxs[(int)esfx]);
    }
}

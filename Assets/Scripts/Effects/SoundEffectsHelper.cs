using UnityEngine;
using System.Collections;

public class SoundEffectsHelper : MonoBehaviour {

    public static SoundEffectsHelper Instance;    
    public AudioClip gunShotSound;
    public AudioClip swapSound;

    void Awake()
    {
        // Register the singleton
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SoundEffectsHelper!");
        }
        Instance = this;
    }

    public void MakeSwapSound()
    {
        MakeSound(swapSound, 1f);
    }

    public void MakeGunShotSound()
    {
        MakeSound(gunShotSound, 0.5f);
    }
    
    private void MakeSound(AudioClip originalClip, float volume)
    {                
        AudioSource.PlayClipAtPoint(originalClip, transform.position, volume);
    }
}

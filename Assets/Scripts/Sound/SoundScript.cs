using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour {

    public static SoundScript Instance;

    public AudioClip JumpSound;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Existem múltiplas instâncias do script SoundScript.");
        }

        Instance = this;
    }
    public void MakeJumpSound()
    {
        MakeSound(JumpSound);
    }
    private void MakeSound(AudioClip originalClip)
    {
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
}

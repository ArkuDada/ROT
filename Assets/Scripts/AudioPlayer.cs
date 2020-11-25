using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] clip;
    public float volume = 0.5f;

    public void playSound(int choice)
    {
        audioSource.PlayOneShot(clip[choice], volume);
    }
}

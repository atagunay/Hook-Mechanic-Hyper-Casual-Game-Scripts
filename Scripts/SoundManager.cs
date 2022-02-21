using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{


    [SerializeField]
    private AudioSource winSound;

    public void PlayWinSound()
    {
        winSound.Play();

    }
}

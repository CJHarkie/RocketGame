using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private AudioClip MetalHitSound;
    private AudioSource RocketAudio;

    void Start()
    {
        RocketAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            RocketAudio.PlayOneShot(MetalHitSound);
        }
    }
}

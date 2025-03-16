using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingPad : MonoBehaviour
{
    [SerializeField] private AudioClip LevelCompleteSound;
    private AudioSource RocketAudio;

    void Start()
    {
        RocketAudio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            RocketAudio.PlayOneShot(LevelCompleteSound);
            Debug.Log("Level Completed!");
        }
    }
}

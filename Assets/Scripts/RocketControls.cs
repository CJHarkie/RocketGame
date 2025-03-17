using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class RocketControls : MonoBehaviour
{
    [SerializeField] float ThrustForce;
    [SerializeField] float RotationForce;

    [SerializeField] AudioClip EngineSound;
    [SerializeField] AudioClip GameOverSound;
    [SerializeField] AudioClip LevelCompleteSound;
    [SerializeField] AudioClip MetalHitSound;
    [SerializeField] float FuelRate = 10;
    Rigidbody rocketRB;
    AudioSource RocketAudio;
    bool RocketCrashed = false;
    public int Coins = 0;
    public float Fuel = 1000.0f;

    void Start()
    {
        rocketRB = GetComponent<Rigidbody>();
        RocketAudio = GetComponent<AudioSource>();
        RocketCrashed = false;
        Coins = 0;
        Fuel = 1000.0f;
    }

    void Update()
    {
        if (!RocketCrashed && Fuel > 0)
        {
            InputManagement();
        }
    }

    private void InputManagement()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rocketRB.AddRelativeForce(Vector3.forward * Time.deltaTime * ThrustForce);
            Fuel -= FuelRate * Time.deltaTime;
            Debug.Log(Fuel);
            if (!RocketAudio.isPlaying)
            {
                RocketAudio.PlayOneShot(EngineSound);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            RocketAudio.Stop();
        }
        if (Input.GetKey(KeyCode.A))
        {
            rocketRB.freezeRotation = true;
            transform.Rotate(Vector3.down * Time.deltaTime * RotationForce);
            rocketRB.freezeRotation = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rocketRB.freezeRotation = true;
            transform.Rotate(Vector3.up * Time.deltaTime * RotationForce);
            rocketRB.freezeRotation = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {       
        if(collision.gameObject.tag == "Respawn")
        {
            Debug.Log("Game Over.");
            RocketCrashed = true;
            RocketAudio.PlayOneShot(GameOverSound);
            StartCoroutine(ReloadGame());
        }
        if (collision.gameObject.tag == "Finish")
        {
            RocketCrashed = true;
            RocketAudio.PlayOneShot(LevelCompleteSound);
            StartCoroutine(LoadNextLevel());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Coin":
                Coins++;
                Debug.Log("Coins : " + Coins);
                RocketAudio.PlayOneShot(MetalHitSound);
                Destroy(other.gameObject);
                break;
            case "Fuel":
                Fuel += 200;
                Destroy(other.gameObject);
                break;
        }
    }

    IEnumerator ReloadGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

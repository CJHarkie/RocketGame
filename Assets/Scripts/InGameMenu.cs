using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{

    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject OptionsMenu;
    [SerializeField] GameObject HUD;
    [SerializeField] RocketControls RocketControls;
    [SerializeField] TextMeshProUGUI FuelText;
    [SerializeField] TextMeshProUGUI CoinText;

    void Start()
    {
        PauseMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        HUD.SetActive(true);
    }

    void Update()
    {
        FuelText.text = "Fuel : " + RocketControls.Fuel.ToString();
        CoinText.text = "Coins : " + RocketControls.Coins.ToString();
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0.0f;
            PauseMenu.SetActive(true);
            HUD.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        PauseMenu.SetActive(false);
        HUD.SetActive(true);
    }

    public void QuitGameMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void MuteValueChange()
    {
        Debug.Log("Mute / UnMute");
        Camera camera = FindFirstObjectByType<Camera>();
        if (camera != null)
        {
            AudioListener cameraAudio = camera.GetComponent<AudioListener>();
            cameraAudio.enabled = cameraAudio.enabled ? false : true;
        }
    }
}

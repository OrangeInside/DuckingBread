using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;
    public Image seedBar;
    public TextMeshProUGUI livesValue;
    public GameObject feedButton;
    public GameObject lostGameScreen;
    public GameObject wonGameScreen;
    public GameObject feedingTimerObject;
    public TextMeshProUGUI feedingTimerText;

    private void Awake()
    {
        UIManager.Instance = this;
    }

    public void UpdateUI()
    {
        seedBar.fillAmount = GameController.Instance.seedLevel / GameController.Instance.maxSeedLevel;
    }

    public void UpdateTimer()
    {
        feedingTimerText.text = Mathf.Round(GameController.Instance.currentFeedingTime) + "s";
    }
    public void UpdateLives()
    {
        livesValue.text = "x" + GameController.Instance.lives;
    }

    public void ToggleTimer(bool toggle)
    {
        feedingTimerObject.SetActive(toggle);
    }
    public void ToggleSeedButton(bool toggle)
    {
        feedButton.SetActive(toggle);
    }
    
    public void ToggleLostGameScreen(bool toggle)
    {
        lostGameScreen.SetActive(toggle);
        FreezeTime();
    }

    public void ToggleWonGameScreen(bool toggle)
    {
        wonGameScreen.SetActive(toggle);
        FreezeTime();
    }

    public void FreezeTime()
    { 
        Time.timeScale = 0f;
    }

    public void UnFreezeTime()
    {
        Time.timeScale = 1f;
    }
}

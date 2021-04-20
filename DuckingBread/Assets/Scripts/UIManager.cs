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
    private void Awake()
    {
        UIManager.Instance = this;
    }

    public void UpdateUI()
    {
        seedBar.fillAmount = GameController.Instance.seedLevel / GameController.Instance.maxSeedLevel;
    }

    public void UpdateLives()
    {
        livesValue.text = "x" + GameController.Instance.lives;
    }

    public void ToggleSeedButton(bool toggle)
    {
        feedButton.SetActive(toggle);
    }
        
}

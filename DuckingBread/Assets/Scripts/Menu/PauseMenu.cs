using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour {

	public GameObject ui;
	public GameObject continueButton;
	public Color defaultButtonColor;

	public string menuSceneName = "MainMenu";

	public SceneFader sceneFader;

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape) )
		{

			Toggle();
		}
	}

    public void Toggle ()
	{
		continueButton.GetComponent<Image>().color = defaultButtonColor;

		ui.SetActive(!ui.activeSelf);
		TouchSpawn x = FindObjectOfType<TouchSpawn>();
		x.enabled = !x.enabled;
		if (ui.activeSelf)
		{
			Time.timeScale = 0f;
		} 
		else
		{
			Time.timeScale = 1f;
		}
	}

	public void Retry ()
	{
		Toggle();
		sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

	public void Menu ()
	{
		Toggle();
		sceneFader.FadeTo("MainMenu");
	}

}

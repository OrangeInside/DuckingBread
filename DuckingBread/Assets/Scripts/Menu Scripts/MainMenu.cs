using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	 string levelToLoad = "SampleScene";
	
	public SceneFader sceneFader;
	public void Start()
	{
		Cursor.lockState = CursorLockMode.Confined;
	}
	public void Play ()
	{
		sceneFader.FadeTo(levelToLoad);
	}
	public void Tutorial()
	{
		sceneFader.FadeTo("tutorial");
	}
	public void Quit ()
	{
		Debug.Log("Exciting...");
		Application.Quit();
	}

}

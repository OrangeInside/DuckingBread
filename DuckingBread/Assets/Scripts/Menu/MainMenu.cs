using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	string levelToLoad = "SampleScene";
	
	private SceneFader sceneFader;

    private void Start()
    {
		sceneFader = FindObjectOfType<SceneFader>();
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

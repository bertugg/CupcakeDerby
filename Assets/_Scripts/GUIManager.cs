using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour {

	public GameObject MainPanel;
	public GameObject PlayerSelectionPanel;
	private int selectedItemNumber;

    public void OnPlayButton()
    {
		OpenGameScene ();
		//ShowPlayerSelectionPanel ();
    }
	public void OnExitButton()
	{
		Application.Quit ();
	}
	public void BackButtonPressed()
	{
		ShowMainPanel ();
	}
	public void ShowMainPanel()
	{
		
	}
	private void ShowPlayerSelectionPanel()
	{
		
	}
	public static void OpenGameScene()
	{
		SceneManager.LoadScene ("GameScene");
	}
	public static void OpenMainMenu()
	{
		SceneManager.LoadScene ("MenuScene");
	}
}

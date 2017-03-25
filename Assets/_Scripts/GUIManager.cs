using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GUIManager : MonoBehaviour {
	public Image fadeImage;

	public GameObject MainPanel;
	public GameObject PlayerSelectionPanel;
	public GameObject PausePanel;
	public GameObject OptionsPanel;

	[SerializeField]
	private GameObject continueButton;
	[SerializeField]
	private GameObject playButton;
	private SelectPlayer playerSelectionScript;

	void Awake()
	{
		if (GameObject.FindObjectsOfType<Canvas>().Length > 1)
		{
			Destroy (this.gameObject); // Destroy extra canvases
		}
		else
		{
			DontDestroyOnLoad (this.gameObject);
			DontDestroyOnLoad (EventSystem.current.gameObject);
		}
		switch (SceneManager.GetActiveScene ().name) {
		case "MenuScene":
			ShowPanel (PanelType.MainMenu);
			break;
		case "GameScene":
			break;
		case "SelectionScene":
			ShowPanel (PanelType.PlayerSelection);
			break;
		}
	}

	void Update()
	{
		if (PlayerSelectionPanel.gameObject.activeInHierarchy) {
			if (Input.GetKeyUp(KeyCode.Alpha1)) {
				playerSelectionScript.PlayerJoined (1);
			}	

			else if (Input.GetKeyUp(KeyCode.Alpha2)) {
				playerSelectionScript.PlayerJoined (2);
			}

			else if (Input.GetKeyUp(KeyCode.Alpha3)) {
				playerSelectionScript.PlayerJoined (3);
			}

			else if (Input.GetKeyUp(KeyCode.Alpha4)) {
				playerSelectionScript.PlayerJoined (4);
			}
			else if (Input.GetButton("Start1") || Input.GetButton("Start2")) 
			{
				HidePanel (PanelType.PlayerSelection);
				OpenScene ("GameScene");
			}
		}
		if (Input.GetButton("Start1") || Input.GetButton("Start2")) 
		{
			ShowPanel(PanelType.Pause);
		}
	}

    public void OnPlayButton()
    {
		Time.timeScale = 1f;
		HidePanel (PanelType.MainMenu);
		ShowPanel (PanelType.PlayerSelection);
    }
	public void OnExitButton()
	{
		Application.Quit ();
	}
	public void OnBackButton()
	{
		Time.timeScale = 1f;
		HidePanel (PanelType.Pause);
		OpenScene ("MenuScene");
		ShowPanel (PanelType.MainMenu);
	}
	public void OnContinueButton()
	{
		Time.timeScale = 1f;
		HidePanel (PanelType.Pause);
	}
	public void ShowPanel(PanelType panelType)
	{
		switch (panelType) {
		case PanelType.MainMenu:
			if (MainPanel != null) {
				MainPanel.gameObject.SetActive (true);
				EventSystem.current.SetSelectedGameObject (playButton, new BaseEventData (EventSystem.current));
			}
				break;
			case PanelType.Options:
				if (OptionsPanel != null)
					OptionsPanel.gameObject.SetActive (true);
				break;
		case PanelType.Pause:
			if (PausePanel != null && SceneManager.GetActiveScene().name == "GameScene") {
				PausePanel.gameObject.SetActive (true);
				EventSystem.current.SetSelectedGameObject (continueButton, new BaseEventData (EventSystem.current));
				Time.timeScale = 0f;
			}
				break;
		case PanelType.PlayerSelection:
			if (PlayerSelectionPanel != null) {
				PlayerSelectionPanel.gameObject.SetActive (true);
				playerSelectionScript = PlayerSelectionPanel.GetComponent<SelectPlayer> ();
			}
			break;
		default:
			break;
		}
	}
	public void HidePanel(PanelType panelType)
	{
		switch (panelType) {
		case PanelType.MainMenu:
			MainPanel.gameObject.SetActive (false);
			break;
		case PanelType.Options:
			OptionsPanel.gameObject.SetActive (false);
			break;
		case PanelType.Pause:
			Time.timeScale = 1f;
			PausePanel.gameObject.SetActive (false);
			break;
		case PanelType.PlayerSelection:
			PlayerSelectionPanel.gameObject.SetActive (false);
			break;
		default:
			break;
		}
	}
	public void OpenScene(string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}
}

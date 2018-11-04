using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour {
	public Image fadeImage;

	public GameObject MainPanel;
	public GameObject PlayerSelectionPanel;
	public GameObject PausePanel;
	public GameObject OptionsPanel;
	public GameObject HudPanel;

	[SerializeField]
	private Sprite damagedFace;
	[SerializeField]
	private GameObject continueButton;
	[SerializeField]
	private GameObject playButton;
	private SelectPlayer playerSelectionScript;
	public PlayMusic musicController;
	public HUDController hudController;

	public List<GameObject> cupcakes;


	void Awake()
	{
		musicController = GetComponent<PlayMusic> ();
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
			musicController.PlaySelectedMusic (MusicType.MainMenu);
			break;
		case "GameScene":
			ShowPanel (PanelType.HudPanel);
			musicController.PlaySelectedMusic (MusicType.Game);
			placeCharacters ();
			break;
		case "SelectionScene":
			ShowPanel (PanelType.PlayerSelection);
			break;
		}
	}

	void Update()
	{
		if (PlayerSelectionPanel.gameObject.activeInHierarchy) {
			if (Input.GetButton("Fire1")) {
				playerSelectionScript.PlayerJoined (1);
			}	

			else if (Input.GetButton("Fire2")) {
				playerSelectionScript.PlayerJoined (2);
			}

			else if (Input.GetButton("Fire3")) {
				playerSelectionScript.PlayerJoined (3);
			}

			else if (Input.GetButton("Fire4")) {
				playerSelectionScript.PlayerJoined (4);
			}
			else if (Input.GetButton("Start1") || Input.GetButton("Start2")) 
			{
				HidePanel (PanelType.PlayerSelection);
				ShowPanel (PanelType.HudPanel);
				musicController.PlaySelectedMusic (MusicType.Game);
				OpenScene ("GameScene");
				StartCoroutine(waitForSceneChanged()); // Wait for scene changing
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
		musicController.PlaySelectedSound (SoundType.Audience);
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
		HidePanel (PanelType.HudPanel);
		ShowPanel (PanelType.MainMenu);
		OpenScene ("MenuScene");
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
		case PanelType.HudPanel:
			if (HudPanel != null) {
				HudPanel.gameObject.SetActive (true);
				Debug.Log (playerSelectionScript.joinedPlayers);
				hudController.createHUD (playerSelectionScript.joinedPlayers, 100);
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
		case PanelType.HudPanel:
			HudPanel.gameObject.SetActive (false);
			break;
		default:
			break;
		}
	}
	public void OpenScene(string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}
	private void placeCharacters()
	{
		for (int i = 0; i < playerSelectionScript.joinedPlayers.Count; ++i) {
			
			if (playerSelectionScript.joinedPlayers [i]) {
				GameObject cupCake = null;
				CupcakeController cupcakeController = null;

				Debug.Log ("Placing " + i + " cupcake");

				switch (i) {
				case 0: // Player 1 joined
					cupCake = Instantiate (cupcakes [i], new Vector2 (-4f, 1f), Quaternion.identity);
					cupcakeController = cupCake.GetComponent<CupcakeController> ();
					break;
				case 1: // Player 2 joined
					cupCake = Instantiate (cupcakes [i], new Vector2(4f, 1f),Quaternion.identity);
					cupcakeController = cupCake.GetComponent<CupcakeController> ();
					break;
				case 2: // Player 3 joined
					cupCake = Instantiate (cupcakes [i], new Vector2(-4f, -1f),Quaternion.identity);
					cupcakeController = cupCake.GetComponent<CupcakeController> ();
					break;
				case 3: // Player 4 joined
					cupCake = Instantiate (cupcakes [i], new Vector2 (4f, -1f), Quaternion.identity);
					cupcakeController = cupCake.GetComponent<CupcakeController> ();
					break;
				default:
					break;
				}
				cupcakeController._hpBar = hudController.hpBars [i];
				cupcakeController.soundManager = musicController;
				cupcakeController.sadFace = damagedFace;
			}
		}
	}
	private IEnumerator waitForSceneChanged()
	{
		while (SceneManager.GetActiveScene().name != "GameScene") {
			yield return new WaitForEndOfFrame ();
		}
		placeCharacters ();
	}
}

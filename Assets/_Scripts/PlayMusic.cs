﻿using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayMusic : MonoBehaviour {


	public AudioClip menuMusic;						//Assign Audioclip for title music loop
	public AudioClip gameMusic;						//Assign Audioclip for ingame
	public AudioClip victoryMusic;					//Assign Audioclip for Victory 

	public List<AudioClip> hitSound;
	public AudioClip audienceSound;
	public AudioClip teaDrop;

	public AudioMixerSnapshot volumeDown;			//Reference to Audio mixer snapshot in which the master volume of main mixer is turned down
	public AudioMixerSnapshot volumeUp;				//Reference to Audio mixer snapshot in which the master volume of main mixer is turned up

	[SerializeField]
	private AudioSource musicSource;				//Reference to the AudioSource which plays music
	[SerializeField]
	private AudioSource soundSource;				//Reference to the AudioSource which plays music
	private float resetTime = .01f;					//Very short time used to fade in near instantly without a click

	void Awake () 
	{
		//Get a component reference to the AudioSource attached to the UI game object
		//musicSource = GetComponent<AudioSource> ();
		//Call the PlayLevelMusic function to start playing music
	}


	public void PlayLevelMusic()
	{
		//This switch looks at the last loadedLevel number using the scene index in build settings to decide which music clip to play.
		switch (SceneManager.GetActiveScene().name)
		{
			//If scene index is 0 (usually title scene) assign the clip titleMusic to musicSource
			case "MenuScene":
			musicSource.clip = menuMusic;
				break;
			//If scene index is 1 (usually main scene) assign the clip mainMusic to musicSource
			case "GameScene":
			musicSource.clip = gameMusic;
				break;
			default:
			musicSource.clip = menuMusic;
				break;
		}
		//Fade up the volume very quickly, over resetTime seconds (.01 by default)
		FadeUp (resetTime);
		//Play the assigned music clip in musicSource
		musicSource.Play ();
	}
	
	//Used if running the game in a single scene, takes an integer music source allowing you to choose a clip by number and play.
	public void PlaySelectedMusic(MusicType musicType)
	{

		//This switch looks at the integer parameter musicChoice to decide which music clip to play.
		switch (musicType) 
		{
			//if musicChoice is 0 assigns titleMusic to audio source
			case MusicType.MainMenu:
			musicSource.clip = menuMusic;
				break;
				//if musicChoice is 1 assigns mainMusic to audio source
			case MusicType.Game:
			musicSource.clip = gameMusic;
				break;
			case MusicType.Victory:
			musicSource.clip = menuMusic;
				break;
			default:
			musicSource.clip = menuMusic;
				break;
		}
		//Play the selected clip
		musicSource.Play ();
	}
	public void PlaySelectedSound(SoundType soundType)
	{
		switch (soundType) {
		case SoundType.Hit:
			soundSource.clip = hitSound [Random.Range (0, hitSound.Count - 1)];
			break;
		case SoundType.Audience:
			soundSource.clip = audienceSound;
			break;
		default:
			break;
		}
		soundSource.Play ();
	}
	//Call this function to very quickly fade up the volume of master mixer
	public void FadeUp(float fadeTime)
	{
		//call the TransitionTo function of the audioMixerSnapshot volumeUp;
		volumeUp.TransitionTo (fadeTime);
	}

	//Call this function to fade the volume to silence over the length of fadeTime
	public void FadeDown(float fadeTime)
	{
		//call the TransitionTo function of the audioMixerSnapshot volumeDown;
		volumeDown.TransitionTo (fadeTime);
	}
}

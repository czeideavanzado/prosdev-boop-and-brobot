﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour {
	public Slider volumeSlider;
	public string audioType;

	private AudioSource audioSource;
	private float volumeLevel;

	// Use this for initialization
	void Start () {
		volumeLevel = 0.5f;

		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		volumeLevel = volumeSlider.value;
		audioSource.volume = volumeLevel;

		if (audioType == "bg") {
			PlayerPrefs.SetFloat ("VolumeBG", volumeLevel);
		}
	}
}

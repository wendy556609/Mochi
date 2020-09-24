﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour {

	public static SceneController Instance;

	private string activeScene;

	
	// Use this for initialization
	void Awake () {
		if(Instance == null){
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else{
			Destroy(gameObject);
		}
	}

	void Start(){
		activeScene = "Start";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadNextScene(string nextScene){
		SceneManager.LoadScene(nextScene);
	}

	public void ExitGame(){
		Application.Quit();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
	public static bool GameIsPause = false;

	public GameObject pausemanager;
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameIsPause)
			{
				Resume();
			}
			else{
				Pause();
			}
		}
	}
	void Resume ()
	{
	pausemanager.SetActive(false);
	Time.timeScale = 1f;
	GameIsPause = false;
	}

	void Pause()
	{
	pausemanager.SetActive(true);
	Time.timeScale = 0f;
	GameIsPause = true;
	}
}

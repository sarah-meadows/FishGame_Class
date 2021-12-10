using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu1 : MonoBehaviour
{
	public static bool GameIsPause = false;

	public GameObject pausemanager;
	
	public void Pausegic()
    {
		if (GameIsPause)
		{
			Resume();
		}
		else
		{
			Pause();
		}
	}

	public void Resume ()
	{
	pausemanager.SetActive(false);
	Time.timeScale = 1f;
	GameIsPause = false;
	}

	public void Pause()
	{
	pausemanager.SetActive(true);
	Time.timeScale = 0f;
	GameIsPause = true;
	}
}

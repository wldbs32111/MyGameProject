using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public GameObject ExitPanel;
    public void ExitApp()
	{
		ExitPanel.gameObject.SetActive( true );
	}
	public void YesButton()
	{
		Application.Quit();
	}
	public void NoButton()
	{
		ExitPanel.gameObject.SetActive( false );
	}
	public void GameStart(int i)
	{
		SceneManager.LoadScene( i );
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[System.Serializable]
public enum GameState
{
	Title,
	Running,
	Paused,
	Won,
	Lost,
}


public class GameManager : MonoBehaviour
{

	public string _sceneGameRunning;

	#region Inspector Variables

	#endregion



	private static GameManager Singleton { get; set; }

	void Awake()
	{
		Debug.Assert(Singleton == null, "Cannot create multiple instances of the 'GameManager' singleton class.");

		Singleton = this;
	}

	void Start()
	{
		SceneManager.LoadScene(1, LoadSceneMode.Additive);
	}

	public static void SwitchGameState(GameState gamestate)
	{
		switch (gamestate)
		{
			case GameState.Title: 
				break;
			case GameState.Lost: 
				SceneManager.UnloadScene(2);
				break;
			case GameState.Won: 
				SceneManager.UnloadScene(2);
				break;
			case GameState.Paused: 

				break;
			case GameState.Running:
				
				Singleton.StartCoroutine(Singleton.LoadSceneRunning());
				break;
			default:
				break;
		}
	}

	IEnumerator LoadSceneRunning()
	{
		yield return SceneManager.LoadSceneAsync(_sceneGameRunning, LoadSceneMode.Additive);
		SceneManager.SetActiveScene(SceneManager.GetSceneByName(_sceneGameRunning));
	}
}

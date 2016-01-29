using UnityEngine;
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
	#region Inspector Variables
	#endregion

	GameState _gameState = GameState.Title;

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
}

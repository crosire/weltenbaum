using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Wave
{
	public int AmountType0, AmountType1, AmountType2;
}

public class EnemyManager : MonoBehaviour
{
	#region Inspector Variables
	[SerializeField]
	GameObject[] _enemies;
	[SerializeField]
	Transform[] _enemiesSpawnpoints;
	[SerializeField]
	Wave[] _waves;
	#endregion

	int _finishedWaves = 0, _aliveEnemies = 0;

	public static int AliveEnemies { get; set; }

	private static EnemyManager Singleton { get; set; }

	void Awake()
	{
		Debug.Assert(Singleton == null, "Cannot create multiple instances of the 'GameManager' singleton class.");

		Singleton = this;
	}

	void Update()
	{
		if (_aliveEnemies <= 0)
		{
			SpawnWave();
		}
	}

	void SpawnWave()
	{
		_aliveEnemies = _waves[_finishedWaves].AmountType0 + _waves[_finishedWaves].AmountType1 + _waves[_finishedWaves].AmountType2;

		for (int i = 0; i < _aliveEnemies; i++)
		{
			Instantiate(_enemies[0], _enemiesSpawnpoints[0].position, Quaternion.identity);
		}

		if (_finishedWaves < _waves.Length)
		{
			_finishedWaves++;
		}
		else
		{
			GameManager.SwitchGameState(GameState.Won);
		}
	}
}

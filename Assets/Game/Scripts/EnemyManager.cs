using UnityEngine;

[System.Serializable]
public struct Wave
{
	public float SpawnInterval;
	public int[] SpawnAmounts;
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

	int _currentWave = -1, _remainingEnemies = 0;
	float _spawnTimeout = 0.0f;

	public static int AliveEnemies { get; set; }

	private static EnemyManager Singleton { get; set; }

	void Awake()
	{
		Debug.Assert(Singleton == null, "Cannot create multiple instances of the 'GameManager' singleton class.");

		Singleton = this;
	}

	void Update()
	{
		if (_spawnTimeout > 0.0f)
		{
			_spawnTimeout -= Time.deltaTime;
		}
		else if (_remainingEnemies <= 0)
		{
			if (AliveEnemies <= 0)
			{
				NextWave();
			}
		}
		else
		{
			_spawnTimeout = _waves[_currentWave].SpawnInterval;

			Debug.Assert(_waves[_currentWave].SpawnAmounts.Length == _enemies.Length, "Wave spawn amount array length does not match EnemyManager enemy array length.");

			int randomType, randomSpawnpoint = Random.Range(0, _enemiesSpawnpoints.Length);

			do
			{
				randomType = Random.Range(0, _enemies.Length);
			}
			while (_waves[_currentWave].SpawnAmounts[randomType] <= 0);

			AliveEnemies++;
			_remainingEnemies--;
			_waves[_currentWave].SpawnAmounts[randomType]--;

			var instance = (GameObject)Instantiate(_enemies[randomType], _enemiesSpawnpoints[randomSpawnpoint].position, Quaternion.identity);

			instance.transform.SetParent(this.transform);
		}
	}

	void NextWave()
	{
		if (++_currentWave >= _waves.Length)
		{
			GameManager.SwitchGameState(GameState.Won);
			return;
		}

		_remainingEnemies = 0;
		System.Array.ForEach(_waves[_currentWave].SpawnAmounts, amount => _remainingEnemies += amount);
	}
}

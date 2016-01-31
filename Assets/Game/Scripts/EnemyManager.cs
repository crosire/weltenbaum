using UnityEngine;

[System.Serializable]
public struct Wave
{
	public float SpawnInterval;
	public int[] SpawnAmounts;
}

[RequireComponent(typeof(AudioSource))]
public class EnemyManager : MonoBehaviour
{
	#region Inspector Variables
	[SerializeField]
	GameObject[] _enemies;
	[SerializeField]
	Wave[] _waves;
	#endregion

	int _currentWave = -1, _remainingEnemies = 0, _aliveEnemies = 0;
	float _spawnTimeout = 0.0f, _fightingTimeout = 0.0f;
	Vector3[] _spawnpoints;
	AudioSource _audio;

	public static bool Fighting
	{
		get { return Singleton._fightingTimeout > 0.0f; }
		set { Singleton._fightingTimeout = 5.0f; }
	}
	public static int AliveEnemies
	{
		get { return Singleton._aliveEnemies; }
		set { Singleton._aliveEnemies = value; }
	}

	private static EnemyManager Singleton { get; set; }

	void Awake()
	{
		Debug.Assert(Singleton == null, "Cannot create multiple instances of the 'EnemyManager' singleton class.");

		Singleton = this;

		_audio = GetComponent<AudioSource>();
	}
	void Start()
	{
		_spawnpoints = new Vector3[LaneManager.LaneCount];

		for (int lane = 0; lane < _spawnpoints.Length; lane++)
		{
			_spawnpoints[lane] = LaneManager.GetWaypoint(lane, 0);
		}
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

			int randomType, randomSpawnpoint = Random.Range(0, _spawnpoints.Length);

			do
			{
				randomType = Random.Range(0, _enemies.Length);
			}
			while (_waves[_currentWave].SpawnAmounts[randomType] <= 0);

			AliveEnemies++;
			_remainingEnemies--;
			_waves[_currentWave].SpawnAmounts[randomType]--;

			var instance = (GameObject)Instantiate(_enemies[randomType], _spawnpoints[randomSpawnpoint], Quaternion.identity);

			instance.transform.SetParent(this.transform);
			instance.GetComponent<PathFollowAgent>().LaneIndex = randomSpawnpoint;
		}

		if (_fightingTimeout > 0.0f)
		{
			_audio.volume += Time.deltaTime;
			_fightingTimeout -= Time.deltaTime;
		}
		else
		{
			_audio.volume -= Time.deltaTime;
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

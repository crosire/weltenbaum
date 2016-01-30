using UnityEngine;

public class AllyManager : MonoBehaviour
{
	#region Inspector Variables
	[SerializeField]
	GameObject[] _allies;
	[SerializeField]
	Transform[] _alliesSpawnpoints;
	[SerializeField]
	CameraMovement _cameraMovement;
	#endregion

	void Start()
	{
		_cameraMovement = GameObject.Find("Camera").GetComponent<CameraMovement>();
	}

	public void Spawn(int type)
	{
		var instance = (GameObject)Instantiate(_allies[type], _alliesSpawnpoints[_cameraMovement.LaneIndex].position, Quaternion.identity);

		instance.transform.SetParent(this.transform);
		instance.GetComponent<PathFollowAgent>().LaneIndex = _cameraMovement.LaneIndex;
	}
}

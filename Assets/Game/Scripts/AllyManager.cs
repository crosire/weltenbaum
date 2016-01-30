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

	public void OnGesture1()
	{
		var instance = (GameObject)Instantiate(_allies[0], _alliesSpawnpoints[_cameraMovement.LaneIndex].position, Quaternion.identity);

		instance.transform.SetParent(this.transform);
		instance.GetComponent<PathFollowAgent>().LaneIndex = _cameraMovement.LaneIndex;
	}
}

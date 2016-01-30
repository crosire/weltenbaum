using UnityEngine;
using DG.Tweening;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
	public Transform _cameraFocus;
	public Transform[] _laneCameraPositions;
	public Transform[] _cameraTransitionsPositions;
	int _preLaneIndex;
	int _laneIndex = 0;
	float _distance = 0;


	public int LaneIndex { get { return _laneIndex; } }

	void Start()
	{
		this.transform.DOMove(_laneCameraPositions[_laneIndex].transform.position, 1f);
	}

	void Update()
	{
		this.transform.DOLookAt(_cameraFocus.transform.position, 0);
		UpdateLaneIndex();
		if (_distance > 0)
		{
			
			var tmp = Vector3.Slerp(new Vector2(_laneCameraPositions[_laneIndex].position.x, _laneCameraPositions[_laneIndex].position.z), new Vector2(_laneCameraPositions[_preLaneIndex].position.x, _laneCameraPositions[_preLaneIndex].position.z), _distance);
			tmp.z = tmp.y;
			tmp.y = this.transform.position.y;
			this.transform.position = tmp;
			_distance -= Time.deltaTime;
		}
	}

	void UpdateLaneIndex()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			_preLaneIndex = _laneIndex;
			if (_laneIndex > 0)
			{
				_laneIndex--;
			}
			else
			{
				_laneIndex = _laneCameraPositions.Length - 1;
			}
			_distance = 1;
		}
		else if (Input.GetKeyDown(KeyCode.D))
		{
			_preLaneIndex = _laneIndex;
			if (_laneIndex < _laneCameraPositions.Length - 1)
			{
				_laneIndex++;
			}
			else
			{
				_laneIndex = 0;
			}
			_distance = 1;
		}

	}
}

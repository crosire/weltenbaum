using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour
{
	#region Inspector Variables
	[SerializeField]
	Transform _lookAt;
	[SerializeField]
	Transform[] _viewPositions;
	#endregion

	int _laneIndex = 0, _laneIndexPrev = 0;
	float _distance = 0;

	public int LaneIndex { get { return _laneIndex; } }

	void Start()
	{
		this.transform.DOMove(_viewPositions[_laneIndex].transform.position, 1f);
	}

	void Update()
	{
		if (_distance > 0.0f)
		{
			this.transform.position = Vector3.Slerp(_viewPositions[_laneIndex].position, _viewPositions[_laneIndexPrev].position, _distance);

			_distance -= Time.deltaTime;
		}
		else if (Input.GetKeyDown(KeyCode.D))
		{
			_distance = 1.0f;
			_laneIndexPrev = _laneIndex--;

			if (_laneIndex < 0)
			{
				_laneIndex = _viewPositions.Length - 1;
			}
		}
		else if (Input.GetKeyDown(KeyCode.A))
		{
			_distance = 1.0f;
			_laneIndexPrev = _laneIndex++;

			if (_laneIndex >= _viewPositions.Length)
			{
				_laneIndex = 0;
			}
		}

		this.transform.LookAt(_lookAt.position);
	}
}

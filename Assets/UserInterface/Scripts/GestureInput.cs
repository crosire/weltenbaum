using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System.Collections.Generic;

public enum GestureDirection
{
	Up,
	Down,
	Left,
	Right
}
[System.Serializable]
public struct Gesture
{
	public string Name;
	public GestureDirection[] Directions;
	public UnityEvent Callback;
}

public class GestureInput : MonoBehaviour
{
	#region Inspector Variables
	[SerializeField]
	[Tooltip("The maximum time in seconds a gesture recording may last.")]
	float _recordingTimeout = 5.0f;
	[SerializeField]
	[Tooltip("The minimum amount of pixels the mouse cursor must be away from the last position before it is recorded.")]
	float _recordingEpsilon = 10.0f;
	[SerializeField]
	[Tooltip("The minimum amount of pixels a recorded position must be away from the previous line segment before it is recognized as a new one.")]
	float _lineSegmentEpsilon = 5.0f;
	[SerializeField]
	Gesture[] _gestures = new Gesture[0];
	#endregion

	bool _recording = false;
	float _recordingLength = 0.0f;
	readonly List<Vector2> _history = new List<Vector2>();

	void Update()
	{
		if (Input.GetMouseButton(0) && _recordingLength < _recordingTimeout)
		{
			_recording = true;
			_recordingLength += Time.deltaTime;

			var pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

			if (Vector2.Distance(pos, _history.LastOrDefault()) > _recordingEpsilon)
			{
				_history.Add(pos);
			}
		}
		else if (_recording)
		{
			_recording = false;
			_recordingLength = 0.0f;

			var matches = new List<Gesture>(_gestures);

			// Remove all positions that are not vertices
			for (int i = 2; i < _history.Count;)
			{
				var v = _history[i - 2];
				var x = _history[i - 1];
				var w = _history[i - 0];

				float t = Vector2.Dot(x - v, w - v) / (w - v).sqrMagnitude;
				float d = t < 0 ? Vector2.Distance(x, v) : t > 1 ? Vector2.Distance(x, w) : Vector2.Distance(x, v + t * (w - v));

				if (d < _lineSegmentEpsilon)
				{
					_history.RemoveAt(i - 1);
				}
				else
				{
					i++;
				}
			}

			// Calculate gesture directions from positions and find matches
			for (int i = 1; i < _history.Count && matches.Count != 0; i++)
			{
				var direction = _history[i - 1] - _history[i - 0];
				float angle = Vector2.Angle(direction, Vector2.right);

				if (Vector3.Cross(direction, Vector3.right).z < 0)
				{
					angle = 360 - angle;
				}

				GestureDirection gestureDirection;

				if (angle >= 45.0f && angle < 135.0f)
				{
					gestureDirection = GestureDirection.Up;
				}
				else if (angle >= 135.0f && angle < 225.0f)
				{
					gestureDirection = GestureDirection.Right;
				}
				else if (angle >= 225.0f && angle < 315.0f)
				{
					gestureDirection = GestureDirection.Down;
				}
				else
				{
					gestureDirection = GestureDirection.Left;
				}

				matches.RemoveAll(it => i > it.Directions.Length || it.Directions[i - 1] != gestureDirection);
			}

			matches.RemoveAll(it => it.Directions.Length != _history.Count - 1);
			_history.Clear();

			if (matches.Count == 1)
			{
				Debug.Log("Match: " + matches.First().Name);

				matches.First().Callback.Invoke();
			}
			else if (matches.Count == 0)
			{
				Debug.Log("No match.");
			}
			else
			{
				Debug.Log("Ambiguous match.");
			}
		}
	}
}

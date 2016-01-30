﻿using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(GestureInput))]
public class GestureVisualisation : MonoBehaviour
{
	#region Inspector Variables
	[SerializeField]
	float _successTimeout = 1.0f;
	[SerializeField]
	Color _lineColor = Color.red;
	[SerializeField]
	Color _lineColorOnSuccess = Color.green;
	#endregion

	LineRenderer _renderer;
	GestureInput _gestureInput;
	int _vertexCount = 0;
	Vector3 _lastVertex;
	float _timeout = 0.0f;
	bool _wasRecordingLastFrame = false;

	void Awake()
	{
		_renderer = GetComponent<LineRenderer>();
		_gestureInput = GetComponent<GestureInput>();

		var gestures = _gestureInput.Gestures;

		for (int i = 0; i < gestures.Length; i++)
		{
			gestures[i].Callback.AddListener(OnGestureSuccess);
		}

		Reset();
	}

	void Update()
	{
		if (_gestureInput.Recording)
		{
			if (!_wasRecordingLastFrame || _timeout > 0.0f)
			{
				Reset();

				_wasRecordingLastFrame = true;
			}

			var vertex = Input.mousePosition;

			if (_vertexCount > 0 && Vector3.Distance(vertex, _lastVertex) < 2.0f)
			{
				return;
			}
			else
			{
				_lastVertex = vertex;
			}

			vertex.z = Camera.main.nearClipPlane;
			vertex = Camera.main.ScreenToWorldPoint(vertex);

			// Fix line artifacts
			vertex.z += 0.001f * Input.mousePosition.x / Screen.width;

			_renderer.SetVertexCount(++_vertexCount);
			_renderer.SetPosition(_vertexCount - 1, vertex);
		}
		else if (_timeout > 0.0f)
		{
			_timeout -= Time.deltaTime;
		}
		else if (_wasRecordingLastFrame)
		{
			Reset();

			_wasRecordingLastFrame = false;
		}
	}
	void Reset()
	{
		_timeout = 0.0f;
		_renderer.SetColors(_lineColor, _lineColor);
		_renderer.SetVertexCount(_vertexCount = 0);
	}

	void OnGestureSuccess()
	{
		_timeout = _successTimeout;
		_renderer.SetColors(_lineColorOnSuccess, _lineColorOnSuccess);
	}
}

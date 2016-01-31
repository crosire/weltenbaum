﻿using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup), typeof(AudioSource))]
public class SplashUI : MonoBehaviour
{
	#region Inspector Variables

	[SerializeField]
	CanvasGroup _background, _info, _info2;

	#endregion

	Sequence _sequence;
	AudioSource _audio;
	bool _ready = false;

	void Awake()
	{
		_audio = GetComponent<AudioSource>();
	}

	void Start()
	{
		const float fadeLength = 5f;
		float timeout = _audio.clip.length - fadeLength;

		_sequence = DOTween.Sequence()
			.Insert(0, _background.DOFade(1f, fadeLength))
			.Insert(0, _info.DOFade(1f, fadeLength))
			.AppendInterval(timeout)
			.Insert(timeout, _info.DOFade(0f, fadeLength))
			.Append(_info2.DOFade(1f, 1f));
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			_sequence.Complete(true);
			DOTween.Sequence().AppendInterval(1f).AppendCallback(() => _ready = true);
		}
		if (Input.anyKeyDown && _ready)
		{
			SceneManager.LoadSceneAsync(1);
		}
	}
}

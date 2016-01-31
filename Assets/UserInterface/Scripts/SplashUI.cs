using UnityEngine;
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
			.Append(_info2.DOFade(1f, 1f))
			.AppendInterval(1f)
			.OnComplete(() => _ready = true);
	}

	void Update()
	{
		if (Input.anyKeyDown)
		{
			if (_ready)
			{
				SceneManager.LoadSceneAsync(1);
			}
			else
			{
				_sequence.Complete(true);
			}
		}
	}
}

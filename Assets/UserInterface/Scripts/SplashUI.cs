using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup), typeof(AudioSource))]
public class SplashUI : MonoBehaviour
{
	#region Inspector Variables
	[SerializeField]
	CanvasGroup _background, _info;
	#endregion

	Sequence _sequence;
	AudioSource _audio;

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
			.Insert(timeout, _background.DOFade(0f, fadeLength))
			.Insert(timeout, _info.DOFade(0f, fadeLength))
			.AppendCallback(() => SceneManager.LoadSceneAsync(1));
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			_sequence.Complete(true);
		}
	}
}

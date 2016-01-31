using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup), typeof(AudioSource))]
public class IngameUI : MonoBehaviour
{
	#region Inspector Variables
	#endregion

	AudioSource[] _audio;

	void Awake()
	{
		_audio = GetComponents<AudioSource>();
	}
	void Start()
	{
		DOTween.Sequence()
			.Append(GetComponent<CanvasGroup>().DOFade(1.0f, _audio[0].clip.length))
			.AppendCallback(() => _audio[1].Play());
	}

	void Update()
	{
		if ((GameManager.CurrentState == GameState.Won || GameManager.CurrentState == GameState.Lost) && _audio[1].volume > 0.0f)
		{
			_audio[1].volume -= Time.deltaTime;
		}
	}
}

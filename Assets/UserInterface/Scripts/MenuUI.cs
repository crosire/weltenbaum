using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup), typeof(AudioSource))]
public class MenuUI : MonoBehaviour
{
	#region Inspector Variables
	[SerializeField]
	CanvasGroup[] _pages;
	#endregion

	AudioSource _audio;

	void Awake()
	{
		_audio = GetComponent<AudioSource>();
	}
	void Start()
	{
		GetComponent<CanvasGroup>().DOFade(1.0f, 1.0f);
	}

	public void OnStartPressed()
	{
		DOTween.Sequence()
			.Insert(0, GetComponent<CanvasGroup>().DOFade(0.0f, 1.0f))
			.Insert(0, _audio.DOFade(0.0f, 2.0f))
			.OnComplete(() => GameManager.SwitchGameState(GameState.Running));
	}
	public void OnHelpPressed()
	{
		DOTween.Sequence()
			.Append(_pages[0].DOFade(0f, .5f))
			.Append(_pages[1].DOFade(1f, .5f));
	}
	public void OnAboutPressed()
	{
		DOTween.Sequence()
			.Append(_pages[1].DOFade(0f, .5f))
			.Append(_pages[0].DOFade(1f, .5f));
	}
	public void OnExitPressed()
	{
		Application.Quit();
	}
}

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class Interface : MonoBehaviour
{
	[Header("Menu")]
	public CanvasGroup _menu;
	public Button[] _buttons;
	public CanvasGroup[] _pages;

	bool[] _isActive = new bool[2];
	

	[Header("Interface")]
	public CanvasGroup _interface;

	void Start()
	{
		
	}

	void Update()
	{
	
	}

	void setPageActive()
	{

		

	}

	#region Buttons

	public void OnStartPressed()
	{
		Sequence sequence = DOTween.Sequence();
		sequence
			.Append(_menu.DOFade(0f, 1f))
			.Append(_interface.DOFade(1f, .5f));
		GameState.Running;
	}

	public void OnHelpPressed()
	{
		Sequence sequence = DOTween.Sequence();
		sequence
			.Append(_pages[0].DOFade(0f, .5f))
			.Append(_pages[1].DOFade(1f, .5f));
	}

	public void OnAboutPressed()
	{
		Sequence sequence = DOTween.Sequence();
		sequence
			.Append(_pages[1].DOFade(0f, .5f))
			.Append(_pages[0].DOFade(1f, .5f));
	}

	public void OnExitPressed()
	{
		Application.Quit();
	}

	#endregion
}

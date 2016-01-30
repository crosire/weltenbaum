using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuUI : MonoBehaviour
{
	public CanvasGroup _menu;
	public Button[] _buttons;
	public CanvasGroup[] _pages;

	#region Buttons

	public void OnStartPressed()
	{
		_menu.DOFade(0f, 1f).OnComplete(() => GameManager.SwitchGameState(GameState.Running));
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

	#endregion
}

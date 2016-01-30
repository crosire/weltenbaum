using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IngameUI : MonoBehaviour
{
	public CanvasGroup _interface;

	void Start()
	{
		_interface.DOFade(1f, .5f);
	}
}

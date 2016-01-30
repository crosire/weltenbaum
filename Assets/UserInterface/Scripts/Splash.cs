using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class Splash : MonoBehaviour
{
	public CanvasGroup _guy, _description;
	Sequence _seq;

	void Start()
	{
		_seq = DOTween.Sequence();
		_seq
			.AppendInterval(3f)
			.Insert(0, _guy.DOFade(1f, 5f))
			.Insert(0, _description.DOFade(1f, 1f))
			.AppendInterval(12f)
			.Append(_guy.DOFade(0f, 5f))
			.Append(_description.DOFade(0f, 1f))
			.Append(this.GetComponent<CanvasGroup>().DOFade(0f, 1f)).OnComplete(() => Destroy(this.gameObject));
	}

	void Update()
	{
	}
}

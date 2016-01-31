using UnityEngine;

public class ScoreUI : MonoBehaviour
{
	public void OnContinueClick()
	{
		GameManager.SwitchGameState(GameState.Menu);
	}
}

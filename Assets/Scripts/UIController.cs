using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
	[SerializeField]
	private	GameObject		resultPanel;
	[SerializeField]
	private	TextMeshProUGUI	textPlaytime;
	[SerializeField]
	private	TextMeshProUGUI	textMoveCount;
	[SerializeField]
	private	Board			board;
	
	//Muestra el panel final y muestra el numero de movimientos y el tiempo transcurrido
	public void OnResultPanel()
	{
		resultPanel.SetActive(true);

		textPlaytime.text = $"PLAY TIME : {board.Playtime/60:D2}:{board.Playtime%60:D2}";
		textMoveCount.text = "MOVE COUNT : "+board.MoveCount;
	}

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Tile : MonoBehaviour, IPointerClickHandler
{
	private	TextMeshProUGUI	textNumeric;
	private	Board board;
	public	Vector3	correctPosition { private set; get; }

	public	bool IsCorrected {private set; get;} = false;

	private	int	numeric;
	
	//le da el valor numerico a todos los hijos y elimina el último
	public void Setup(Board board, int hideNumeric, int numeric)
	{
		this.board = board;
		textNumeric = GetComponentInChildren<TextMeshProUGUI>();

		this.numeric = numeric;
        textNumeric.text = numeric.ToString();
        if ( this.numeric == hideNumeric )
		{
			GetComponent<UnityEngine.UI.Image>().enabled = false;
			textNumeric.enabled = false;
		}
	}
	//posiciona a los tiles en orden
	public void SetCorrectPosition()
	{
		correctPosition = GetComponent<RectTransform>().localPosition;
	}

	//comprueba en que tile estas haciendo click
	public void OnPointerClick(PointerEventData eventData)
	{
		board.IsMoveTile(this);
	}

	//inicia la corrutina de mover la ficha
	public void OnMoveTo(Vector3 end)
	{
		StartCoroutine("MoveTo", end);
	}

	//corrutina que hace que se mueva la ficha seleccionada y comprueba si la posicion es correcta y cuantas estan correctas
	private IEnumerator MoveTo(Vector3 end)
	{
		float	current  = 0;
		float	percent  = 0;
		float	moveTime = 0.1f;
		Vector3	start	 = GetComponent<RectTransform>().localPosition;

		while ( percent < 1 )
		{
			current += Time.deltaTime;
			percent = current / moveTime;

			GetComponent<RectTransform>().localPosition = Vector3.Lerp(start, end, percent);

			yield return null;
		}

		

		if(correctPosition == GetComponent<RectTransform>().localPosition)
			IsCorrected= true;
		else
            IsCorrected = false;


        board.IsGameOver();
		yield return null;
	}
}


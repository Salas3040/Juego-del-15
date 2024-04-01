using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
	[SerializeField] private GameObject	tilePrefab;								
	[SerializeField] private Transform	tilesParent;							

	private	List<Tile>	tileList;								

	private	Vector2Int	puzzleSize = new Vector2Int(4, 4);		
	private	float neighborTileDistance = 102;				

	public	Vector3	EmptyTilePosition { set; get; }			
	public	int	Playtime { private set; get; } = 0;		
	public	int	MoveCount { private set; get; } = 0;	

	//se inicia la escena con una corrutina en orden
	private IEnumerator Start()
	{
		tileList = new List<Tile>();

		SpawnTiles();

		LayoutRebuilder.ForceRebuildLayoutImmediate(tilesParent.GetComponent<RectTransform>());

		
		yield return new WaitForEndOfFrame();

		
		tileList.ForEach(x => x.SetCorrectPosition());

		StartCoroutine("OnSuffle");
		
		StartCoroutine("CalculatePlaytime");
	}

    private void Update()
    {
		//Truquitos de debug para ver si funciona sin completar el puzle (pereza)
		if (Input.GetKeyDown(KeyCode.S))		
            tileList.ForEach(x => x.OnMoveTo(x.correctPosition));
			
       
    }

    //crea los tiles en orden y los guarda en la lista
    private void SpawnTiles()
	{
		for ( int y = 0; y < puzzleSize.y; ++ y )
		{
			for ( int x = 0; x < puzzleSize.x; ++ x )
			{
				GameObject clone = Instantiate(tilePrefab, tilesParent);
				Tile tile = clone.GetComponent<Tile>();

				tile.Setup(this, puzzleSize.x * puzzleSize.y, y * puzzleSize.x + x + 1);

				tileList.Add(tile);
			}
		}
	}

	//mezcla los tiles
	private IEnumerator OnSuffle()
	{
		float current	= 0;
		float percent	= 0;
		float time		= 1.5f;

		while ( percent < 1 )
		{
			current += Time.deltaTime;
			percent = current / time;

			int index = Random.Range(0, puzzleSize.x * puzzleSize.y);
			tileList[index].transform.SetAsLastSibling();

			yield return null;
		}		
		EmptyTilePosition = tileList[tileList.Count-1].GetComponent<RectTransform>().localPosition;
	}

	//Comprueba si la ficha seleccionada colinda con el hueco vacio y las intercambia y suma un movimiento
	public void IsMoveTile(Tile tile)
	{
		if ( Vector3.Distance(EmptyTilePosition, tile.GetComponent<RectTransform>().localPosition) == neighborTileDistance)
		{
			Vector3 goalPosition = EmptyTilePosition;

			EmptyTilePosition = tile.GetComponent<RectTransform>().localPosition;

			tile.OnMoveTo(goalPosition);			
			MoveCount ++;
		}
	}

	//si todos los tiles estan en orden correcto, se activa el panel de final de juego
	public void IsGameOver()
	{
		List<Tile> tiles = tileList.FindAll(x => x.IsCorrected == true);

		Debug.Log("Correct Count : "+tiles.Count);
		if ( tiles.Count == puzzleSize.x * puzzleSize.y - 1 )
		{
			Debug.Log("GameClear");
			
			StopCoroutine("CalculatePlaytime");			
			GetComponent<UIController>().OnResultPanel();
		}
	}

	//cuenta los segundos desde que se inicia la partida
	private IEnumerator CalculatePlaytime()
	{
		while ( true )
		{
			Playtime ++;

			yield return new WaitForSeconds(1);
		}
	}
}

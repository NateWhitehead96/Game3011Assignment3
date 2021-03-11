﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {
	private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
	private static Tile previousSelected = null;

	private SpriteRenderer render;
	private bool isSelected = false;

	private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

	public bool matchFound = false;
	void Awake() {
		render = GetComponent<SpriteRenderer>();
    }

	private void Select() {
		isSelected = true;
		render.color = selectedColor;
		previousSelected = gameObject.GetComponent<Tile>();
	}

	private void Deselect() {
		isSelected = false;
		render.color = Color.white;
		previousSelected = null;
	}
	private void OnMouseDown()
	{
		//print("in mouse down");
		if (render.sprite == null || BoardManager.Instance.isShifting)
		{
			return;
		}
		if (isSelected)
		{
			Deselect();
		}
		else
		{
			if (previousSelected == null)
			{
				Select();
				//print("Selecting");
			}
			else
			{
				//SwapSprite(previousSelected.render);
				//previousSelected.Deselect();
				if (GetAllAdjacent().Contains(previousSelected.gameObject))
				{
					SwapSprite(previousSelected.render);
					previousSelected.ClearAllMatches();
					previousSelected.Deselect();
					ClearAllMatches();
				}
				else
				{
					
					previousSelected.GetComponent<Tile>().Deselect();
					Select();
				}
			}
		}
	}

	public void SwapSprite(SpriteRenderer renderSwap)
	{
		if (render.sprite == renderSwap)
		{
			return;
		}

		Sprite tempSprite = renderSwap.sprite;
		renderSwap.sprite = render.sprite;
		render.sprite = tempSprite;
	}

	private GameObject GetAdjacent(Vector2 castDirection)
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, castDirection);
		if (hit.collider != null)
		{
			Debug.DrawRay(transform.position, castDirection, Color.red);
			return hit.collider.gameObject;
		}
		else
			return null;
	}

	private List<GameObject> GetAllAdjacent()
	{
		List<GameObject> adjacentTiles = new List<GameObject>();
		for (int i = 0; i < adjacentDirections.Length; i++)
		{
			adjacentTiles.Add(GetAdjacent(adjacentDirections[i]));
		}
		//adjTiles = adjacentTiles;
		return adjacentTiles;
	}
	private List<GameObject> FindMatch(Vector2 castDir)
	{
		List<GameObject> matchingTiles = new List<GameObject>();
		RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
		while (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite)
		{
			matchingTiles.Add(hit.collider.gameObject);
			hit = Physics2D.Raycast(hit.collider.transform.position, castDir);
		}
		return matchingTiles;
	}

	private void ClearMatch(Vector2[] paths)
	{
		List<GameObject> matchingTiles = new List<GameObject>();
		for (int i = 0; i < paths.Length; i++)
		{
			matchingTiles.AddRange(FindMatch(paths[i]));
		}
		if (matchingTiles.Count >= 2)
		{
			for (int i = 0; i < matchingTiles.Count; i++)
			{
				matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
			}
			matchFound = true;
		}
	}

	public void ClearAllMatches()
	{
		if (render.sprite == null)
			return;

		ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
		ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });
		if (matchFound)
		{
			render.sprite = null;
			matchFound = false;
			StopCoroutine(BoardManager.Instance.FindEmptyTiles());
			StartCoroutine(BoardManager.Instance.FindEmptyTiles());
			//BoardManager.Instance.StartCoroutines();
		}
	}
}
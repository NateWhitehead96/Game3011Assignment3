using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Sprite[] enemies;
    private SpriteRenderer render;
    // Start is called before the first frame update
    void Start() // depending on difficulty spawn a different enemy
    {
        render = GetComponent<SpriteRenderer>();
        if(BoardManager.difficulty == 1)
        {
            render.sprite = enemies[0];
        }
        if (BoardManager.difficulty == 2)
        {
            render.sprite = enemies[1];
        }
        if (BoardManager.difficulty == 3)
        {
            render.sprite = enemies[2];
        }
    }
}

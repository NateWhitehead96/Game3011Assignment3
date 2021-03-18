using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    public List<Sprite> items = new List<Sprite>();
    public GameObject tile;
    public int xSize;
    public int ySize;

    public GameObject[,] grid;

    public bool isShifting { get; set; }

    public int turnsLeft;
    public float score;

    public static int difficulty = 1; // Difficulty will determine # of block sprites. 1 meaning none. 2 meaning some will spawn after you starts clearing. 3 meaning some start and spawn during play

    public GameObject projectile;
    public Transform projTransform;

    // Start is called before the first frame update
    void Start()
    {
        Instance = GetComponent<BoardManager>();
        turnsLeft = 10 * difficulty + 10; // give player more turns based on difficulty
        Vector2 offset = tile.GetComponent<SpriteRenderer>().bounds.size;
        CreateBoard(offset.x, offset.y);
        score = FindObjectOfType<UIScript>().PointSlider.maxValue;
    }

    public void HideBoard()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                grid[x, y].SetActive(false);
            }
        }
    }
    public void ShowBoard()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                grid[x, y].SetActive(true);
            }
        }
    }

    private void CreateBoard(float offsetX, float offsetY)
    {
        grid = new GameObject[xSize, ySize];

        float startX = transform.position.x;
        float startY = transform.position.y;

        Sprite[] previousLeft = new Sprite[ySize];
        Sprite previousBelow = null;

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                GameObject newTile = Instantiate(tile, new Vector3(startX + (offsetX * x), startY + (offsetY * y), 0), Quaternion.identity);
                grid[x, y] = newTile;
                newTile.transform.parent = transform;
                // to make sure there are no repeating sprites
                List<Sprite> possibleItems = new List<Sprite>();
                possibleItems.AddRange(items);
                possibleItems.Remove(previousLeft[y]);
                possibleItems.Remove(previousBelow);

                if(difficulty < 3)
                {
                    Sprite newSprite = possibleItems[Random.Range(0, possibleItems.Count - 1)];
                    newTile.GetComponent<SpriteRenderer>().sprite = newSprite;
                    previousLeft[y] = newSprite;
                    previousBelow = newSprite;
                }
                else
                {
                    Sprite newSprite = possibleItems[Random.Range(0, possibleItems.Count)];
                    newTile.GetComponent<SpriteRenderer>().sprite = newSprite;
                    previousLeft[y] = newSprite;
                    previousBelow = newSprite;
                }
                
            }
        }
    }

    public void StartCoroutines()
    {
        StartCoroutine(FindEmptyTiles());
    }

    public IEnumerator FindEmptyTiles()
    {
        print("in empty");

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                if(grid[x, y].GetComponent<SpriteRenderer>().sprite == null)
                {
                    print("some empty tiles");
                    Instantiate(projectile, projTransform);
                    yield return StartCoroutine(ShiftTilesDown(x, y));
                    
                    break;
                }
            }
        }
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                grid[x, y].GetComponent<Tile>().ClearAllMatches();
            }
        }
    }

    public IEnumerator ShiftTilesDown(int x, int yStart)
    {
        //int thisY = y;
        float shiftDelay = 0.3f;
        print("in shifting");
        isShifting = true;
        List<SpriteRenderer> renders = new List<SpriteRenderer>();
        int nullCount = 0;

        for (int y = yStart; y < ySize; y++)
        {
            SpriteRenderer render = grid[x, y].GetComponent<SpriteRenderer>();
            if(render.sprite == null)
            {
                nullCount++;
            }
            renders.Add(render);
        }
        for (int i = 0; i < nullCount; i++)
        { 
            yield return new WaitForSeconds(shiftDelay);
            for (int k = 0; k < renders.Count - 1; k++)
            { 
                renders[k].sprite = renders[k + 1].sprite;
                renders[k + 1].sprite = GetNewSprite(x, ySize - 1);
            }
        }
        isShifting = false;
    }

    private Sprite GetNewSprite(int x, int y)
    {
        List<Sprite> possibleItems = new List<Sprite>();
        possibleItems.AddRange(items);

        if(x > 0)
        {
            possibleItems.Remove(grid[x - 1, y].GetComponent<SpriteRenderer>().sprite);

        }
        if (x < xSize - 1)
        {
            possibleItems.Remove(grid[x + 1, y].GetComponent<SpriteRenderer>().sprite);
        }
        if (y > 0)
        {
            possibleItems.Remove(grid[x, y - 1].GetComponent<SpriteRenderer>().sprite);
        }
        if(difficulty > 1) // if we're not in easy allow block sprites to spawn
        {
            return possibleItems[Random.Range(0, possibleItems.Count)];
        }
        else // dont spawn blocks
            return possibleItems[Random.Range(0, possibleItems.Count - 1)];
    }

    // Update is called once per frame
    void Update()
    {
        if (!Manager.InGame)
        {
            // hide all the tiles and junk
            gameObject.SetActive(false);
        }
        else
            gameObject.SetActive(true);
    }
}

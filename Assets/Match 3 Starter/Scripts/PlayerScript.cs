using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Sprite[] heroes;
    private static SpriteRenderer render;

    public static int heroChoice;

    public Canvas heroPick;
    public Canvas UICanvas;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        if (Manager.InGame)
        {
            heroPick.gameObject.SetActive(false);
            UICanvas.gameObject.SetActive(true);
        }
        if (heroChoice == 0)
        {
            render.sprite = heroes[0];
        }
        if (heroChoice == 1)
        {
            render.sprite = heroes[1];
        }
        if (heroChoice == 2)
        {
            render.sprite = heroes[2];
        }
    }

    public void OnWarrior()
    {
        heroChoice = 0;
        render.sprite = heroes[0];
        Manager.InGame = true;
        BoardManager.Instance.gameObject.SetActive(true);
        heroPick.gameObject.SetActive(false);
        UICanvas.gameObject.SetActive(true);
    }

    public void OnRanger()
    {
        heroChoice = 1;
        render.sprite = heroes[1];
        Manager.InGame = true;
        BoardManager.Instance.gameObject.SetActive(true);
        heroPick.gameObject.SetActive(false);
        UICanvas.gameObject.SetActive(true);
    }

    public void OnMage()
    {
        heroChoice = 2;
        render.sprite = heroes[2];
        Manager.InGame = true;
        BoardManager.Instance.gameObject.SetActive(true);
        heroPick.gameObject.SetActive(false);
        UICanvas.gameObject.SetActive(true);
    }
}

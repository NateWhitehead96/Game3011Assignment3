using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Sprite[] type;
    private SpriteRenderer render;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        if(PlayerScript.heroChoice == 0)
        {
            render.sprite = type[0];
        }
        if (PlayerScript.heroChoice == 1)
        {
            render.sprite = type[1];
        }
        if (PlayerScript.heroChoice == 2)
        {
            render.sprite = type[2];
        }
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x - 1 * Time.deltaTime, transform.position.y, transform.position.z);
        if (transform.position.x < -0.7)
        {
            Destroy(gameObject);
        }
    }
}

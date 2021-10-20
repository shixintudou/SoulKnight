using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigYellowCrystal : EnemyBase
{
    // Start is called before the first frame update
    [SerializeField]
    Sprite sprite;
    SpriteRenderer spriteRenderer;
    Collider2D collider;
    void Start()
    {
        HP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
        state = State.Move;
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state!=State.Dead)
        {
            if(HP<=0)
            {
                int n = Random.Range(10, 15);
                for(int i=0;i<n;i++)
                {
                    GameObject obj = Instantiate(Resources.Load<GameObject>("Coin"));
                    obj.transform.position = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), transform.position.z);
                }
                collider.enabled = false;
                spriteRenderer.sprite = sprite;
                state = State.Dead;
            }
        }
    }
}

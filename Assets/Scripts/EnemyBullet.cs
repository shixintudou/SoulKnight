using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    public Vector2 movePosition;
    public float speed;
    private bool canmove;
    void Awake()
    {
        movePosition = new Vector2();
        canmove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canmove)
            transform.Translate(movePosition * speed * Time.deltaTime);
    }
    public void SetMovePosition(Vector2 vector)
    {
        movePosition = vector;
        canmove = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.GetDamage(damage);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Wall"))
            gameObject.SetActive(false);
            
    }
    
}

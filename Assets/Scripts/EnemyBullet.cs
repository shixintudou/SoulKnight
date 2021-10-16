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
    private void OnEnable()
    {
        StartCoroutine(SetFalseCoroutine());
    }
    // Update is called once per frame
    void Update()
    {
        if (canmove)
        {
            transform.Translate(movePosition * speed * Time.deltaTime);
            int num;
            Collider2D[] Contact = new Collider2D[10];
            ContactFilter2D ContactF2D = new ContactFilter2D();
            ContactF2D.NoFilter();
            num = GetComponent<Collider2D>().OverlapCollider(ContactF2D, Contact);
            for (int i = 0; i < num; i++)
            {
                if (Contact[i].CompareTag("Player"))
                {
                    PlayerController.Instance.GetDamage(damage);
                    gameObject.SetActive(false);
                }
                else if (Contact[i].CompareTag("Wall"))
                    gameObject.SetActive(false);
            }
           
        }
            
    }
    public void SetMovePosition(Vector2 vector)
    {
        movePosition = vector;
        canmove = true;
    }
    IEnumerator SetFalseCoroutine()
    {
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        PlayerController.Instance.GetDamage(damage);
    //        gameObject.SetActive(false);
    //    }
    //    else if (collision.gameObject.CompareTag("Wall"))
    //        gameObject.SetActive(false);
            
    //}
    
}

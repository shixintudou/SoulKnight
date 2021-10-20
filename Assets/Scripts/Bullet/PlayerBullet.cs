using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    public Vector2 MovePosition;
    public float speed = 3f;
    private void Start()
    {
        StartCoroutine(SetFalseCoroutine());
        Vector2 vector = new Vector2(transform.position.x, transform.position.y);
        MovePosition = (PlayerController.Instance.GetMousePosition() - vector).normalized;       
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(MovePosition * speed * Time.deltaTime);
        int num;
        Collider2D[] Contact = new Collider2D[10];
        ContactFilter2D ContactF2D = new ContactFilter2D();
        ContactF2D.NoFilter();
        num = GetComponent<Collider2D>().OverlapCollider(ContactF2D, Contact);
        for(int i=0;i<num;i++)
        {
            if(Contact[i].CompareTag("Wall"))
            {
                gameObject.SetActive(false);
            }
            else
            {
#nullable enable
                EnemyBase? enemyBase = Contact[i].GetComponent<EnemyBase>();
#nullable disable
                if(enemyBase!=null)
                {
                    enemyBase.Hurt(damage);
                    gameObject.SetActive(false);
                }
            }
        }
    }
    IEnumerator SetFalseCoroutine()
    {
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
    }
    
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    collision.gameObject.GetComponent<EnemyBase>()?.Hurt(damage);


    //    gameObject.SetActive(false);
    //}
}

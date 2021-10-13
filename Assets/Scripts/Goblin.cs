using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Goblin : EnemyBase
{
    public Collider2D attackRange;
    //private bool attackto;
    // Start is called before the first frame update
    void Start()
    {       
        state = State.Move;
        //attackto = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Move:
                Move();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Dead:
                Dead();
                break;
        }
        if (HP <= 0)
            ChangeToDead();
        
    }
    public override void ChangeToMove()
    {
        base.ChangeToMove();
        StopAllCoroutines();
        StartCoroutine(MoveCoroutine());
    }
    public override void ChangeToAttack()
    {
        base.ChangeToAttack();
        StopAllCoroutines();
        StartCoroutine(AttackCoroutine());
    }
    public override void Attack()
    {
        Vector3 vector = transform.position - PlayerController.Instance.transform.position;
        transform.position -= vector * speed * Time.deltaTime;



        if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) >= attacDistance)
            ChangeToMove();
    }
    public override void Move()
    {
        transform.position += movePosition * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < attacDistance)
            ChangeToAttack();
    }
    IEnumerator MoveCoroutine()
    {
        while(state==State.Move)
        {
            System.Random random = new System.Random();
            movePosition = new Vector3(random.Next() * UnityEngine.Random.Range(-1, 2), random.Next() * UnityEngine.Random.Range(-1, 2), transform.position.z);
            movePosition.Normalize();
            yield return new WaitForSeconds(idleTime);
        }
    }
    IEnumerator AttackCoroutine()
    {
        while(state==State.Attack)
        {
            int num;
            Collider2D[] Contact = new Collider2D[10];
            ContactFilter2D ContactF2D = new ContactFilter2D();
            ContactF2D.NoFilter();
            num = attackRange.OverlapCollider(ContactF2D, Contact);
            for(int i=0;i<num;i++)
            {
                if(Contact[i].CompareTag("Player"))
                {
                    PlayerController.Instance.GetDamage(attackNumber);
                }
                print("attack");
            }
            yield return new WaitForSeconds(attackCd);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            movePosition *= -1;
    }
    
}

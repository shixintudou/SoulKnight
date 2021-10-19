using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Goblin : EnemyBase
{
    public Collider2D attackRange;
    //private bool attackto;
    // Start is called before the first frame update
    private void OnEnable()
    {
        state = State.Move;
        HP = maxHP;
    }
    void Start()
    {       
        state = State.Move;
        HP = maxHP;
        //attackto = false;
        StartCoroutine(MoveCoroutine());
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
        if (movePosition.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (movePosition.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
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
        transform.position -= vector.normalized * speed * Time.deltaTime;
        RaycastHit2D raycastHit;
        Vector2 castposition = transform.position - PlayerController.Instance.transform.position;
        castposition.Normalize();
        Vector2 origin = new Vector2(transform.position.x, transform.position.y) + castposition;
        raycastHit = Physics2D.Raycast(origin, castposition, avoidDistance);

        if (raycastHit.collider?.CompareTag("PlayerBullet") == true)
        {
            switch (UnityEngine.Random.Range(0, 2))
            {
                case 0:
                    castposition = Quaternion.Euler(90, 0, 0) * castposition;
                    break;
                case 1:
                    castposition = Quaternion.Euler(-90, 0, 0) * castposition;
                    break;
            }
            castposition.Normalize();
            StopCoroutine(AvoidCoroutine(castposition, avoidTime));
            StartCoroutine(AvoidCoroutine(castposition, avoidTime));
        }


        if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) >= attackDistance)
            ChangeToMove();
    }
    public override void Move()
    {
        transform.position += movePosition * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < attackDistance)
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
    IEnumerator AvoidCoroutine(Vector2 avoidTarget, float time)
    {
        while (avoidTime > 0)
        {
            transform.Translate(avoidTarget * speed * Time.deltaTime);
            avoidTime -= Time.deltaTime;
            yield return null;
        }
        avoidTime = time;
    }
#nullable enable
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
                //print("attack");
            }
            yield return new WaitForSeconds(attackCd);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            movePosition *= -1;
    }
    public override void Dead()
    {
        base.Dead();
    }
}

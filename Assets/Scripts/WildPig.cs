using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildPig : EnemyBase
{
    // Start is called before the first frame update
    public float attackspeed;
    public Collider2D AttackRange;
    void Start()
    {
        state = State.Move;
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
            case State.AfterAttack:
                AfterAttack();
                break;
        }
        if (HP <= 0)
            ChangeToDead();
        if (movePosition.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (movePosition.x < 0)
            transform.localScale = new Vector3(-1, 1, 1 );
    }
    public override void Move()
    {
        transform.position += movePosition * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < attackDistance)
            ChangeToAttack();
    }
    public override void Attack()
    {
        if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) >= attackDistance)
            ChangeToMove();

        



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
            StopCoroutine(AvoidCoroutine(castposition, avoidTime));
            StartCoroutine(AvoidCoroutine(castposition, avoidTime));
        }

    }
    public override void AfterAttack()
    {
        transform.position += movePosition * speed * Time.deltaTime;
    }
    public override void ChangeToAfterAttack()
    {
        base.ChangeToAfterAttack();
        StartCoroutine(QuitAfterAttackCoroutine());
    }
    public override void ChangeToMove()
    {

        base.ChangeToMove();
        StartCoroutine(MoveCoroutine());
    }
    public override void ChangeToAttack()
    {
        base.ChangeToAttack();
        StopCoroutine(MoveCoroutine());
        StartCoroutine(AttackCoroutine());
    }
    IEnumerator MoveCoroutine()
    {
        while (state == State.Move)
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
    IEnumerator AttackCoroutine()
    {
        //StopCoroutine(IdleCoroutine(attackCd));
        while (state == State.Attack)
        {
            Vector3 vector = transform.position - PlayerController.Instance.transform.position;
            vector.Normalize();
            transform.position -= vector.normalized * attackspeed * Time.deltaTime;
            int num;
            Collider2D[] Contact = new Collider2D[10];
            ContactFilter2D ContactF2D = new ContactFilter2D();
            ContactF2D.NoFilter();
            num = AttackRange.OverlapCollider(ContactF2D, Contact);
            for (int i = 0; i < num; i++)
            {
                if (Contact[i].CompareTag("Player"))
                {
                    PlayerController.Instance.GetDamage(attackNumber);
                    ChangeToAfterAttack();
                }
                print("attack");
            }
            yield return null;
        }
    }
    
    IEnumerator QuitAfterAttackCoroutine()
    {
        yield return new WaitForSeconds(attackCd);
        ChangeToMove();
    }
    //IEnumerator IdleCoroutine(float AttacCd)
    //{
    //    StopCoroutine(AttackCoroutine());
    //    while(attackCd>0)
    //    {
    //        transform.position += movePosition * speed * Time.deltaTime;
    //        attackCd -= Time.deltaTime;
    //        yield return null;

    //    }
    //    attackCd = AttacCd;
    //    StartCoroutine(AttackCoroutine());
    //}
}

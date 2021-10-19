using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinWitch : EnemyBase
{
    // Start is called before the first frame update
    public float safeDistance;
    public int BulletNumber;
    GameObject[] Bullets;
    public float bulletSpeed;
    private void OnEnable()
    {
        state = State.Move;
        HP = maxHP;
    }
    void Start()
    {
        Bullets = new GameObject[BulletNumber];
        state = State.Move;
        HP = maxHP;
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
    public override void Move()
    {
        base.Move();
        transform.position += movePosition * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < attackDistance)
            ChangeToAttack();
    }

    public override void Attack()
    {
        float distance;
        Vector2 attackposition = PlayerController.Instance.transform.position - transform.position;
        distance = attackposition.magnitude;
        attackposition.Normalize();
        if (distance > safeDistance)
        {
            transform.Translate(attackposition * speed * Time.deltaTime * 0.3f);
        }
        else
        {
            transform.Translate(-attackposition * speed * Time.deltaTime * 0.3f);
        }



        //for (int i = 0; i < BulletNumber; i++)
        //{
        //    Bullets[i] = BubblePool.Instance.GetBubble("bullet_goblinwitch(Clone)");
        //    Bullets[i].transform.position = transform.position + new Vector3(attackposition.x, attackposition.y, transform.position.z);

        //}
        //StartCoroutine(AttackCoroutine());
        if (distance > attackDistance)
            ChangeToMove();


        RaycastHit2D raycastHit;
        Vector2 castposition = transform.position - PlayerController.Instance.transform.position;
        castposition.Normalize();
        Vector2 origin = new Vector2(transform.position.x, transform.position.y) + castposition;
        raycastHit = Physics2D.Raycast(origin, castposition, avoidDistance);

        if (raycastHit.collider?.CompareTag("PlayerBullet") == true)
        {
            switch (Random.Range(0, 2))
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


    }
    public override void Dead()
    {
        base.Dead();
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
        StartCoroutine(ShootCoroutine());

    }
    IEnumerator MoveCoroutine()
    {
        while (state == State.Move)
        {
            System.Random random = new System.Random();
            movePosition = new Vector3(random.Next() * Random.Range(-1, 2), random.Next() * Random.Range(-1, 2), transform.position.z);
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
    //IEnumerator AttackCoroutine()
    //{

    //    for (int i = 0; i < BulletNumber; i++)
    //    {
    //        Vector2 attackposition = PlayerController.Instance.transform.position - transform.position;
    //        Bullets[i].SetActive(true);
    //        Bullets[i].transform.Translate(attackposition.normalized * speed * Time.deltaTime * 5);
    //        yield return new WaitForSeconds(attackCd / 5);
    //    }
    //    bool book = true;
    //    while (book)
    //    {

    //        for (int i = 0; i < BulletNumber; i++)
    //        {
    //            Vector2 attackposition = PlayerController.Instance.transform.position - transform.position;
    //            Bullets[i].transform.Translate(attackposition.normalized * speed * Time.deltaTime);
    //            yield return null;
    //        }


    //        foreach (var i in Bullets)
    //        {
    //            if (i.activeInHierarchy)
    //            {
    //                book = true;
    //                break;
    //            }
    //            else
    //                book = false;
    //        }
    //    }
    //}
    IEnumerator ShootCoroutine()
    {
        while(state==State.Attack)
        {
            Vector2 attackposition = PlayerController.Instance.transform.position - transform.position;
            attackposition.Normalize();
            for (int i = 0; i < BulletNumber; i++)
            {
                Bullets[i] = BubblePool.Instance.GetBubble("bullet_goblinwitch(Clone)");
                Bullets[i].transform.position = transform.position + new Vector3(attackposition.x, attackposition.y, transform.position.z);
                Bullets[i].SetActive(true);
                Bullets[i].GetComponent<EnemyBullet>()?.SetMovePosition(attackposition);
                yield return new WaitForSeconds(attackCd / 10);

            }
            yield return new WaitForSeconds(attackCd);
        }
    }
    
}

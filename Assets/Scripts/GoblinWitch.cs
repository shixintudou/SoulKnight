using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinWitch : EnemyBase
{
    // Start is called before the first frame update
    public float safeDistance;
    void Start()
    {
        state = State.Move;
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
        if(distance>safeDistance)
        {
            transform.Translate(attackposition * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(-attackposition * speed * Time.deltaTime);
        }
        
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
            StopAllCoroutines();
            StartCoroutine(AvoidCoroutine(castposition, avoidTime));
        }


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
    IEnumerator AvoidCoroutine(Vector2 avoidTarget,float time)
    {       
        while(avoidTime>0)
        {
            transform.Translate(avoidTarget * speed * Time.deltaTime);
            avoidTime -= Time.deltaTime;
            yield return null;
        }
        avoidTime = time;
    }
    //IEnumerator AttackCoroutine()
    //{

    //}
}

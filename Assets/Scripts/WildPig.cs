using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildPig : EnemyBase
{
    // Start is called before the first frame update
    public float attackspeed;
    void Start()
    {
        
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
    }
    public override void ChangeToMove()
    {

        base.ChangeToMove();
        StartCoroutine(MoveCoroutine());
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
    IEnumerator AttackCoroutine()
    {

    }
}

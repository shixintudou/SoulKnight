using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Goblin : EnemyBase
{
    
    // Start is called before the first frame update
    void Start()
    {       
        state = State.Move;        
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
        if (state != State.Move && HP > 0 && Vector3.Distance(transform.position, PlayerController.Instance.transform.position) >= attacDistance)
            ChangeToMove();
        if (state != State.Attack && HP > 0 && Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < attacDistance)
            ChangeToAttack();
    }
    public override void ChangeToMove()
    {
        base.ChangeToMove();
        StartCoroutine(MoveCoroutine());
    }
    public override void Attack()
    {
        
    }
    public override void Move()
    {
        transform.position += movePosition * speed * Time.deltaTime;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            movePosition *= -1;
    }
}

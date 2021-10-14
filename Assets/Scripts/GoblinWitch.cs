using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinWitch : EnemyBase
{
    // Start is called before the first frame update
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
}

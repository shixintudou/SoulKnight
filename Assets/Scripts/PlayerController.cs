using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum State
    {
        Idle,
        Move,
        Attack,
        Hurt,
        Dead,
    }
    enum WeaponState
    {
        Close,
        Far,
        Hand,
    }
    enum SkillState
    {
        SkillOn,
        SkillCanUse,
        SkillNotPrepared,
    }

    State state;
    WeaponState weaponState;
    SkillState skillState;
    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
        weaponState = WeaponState.Far;
        skillState = SkillState.SkillCanUse;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Move:
                Move();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Hurt:
                Hurt();
                break;
            case State.Dead:
                Dead();
                break;
        }
    }
    public void Idle()
    {

    }
    public void Move()
    {
        
    }
    public void Attack()
    {

    }
    public void Hurt()
    {

    }
    public void Dead()
    {

    }
}

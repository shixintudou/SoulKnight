using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public enum State
    { 
    Move,Attack,Dead
    }
    public int HP;
    public int attackNumber;
    public State state;
    public float attackCd;
    public float idleTime;
    public float attacDistance;
    public float speed;
    public Vector3 movePosition;
    // Start is called before the first frame update
    
    
    
    public virtual void Move()
    {
        
    }
    public virtual void Attack()
    {

    }
    public virtual void Dead()
    {
        gameObject.SetActive(false);
    }
    public void Hurt(int damage)
    {
        HP -= damage;
    }
    public virtual void ChangeToMove()
    {
        state = State.Move;
    }
    public  virtual void ChangeToAttack()
    {
        state = State.Attack;
    }
    public virtual void ChangeToDead()
    {
        state = State.Dead;
    }
}

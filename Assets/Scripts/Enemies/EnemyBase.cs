using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public enum State
    { 
    Move,Attack,Dead,AfterAttack
    }
    public int HP;
    public int maxHP;
    public int attackNumber;
    public State state;
    public float attackCd;
    public float idleTime;
    public float attackDistance;
    public float speed;
    public Vector3 movePosition;
    public float avoidDistance;
    public float avoidTime;
    // Start is called before the first frame update
    
    
    
    public virtual void Move()
    {
        
    }
    public virtual void Attack()
    {

    }
    public virtual void AfterAttack()
    {

    }
    public virtual void Dead()
    {
        int n = Random.Range(0, 3);
        switch (n)
        {
            case 0:
                break;
            case 1:
                GameObject coin = Instantiate(Resources.Load<GameObject>("Coin"));
                coin.transform.position = transform.position;
                break;
            case 2:
                GameObject blue = Instantiate(Resources.Load<GameObject>("BlueCrystal"));
                blue.transform.position = transform.position;
                break;
        }
        ChangeToMove();
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
    public virtual void ChangeToAfterAttack()
    {
        state = State.AfterAttack;
    }
    public void Attract(float attracttime, float attractspeed, Vector2 attracttarget)
    {
        StartCoroutine(AttractCoroutine(attracttime, attractspeed, attracttarget));
    }

    public IEnumerator AttractCoroutine(float attracttime, float attractspeed,Vector2 attracttarget)
    {       
        float time = attracttime;
        while(time>0)
        {
            Vector2 vector = Vector2.Lerp(transform.position, attracttarget, attractspeed * Time.deltaTime);
            Vector3 vector3 = new Vector3(vector.x, vector.y, transform.position.z);
            transform.position = vector3;
            time -= Time.deltaTime;
            yield return null;
        }
    }
}

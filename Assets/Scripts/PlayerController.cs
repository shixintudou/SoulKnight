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
    public static PlayerController Instance;
    public float speed;
    public Collider2D HandRange;
    public Collider2D CloseWeaponRange;
    public Collider2D FarWeaponRange;
    public float quittime;
    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
        weaponState = WeaponState.Far;
        skillState = SkillState.SkillCanUse;
        Instance = this;
        HandRange = GameObject.FindGameObjectWithTag("HandRange").GetComponent<Collider2D>();
        CloseWeaponRange = GameObject.FindGameObjectWithTag("CloseWeaponRange").GetComponent<Collider2D>();
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
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            ChangeToMove();
        if (Input.GetMouseButtonDown(0))
            ChangeToAttack();
    }
    public void Move()
    {

        transform.Translate(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed * Time.deltaTime);
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            ChangeToIdle();
        if (Input.GetMouseButtonDown(0))
            ChangeToAttack();
    }
    public void Attack()
    {
        transform.Translate(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed * Time.deltaTime);
        int num;
        Collider2D[] Contact = new Collider2D[10];
        ContactFilter2D ContactF2D = new ContactFilter2D();
        ContactF2D.NoFilter();

        switch (weaponState)
        {
            case WeaponState.Hand:
                num = HandRange.OverlapCollider(ContactF2D, Contact);
                for (int i = 0; i < num; i++)
                {
                    //Contact[i].GetComponent<Enemy>()?.Hurt(basedamage);
                }
                break;
            case WeaponState.Close:
                num = CloseWeaponRange.OverlapCollider(ContactF2D, Contact);
                for (int i = 0; i < num; i++)
                {
                    //Contact[i].GetComponent<Enemy>()?.Hurt(basedamage);
                }
                break;
            case WeaponState.Far:
                num = FarWeaponRange.OverlapCollider(ContactF2D, Contact);
                for (int i = 0; i < num; i++)
                {
                    //Contact[i].GetComponent<Enemy>()?.Hurt(basedamage);
                }
                break;
        }
        if (!Input.GetMouseButton(0))
            StartCoroutine(QuitAttackQuaroutine());
        
    }
    public void Hurt()
    {

    }
    public void Dead()
    {

    }
    public void ChangeToIdle()
    {
        state = State.Idle;
    }
    public void ChangeToMove()
    {
        state = State.Move;
    }
    public void ChangeToAttack()
    {
        state = State.Attack;
    }
    public void ChangeToHurt()
    {
        state = State.Hurt;
    }
    public void ChangeToDead()
    {
        state = State.Dead;
    }
    IEnumerator QuitAttackQuaroutine()
    {
        yield return new WaitForSeconds(quittime);
        ChangeToIdle();
    }
}

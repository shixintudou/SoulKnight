using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move,
        Attack,
        Hurt,
        Dead,
    }
    public enum WeaponState
    {
        Close,
        Far,
        Hand,
    }
    public enum SkillState
    {
        SkillOn,
        SkillCanUse,
        SkillNotPrepared,
    }

    public State state;
    public WeaponState weaponState;
    public SkillState skillState;
    public static PlayerController Instance;
    public float speed;
    public Collider2D HandRange;
    public Collider2D CloseWeaponRange;
    public Collider2D FarWeaponRange;
    public float quitattacktime;
    public float hurttime;
    public int HP;
    public int shield;
    public float shieldcovertime;
    public float everysheildcovertime;
    public int maxshield;
    public int maxHP;
    public float skilltime;
    public float skillcd;
    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
        weaponState = WeaponState.Far;
        skillState = SkillState.SkillCanUse;
        Instance = this;
        HandRange = GameObject.FindGameObjectWithTag("HandRange").GetComponent<Collider2D>();
        CloseWeaponRange = GameObject.FindGameObjectWithTag("CloseWeaponRange").GetComponent<Collider2D>();
        FarWeaponRange = GameObject.FindGameObjectWithTag("FarWeaponRange").GetComponent<Collider2D>();
        HP = maxHP;
        shield = maxshield;
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
        if (HP <= 0)
        {
            ChangeToDead();
        }
        if(shield<maxshield)
        {
            StartCoroutine(ShieldBeginCoverCoroutine());
        }
        if (skillState == SkillState.SkillOn)
            StartCoroutine(SkillOnCoroutine());
        if (skillState == SkillState.SkillNotPrepared)
            StartCoroutine(SkillCdCoroutine());
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
        if(Input.GetAxisRaw("Horizontal")!=0)
        {
            Vector3Int vector3Int = new Vector3Int((int)Input.GetAxisRaw("Horizontal"), 1, 1);
            transform.localScale = vector3Int;
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            ChangeToIdle();
        if (Input.GetMouseButtonDown(0))
            ChangeToAttack();
    }
    public void Attack()
    {
        transform.Translate(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed * Time.deltaTime);
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Vector3Int vector3Int = new Vector3Int((int)Input.GetAxisRaw("Horizontal"), 1, 1);
            transform.localScale = vector3Int;
        }
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
            StartCoroutine(QuitAttackCoroutine());
        
    }
    public void Hurt()
    {
        StartCoroutine(QuitHurtCoroutine());
        StopCoroutine(ShieldCoverCoroutine());
        StopCoroutine(ShieldBeginCoverCoroutine());
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
    IEnumerator QuitAttackCoroutine()
    {
        yield return new WaitForSeconds(quitattacktime);
        ChangeToIdle();
    }
    IEnumerator QuitHurtCoroutine()
    {
        yield return new WaitForSeconds(hurttime);
        ChangeToIdle();
    }
    IEnumerator ShieldBeginCoverCoroutine()
    {
        yield return new WaitForSeconds(shieldcovertime);
        StartCoroutine(ShieldCoverCoroutine());
    }
    IEnumerator ShieldCoverCoroutine()
    {        
       while(shield<maxshield)
        {
            shield++;
            yield return new WaitForSeconds(everysheildcovertime);
        }
    }
    IEnumerator SkillCdCoroutine()
    {
        yield return new WaitForSeconds(skillcd);
        skillState = SkillState.SkillCanUse;
    }
    IEnumerator SkillOnCoroutine()
    {
        yield return new WaitForSeconds(skilltime);
        skillState = SkillState.SkillNotPrepared;
    }
}

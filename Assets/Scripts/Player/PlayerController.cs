using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SkillCovering,
        SkillUsing
    }
    public enum ShieldState
    { 
    Full,NotFull,BeginRecover,Recovering,
    }



    public State state;
    public WeaponState weaponState;
    public SkillState skillState;
    public ShieldState shieldState;
    public static PlayerController Instance;
    public float speed;
    public Collider2D HandRange;
    public Collider2D CloseWeaponRange;
    public Collider2D FarWeaponRange;
    public float quitattacktime;
    public float hurttime;
    public int HP;
    public int maxHP;
    public int EP;
    public int maxEP;
    public int shield;
    public int maxshield;
    public int coin;
    public float shieldcovertime;
    public float everysheildcovertime;
    public float skilltime;
    public float skillcd;
    public List<GameObject> weapons;
    public int maxWeaponAmount=2;
    public int nowWeaponIndex;
    public Animator animatorController;
    public int sceneIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        //print((int)State.Attack);
        DontDestroyOnLoad(gameObject);
        state = State.Idle;
        weaponState = WeaponState.Far;
        skillState = SkillState.SkillCanUse;
        shieldState = ShieldState.Full;
        Instance = this;
        HandRange = GameObject.FindGameObjectWithTag("HandRange").GetComponent<Collider2D>();
        CloseWeaponRange = GameObject.FindGameObjectWithTag("CloseWeaponRange").GetComponent<Collider2D>();
        FarWeaponRange = GameObject.FindGameObjectWithTag("FarWeaponRange").GetComponent<Collider2D>();
        HP = maxHP;
        EP = maxEP;
        shield = maxshield;
        weapons = new List<GameObject>(maxWeaponAmount);
        GameObject game = Instantiate(Resources.Load<GameObject>("M1911"));
        game.transform.position = FarWeaponRange.transform.position;
        game.transform.parent = FarWeaponRange.transform;
        weapons.Add(game);
        nowWeaponIndex = 0;
        animatorController = GetComponent<Animator>();
        animatorController.SetBool("isDead", false);
        coin = 0;
        sceneIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex!=6)
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
                    if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
                        animatorController.SetBool("isRun", false);
                    else
                        animatorController.SetBool("isRun", true);

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
            if(shieldState==ShieldState.NotFull)
            {
                StartCoroutine("ShieldBeginCoverCoroutine");
            }
            if (skillState == SkillState.SkillOn)
                StartCoroutine(SkillOnCoroutine());
            if (skillState == SkillState.SkillNotPrepared)
                StartCoroutine(SkillCdCoroutine());
            if (Input.GetKeyDown(KeyCode.Q) && weapons.Count > 1)
            {
                weapons[nowWeaponIndex].SetActive(false);
                nowWeaponIndex++;
                nowWeaponIndex %= weapons.Count;
                weaponState = (WeaponState)(int)weapons[nowWeaponIndex].GetComponent<WeaponBase>().type;
                weapons[nowWeaponIndex].SetActive(true);
            }
        }
        //print(GetMousePosition());
        
        //if(Input.GetMouseButtonUp(0))
        //{
        //    StopCoroutine(Shootcoroutine());
        //    ChangeToMove();
        //}
        //for(int i=0;i<transform.childCount;i++)
        //{

        //}
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
        transform.Translate(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed * Time.deltaTime);
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
        transform.Translate(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed * Time.deltaTime);
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Vector3Int vector3Int = new Vector3Int((int)Input.GetAxisRaw("Horizontal"), 1, 1);
            transform.localScale = vector3Int;
        }
//        int num;
//        Collider2D[] Contact = new Collider2D[10];
//        ContactFilter2D ContactF2D = new ContactFilter2D();
//        ContactF2D.NoFilter();
//#nullable enable
//        switch (weaponState)
//        {
//            case WeaponState.Hand:
//                num = HandRange.OverlapCollider(ContactF2D, Contact);
//                for (int i = 0; i < num; i++)
//                {
//                    EnemyBase? enemyBase = Contact[i].GetComponent<EnemyBase>();
                    
//                }
//                break;
//            case WeaponState.Close:
//                num = CloseWeaponRange.OverlapCollider(ContactF2D, Contact);
//                for (int i = 0; i < num; i++)
//                {
                   
//                }
//                break;
//#nullable disable
//            case WeaponState.Far:
//                //if(Input.GetMouseButtonDown(0))
//                //{
//                //    GameObject bullet = BubblePool.Instance.GetBubble("bullet_M1911(Clone)");
//                //    bullet.transform.position = transform.position;
//                //    bullet.GetComponent<PlayerBullet>().MovePosition = (GetMousePosition() - new Vector2(bullet.transform.position.x, bullet.transform.position.y)).normalized;
//                //    bullet.SetActive(true);
//                //    StopCoroutine(Shootcoroutine());
//                //}
                    
//                break;
//        }
        
        //if (!Input.GetMouseButton(0))
        //{
        //    StartCoroutine(QuitAttackCoroutine());
            
        //}
        //else
        //{
        //    StopCoroutine("QuitAttacCoroutine");
        //}
        //if (Input.GetMouseButtonUp(0))
        //    StartCoroutine(QuitAttackCoroutine());

    }
    public void Hurt()
    {
        StartCoroutine(QuitHurtCoroutine());
        StopCoroutine("ShieldCoverCoroutine");
        StopCoroutine("ShieldBeginCoverCoroutine");
        shieldState = ShieldState.NotFull;
    }
    public void Dead()
    {
        
    }
    public void GetDamage(int damage)
    {
        shield -= damage;
        if(shield<0)
        {
            HP += shield;
            shield = 0;
        }
        if (HP < 0)
            HP = 0;
        ChangeToHurt();
    }
    public void ChangeToIdle()
    {
        state = State.Idle;
        animatorController.SetBool("isRun", false);
    }
    public void ChangeToMove()
    {
        state = State.Move;
        animatorController.SetBool("isRun", true);
    }
    public void ChangeToAttack()
    {
        state = State.Attack;
        //GameObject bullet = BubblePool.Instance.GetBubble("bullet_M1911(Clone)");
        //bullet.transform.position = transform.position;
        //bullet.GetComponent<PlayerBullet>().MovePosition = (GetMousePosition() - new Vector2(bullet.transform.position.x, bullet.transform.position.y)).normalized;
        //bullet.SetActive(true);
        //StartCoroutine(Shootcoroutine());       
    }
    public void ChangeToHurt()
    {
        state = State.Hurt;
    }
    public void ChangeToDead()
    {
        state = State.Dead;
        animatorController.SetBool("isDead", true);
    }
    public Vector2 GetMousePosition()
    {
        Vector2 screenposition = Input.mousePosition;
        Vector3 vector= Camera.main.ScreenToWorldPoint(new Vector3(screenposition.x, screenposition.y, 20f));
        return new Vector2(vector.x, vector.y);
    }

    //IEnumerator QuitAttackCoroutine()
    //{
    //    yield return new WaitForSeconds(quitattacktime);        
    //    ChangeToIdle();
    //}
    IEnumerator QuitHurtCoroutine()
    {
        yield return new WaitForSeconds(hurttime);
        ChangeToIdle();
    }
    IEnumerator ShieldBeginCoverCoroutine()
    {
        shieldState = ShieldState.BeginRecover;
        yield return new WaitForSeconds(shieldcovertime);
        StartCoroutine("ShieldCoverCoroutine");
    }
    IEnumerator ShieldCoverCoroutine()
    {
        shieldState = ShieldState.Recovering;
        while(shield<maxshield)
        {
            shield++;
            yield return new WaitForSeconds(everysheildcovertime);
        }
        shieldState = ShieldState.Full;
    }
    IEnumerator SkillCdCoroutine()
    {
        skillState = SkillState.SkillCovering;
        yield return new WaitForSeconds(skillcd); 
        skillState = SkillState.SkillCanUse;
    }
    IEnumerator SkillOnCoroutine()
    {
        skillState = SkillState.SkillUsing;
        yield return new WaitForSeconds(skilltime);
        skillState = SkillState.SkillNotPrepared;
    }

    public void SkillCd()
    {
        StartCoroutine(SkillCdCoroutine());
    }
    //IEnumerator Shootcoroutine()
    //{
    //   while(true)
    //    {
    //        yield return new WaitForSeconds(weapons[nowWeaponIndex].GetComponent<WeaponBase>().GetAttackSpeed());
    //        if (Input.GetMouseButton(0))
    //        {
    //            GameObject bullet = BubblePool.Instance.GetBubble("bullet_M1911(Clone)");
    //            bullet.transform.position = transform.position;
    //            bullet.GetComponent<PlayerBullet>().MovePosition = (GetMousePosition() - new Vector2(bullet.transform.position.x, bullet.transform.position.y)).normalized;
    //            bullet.SetActive(true);
    //        }
    //        else
    //            StopCoroutine("Shootcoroutine");
    //    }
            
                   
    //}
}

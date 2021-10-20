using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4 : WeaponBase
{
    Animator animator;
    void Start()
    {
        type = Type.Far;
        attackspeed = 0.2f;
        animator = GameObject.FindGameObjectWithTag("HandRange").GetComponent<Animator>();
        animator.speed = 4;
        EPcost = 2;
        transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {

        isOnPlayer = false;
        foreach (GameObject obj in PlayerController.Instance.weapons)
        {
            if (obj == gameObject)
            {
                isOnPlayer = true;
                break;
            }
        }
        if (isOnPlayer)
        {
            Vector2 vector = (PlayerController.Instance.GetMousePosition() - new Vector2(transform.position.x, transform.position.y)).normalized;
            transform.right = new Vector3(vector.x, vector.y, 0) * PlayerController.Instance.transform.localScale.x;
            if (PlayerController.Instance.weaponState == PlayerController.WeaponState.Far && HandAttack() == null && PlayerController.Instance.EP >= EPcost)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject bullet = BubblePool.Instance.GetBubble("bullet_M4(Clone)");
                    bullet.transform.position = transform.position;
                    bullet.GetComponent<PlayerBullet>().MovePosition = (PlayerController.Instance.GetMousePosition() - new Vector2(bullet.transform.position.x, bullet.transform.position.y)).normalized;                  
                    bullet.SetActive(true);
                    PlayerController.Instance.EP -= EPcost;
                    StartCoroutine(ShootCoroutine());
                }
                if (Input.GetMouseButtonUp(0))
                    StopAllCoroutines();
            }
            else if (HandAttack() != null)
            {
                Hattack();
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < 0.34f && Input.GetKeyDown(KeyCode.R))
            {
                if (PlayerController.Instance.weapons.Count < PlayerController.Instance.maxWeaponAmount)
                {
                    PlayerController.Instance.weapons.Add(gameObject);
                    PlayerController.Instance.weapons[PlayerController.Instance.nowWeaponIndex].SetActive(false);
                    PlayerController.Instance.nowWeaponIndex++;
                    PlayerController.Instance.nowWeaponIndex %= PlayerController.Instance.maxWeaponAmount;
                    gameObject.transform.parent = PlayerController.Instance.FarWeaponRange.transform;
                }
                else
                {
                    GameObject obj = PlayerController.Instance.weapons[PlayerController.Instance.nowWeaponIndex];
                    PlayerController.Instance.weapons.Remove(obj);
                    GameObject game = Instantiate(obj);
                    game.transform.position = obj.transform.position;
                    Destroy(obj);
                    PlayerController.Instance.weapons.Add(gameObject);
                    gameObject.transform.parent = PlayerController.Instance.FarWeaponRange.transform;
                }
            }
        }



    }

    public void Hattack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.Play("HandAttack", 0, 0f);
            int num;
            Collider2D[] Contact = new Collider2D[10];
            ContactFilter2D ContactF2D = new ContactFilter2D();
            ContactF2D.NoFilter();
            num = PlayerController.Instance.HandRange.OverlapCollider(ContactF2D, Contact);
            for (int i = 0; i < num; i++)
            {
                Contact[i].GetComponent<EnemyBase>()?.Hurt(1);
            }

        }
    }

    public override float GetAttackSpeed()
    {
        return attackspeed;
    }
    IEnumerator ShootCoroutine()
    {
        while (Input.GetMouseButton(0))
        {
            yield return new WaitForSeconds(attackspeed);
            GameObject bullet = BubblePool.Instance.GetBubble("bullet_M4(Clone)");
            bullet.transform.position = transform.position;
            bullet.GetComponent<PlayerBullet>().MovePosition = (PlayerController.Instance.GetMousePosition() - new Vector2(bullet.transform.position.x, bullet.transform.position.y)).normalized;
            PlayerController.Instance.EP -= EPcost;
            bullet.SetActive(true);
        }
    }
}

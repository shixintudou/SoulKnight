using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1911 : WeaponBase
{
    // Start is called before the first frame update
    Animator animator;
    void Start()
    {
        type = Type.Far;
        attackspeed = 0.8f;
        animator = GameObject.FindGameObjectWithTag("HandRange").GetComponent<Animator>();
        animator.speed = 4;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 vector = (PlayerController.Instance.GetMousePosition() - new Vector2(transform.position.x, transform.position.y)).normalized;
        transform.right = new Vector3(vector.x, vector.y, 0) * PlayerController.Instance.transform.localScale.x;
        if (PlayerController.Instance.weaponState == PlayerController.WeaponState.Far && HandAttack() == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject bullet = BubblePool.Instance.GetBubble("bullet_M1911(Clone)");
                bullet.transform.position = transform.position;
                bullet.GetComponent<PlayerBullet>().MovePosition = (PlayerController.Instance.GetMousePosition() - new Vector2(bullet.transform.position.x, bullet.transform.position.y)).normalized;
                bullet.SetActive(true);
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

    private void Hattack()
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
            GameObject bullet = BubblePool.Instance.GetBubble("bullet_M1911(Clone)");
            bullet.transform.position = transform.position;
            bullet.GetComponent<PlayerBullet>().MovePosition = (PlayerController.Instance.GetMousePosition() - new Vector2(bullet.transform.position.x, bullet.transform.position.y)).normalized;
            bullet.SetActive(true);
        }
    }
    //IEnumerator HandAttackCoroutine()
    //{
    //    yield return new WaitForSeconds(0.3f);
    //    animator.SetBool("isAttack", true);
    //    yield return new WaitForSeconds(0.3f);
    //    animator.SetBool("isAttack", false);
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaberBlue : WeaponBase
{
    // Start is called before the first frame update
    Animator Saber;
    Animator Qi;
    private void OnEnable()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
    void Start()
    {
        type = Type.Close;
        transform.localScale = new Vector3(1, 1, 1);
        Saber = GetComponent<Animator>();
        Qi = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isOnPlayer = false;
        if (transform.localScale.x < 1)
            transform.localScale = new Vector3Int(1, 1, 1);
        foreach (GameObject obj in PlayerController.Instance.weapons)
        {
            if (obj == gameObject)
            {
                isOnPlayer = true;
                break;
            }
        }
        if(!isCloned)
        {
            if (isOnPlayer)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Saber.Play("SwordRotate", 0, 0f);
                    Qi.Play("SwordAttack", 0, 0f);
                    int num;
                    Collider2D[] Contact = new Collider2D[10];
                    ContactFilter2D ContactF2D = new ContactFilter2D();
                    ContactF2D.NoFilter();
                    num = PlayerController.Instance.CloseWeaponRange.OverlapCollider(ContactF2D, Contact);
                    for (int i = 0; i < num; i++)
                    {
                        Contact[i].GetComponent<EnemyBase>()?.Hurt(damage);
                        Contact[i].GetComponent<EnemyBullet>()?.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                PickWeapon();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Saber.Play("SwordRotate", 0, 0f);
                Qi.Play("SwordAttack", 0, 0f);
                int num;
                Collider2D[] Contact = new Collider2D[10];
                ContactFilter2D ContactF2D = new ContactFilter2D();
                ContactF2D.NoFilter();
                num = PlayerController.Instance.CloseWeaponRange.OverlapCollider(ContactF2D, Contact);
                for (int i = 0; i < num; i++)
                {
                    Contact[i].GetComponent<EnemyBase>()?.Hurt(damage);
                    Contact[i].GetComponent<EnemyBullet>()?.gameObject.SetActive(false);
                }
            }
        }
    }
}

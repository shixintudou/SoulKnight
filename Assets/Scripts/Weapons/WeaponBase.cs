using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    private Collider2D attackrange;
    public float attackspeed;
    public int EPcost;
    public static float HandRange = 0.2f;
    public bool isOnPlayer;
    public bool isCloned = false;
    public enum Type
    {
        
        Close,
        Far,
    }
    public Type type;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual float GetAttackSpeed() => attackspeed;
#nullable enable
    public EnemyBase? HandAttack()
    {
        int num;
        Collider2D[] Contact = new Collider2D[10];
        ContactFilter2D ContactF2D = new ContactFilter2D();
        ContactF2D.NoFilter();
        num = PlayerController.Instance.HandRange.OverlapCollider(ContactF2D, Contact);
        for(int i=0;i<num;i++)
        {
            EnemyBase? enemyBase = Contact[i].GetComponent<EnemyBase>();           
            if (enemyBase != null)
                return enemyBase;
        }
        return null;
          
    }
    public void PickWeapon()
    {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < 0.34f && Input.GetKeyDown(KeyCode.R))
        {
            if (PlayerController.Instance.weapons.Count < PlayerController.Instance.maxWeaponAmount)
            {
                PlayerController.Instance.weapons.Add(gameObject);
                PlayerController.Instance.weapons[PlayerController.Instance.nowWeaponIndex].SetActive(false);
                PlayerController.Instance.nowWeaponIndex++;
                PlayerController.Instance.nowWeaponIndex %= PlayerController.Instance.maxWeaponAmount;
                gameObject.transform.position = PlayerController.Instance.transform.position;
                gameObject.transform.parent = PlayerController.Instance.CloseWeaponRange.transform;
            }
            else
            {
                GameObject obj = PlayerController.Instance.weapons[PlayerController.Instance.nowWeaponIndex];
                PlayerController.Instance.weapons.Remove(obj);
                GameObject game = Instantiate(obj);
                game.transform.position = obj.transform.position;
                Destroy(obj);
                PlayerController.Instance.weapons.Add(gameObject);
                gameObject.transform.position = PlayerController.Instance.transform.position;
                gameObject.transform.parent = PlayerController.Instance.CloseWeaponRange.transform;
            }
        }
    }    
}

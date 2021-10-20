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
}

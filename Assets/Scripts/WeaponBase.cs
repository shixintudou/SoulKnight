using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    private Collider2D attackrange;
    public static float attackspeed;
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
}

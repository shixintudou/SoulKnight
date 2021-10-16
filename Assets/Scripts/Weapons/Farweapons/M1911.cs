using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1911 : WeaponBase
{
    // Start is called before the first frame update
    void Awake()
    {
        type = Type.Far;       
        attackspeed = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(PlayerController.Instance.GetMousePosition());
    }
    public override float GetAttackSpeed()
    {
        return attackspeed;
    }
}

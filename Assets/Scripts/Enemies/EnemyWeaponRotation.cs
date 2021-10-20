using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInParent<EnemyBase>().state == EnemyBase.State.Attack)
            transform.LookAt(PlayerController.Instance.transform);
    }
}

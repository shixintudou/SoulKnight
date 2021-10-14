using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        //Vector2 rotpos = PlayerController.Instance.GetMousePosition() - pos;
        //rotpos.Normalize();
        transform.LookAt(PlayerController.Instance.GetMousePosition());
    }
   
}

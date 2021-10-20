using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaPontreasureChest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < 0.5f)
        {
            int n= Random.Range(0, 4);
            GameObject obj = Instantiate(Resources.Load<GameObject>(WeaponName.Instance.weapons[n]));
            obj.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTreasureChest : MonoBehaviour
{
    // Start is called before the first frame update
    int num;
    GameObject obj;
    void Start()
    {
        num = Random.Range(4, 8);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position,PlayerController.Instance.transform.position)<0.5f)
        {
            for(int i=0;i<num;i++)
            {
                int n = Random.Range(0, 2);                
                if(n==0)
                {
                    obj = Instantiate(Resources.Load<GameObject>("Coin"));
                }
                if(n==1)
                {
                    obj = Instantiate(Resources.Load<GameObject>("BlueCrystal"));
                }
                obj.transform.position = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
            }
            Destroy(gameObject);
        }
    }
}

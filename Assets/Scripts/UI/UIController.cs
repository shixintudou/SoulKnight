using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    GameObject player;
    GameObject[] Pools;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        player = GameObject.FindGameObjectWithTag("Player");
        Pools= GameObject.FindGameObjectsWithTag("Pool");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            transform.GetChild(1).gameObject.SetActive(false);
            player.SetActive(true);
            for(int i=0;i<Pools.Length;i++)
            {
                Pools[i].SetActive(true);
                Pools[i].SetActive(true);
            }
        }
            

    }
    
}

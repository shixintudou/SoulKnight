using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Tilemaps;
//using System.IO;

public class MapSetter : MonoBehaviour
{

    [System.Serializable]
    public class MapItem//地板，栅栏，地图障碍物等
    {
        public int itemAmount;
        public GameObject itemToMap;
        public bool shouldExpand;
    }




   
    public List<MapItem> itemsToThisMap;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);        
    }
    void OnEnable()
    {
        itemsToThisMap = new List<MapItem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
}

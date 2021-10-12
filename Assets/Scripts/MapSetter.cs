using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Tilemaps;
//using System.IO;

public class MapSetter : MonoBehaviour
{

    [System.Serializable]
    public class MapItem//�ذ壬դ������ͼ�ϰ����
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

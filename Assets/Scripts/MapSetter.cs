using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapSetter : MonoBehaviour
{

    [System.Serializable]
    public class MapItem//地板，栅栏，地图障碍物等
    {
        public int itemAmount;
        public GameObject itemToMap;
        public bool shouldExpand;
    }
    



    //[SerializeField]
    public List<MapItem> itemsToThisMap;//物体类型
    //[SerializeField]
    public Dictionary<string, List<GameObject>> tagObejectsInMap;//tag为key,游戏对象的list为value

    void Awake()
    {
        DontDestroyOnLoad(gameObject);        
    }
    void OnEnable()
    {
        itemsToThisMap = new List<MapItem>();
        foreach (MapItem item in itemsToThisMap)
        {
            tagObejectsInMap.Add(item.itemToMap.tag, new List<GameObject>());
            for(int i=0;i<item.itemAmount;i++)
            {
                GameObject obj = Instantiate(item.itemToMap);
                obj.SetActive(false);
                tagObejectsInMap[obj.tag].Add(obj);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetItem(string tag)
    {
        switch(tag)
        {
            case "StartMap":
               
                break;
        }
    }
}

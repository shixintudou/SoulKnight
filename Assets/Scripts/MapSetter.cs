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
        public bool shouldExpand = true;
    }
    public enum MapDirection
    {
        Up, Left, Down, Right
    }
    public int nowPosx;
    public int nowPosy;



    //[SerializeField]
    public List<MapItem> itemsToThisMap;//物体类型
    //[SerializeField]
    public Dictionary<string, List<GameObject>> tagObejectsInMap;//tag为key,游戏对象的list为value

    public float mapDistance;
    public MapDirection mapDirection;
    public bool[,] dfsmap;
    public float mapNumber;

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        //itemsToThisMap = new List<MapItem>();
        mapNumber = 0;
        tagObejectsInMap = new Dictionary<string, List<GameObject>>();
        foreach (MapItem item in itemsToThisMap)
        {
            tagObejectsInMap.Add(item.itemToMap.tag, new List<GameObject>());
            item.shouldExpand = false;
            for (int i = 0; i < item.itemAmount; i++)
            {
                GameObject obj = Instantiate(item.itemToMap);                
                //DontDestroyOnLoad(obj);
                tagObejectsInMap[obj.tag].Add(obj);
            }
        }
        dfsmap = new bool[itemsToThisMap[0].itemAmount, itemsToThisMap[0].itemAmount];
        for(int i=0;i<itemsToThisMap[0].itemAmount;i++)
        {
            for(int j=0;j<itemsToThisMap[0].itemAmount;j++)
            {
                dfsmap[i, j] = false;
            }
        }
        nowPosx = itemsToThisMap[0].itemAmount / 2;
        nowPosy = itemsToThisMap[0].itemAmount / 2;
        dfsmap[nowPosx,nowPosy] = true;
        //MapMove();
        //print(mapNumber);
        //tagObejectsInMap["StartMap"][0].SetActive(true);
        //tagObejectsInMap["StartMap"][0].transform.position += mapDistance * Vector3.right;
        //tagObejectsInMap["StartMap"][0].transform.GetChild(0).gameObject.SetActive(false);
    }
    public void Setmap()
    {
        while(mapNumber<itemsToThisMap[0].itemAmount)
        {
            mapDirection = (MapDirection)Random.Range(0, 5);
            switch(mapDirection)
            {
                case MapDirection.Up:
                    break;
                case MapDirection.Left:
                    break;
                case MapDirection.Down:
                    break;
                case MapDirection.Right:
                    break;
            }
        }
    }
    // Update is called once per frame
    //void Update()
    //{
    //    //print(tagObejectsInMap["StartMap"][0].transform.childCount);
    //}
    //public void SetItem(string tag)
    //{
    //    switch (tag)
    //    {
    //        case "StartMap":

    //            break;
    //    }
    //}
    //public void MapMove()
    //{

    //    //bool[,] dfsmap = new bool[itemsToThisMap[0].itemAmount, itemsToThisMap[0].itemAmount];
    //    for (int i = 0; i < itemsToThisMap[0].itemAmount * 2; i++)
    //    {
    //        for (int j = 0; j < itemsToThisMap[0].itemAmount * 2; j++)
    //        {
    //            dfsmap[i, j] = false;
    //        }
    //    }
    //    int Startx = Random.Range(0, itemsToThisMap[0].itemAmount);
    //    int Starty = Random.Range(0, itemsToThisMap[0].itemAmount);
    //    dfsmap[Startx, Starty] = true;//确定初始地图的位置
    //    if (mapNumber < itemsToThisMap[0].itemAmount)
    //    {
    //        Dfs(Startx, Starty);
    //    }

    //    int count = 0;
    //    for (int i = 0; i < itemsToThisMap[0].itemAmount * 2; i++)
    //    {
    //        for (int j = 0; j < itemsToThisMap[0].itemAmount * 2; j++)
    //        {
    //            if (dfsmap[i, j] && i != Startx && j != Starty)
    //            {
    //                Vector3 vector = new Vector3(i - Startx, j - Starty, transform.position.z);
    //                vector *= mapDistance;
    //                tagObejectsInMap["StartMap"][count++].transform.position += vector;
    //                print(true);
    //                print(count);
    //            }

    //        }
    //    }
    //}
    //private void Dfs(int x, int y)
    //{
    //    if (mapNumber >= itemsToThisMap[0].itemAmount)
    //        return;
    //    if (x >= itemsToThisMap[0].itemAmount * 2)
    //        Dfs(x - 2, y);
    //    if (y >= itemsToThisMap[0].itemAmount * 2)
    //        Dfs(x, y - 2);
    //    if (x < 0)
    //        Dfs(x + 2, y);
    //    if (y < 0)
    //        Dfs(x, y + 2);
    //    //int num = Random.Range(1, 5);
    //    if (x >= itemsToThisMap[0].itemAmount * 2 || y >= itemsToThisMap[0].itemAmount * 2)
    //        return;
    //    if (x < 0 || y < 0)
    //        return;

    //    if (mapNumber >= itemsToThisMap[0].itemAmount)
    //        return;

    //    if (!dfsmap[x, y])
    //    {
    //        dfsmap[x, y] = true;
    //        mapNumber++;
    //    }
    //    if (mapNumber >= itemsToThisMap[0].itemAmount)
    //        return;
    //    Dfs(x + 1, y);
    //    Dfs(x, y + 1);
    //    Dfs(x - 1, y);
    //    Dfs(x, y - 1);
    //    return;


        





    //}
}


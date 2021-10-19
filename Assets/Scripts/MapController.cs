using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    enum MapState
    {
        NotExplored, PlayerEntry, Exploring, Explored
    }





    // Start is called before the first frame update   
    //GameObject Ground;    
    Collider2D collider;
    [SerializeField]
    MapState mapState;
    List<GameObject> enmies;
    void Start()
    {
        collider = GetComponent<Collider2D>();
        mapState = MapState.NotExplored;
        enmies = new List<GameObject>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex!=0)
        {
            switch (mapState)
            {
                case MapState.NotExplored:
                    NotExplored();
                    break;
                case MapState.PlayerEntry:
                    PlayerEntry();
                    break;
                case MapState.Exploring:
                    Exploring();
                    break;
                case MapState.Explored:
                    break;
            }
        }
        

    }
    void NotExplored()
    {
        int num;
        Collider2D[] Contact = new Collider2D[10];
        ContactFilter2D ContactF2D = new ContactFilter2D();
        ContactF2D.NoFilter();
        num = collider.OverlapCollider(ContactF2D, Contact);
        for(int i=0;i<num;i++)
        {
            if (Contact[i].CompareTag("Player"))
            {
                mapState = MapState.PlayerEntry;               
            }
        }
    }
    void PlayerEntry()
    {
        if(MapSetter.Instance.mapNumber>0)
        {
            int amout = Random.Range(5, 10);
            for (int i = 0; i < amout; i++)
            {
                GameObject enemy = EnemyPool.Instance.GetEnemyToMap();
                enemy.transform.position = transform.position + new Vector3(Random.Range(-4.5f, 4.5f), Random.Range(-2.1f, 2.1f), 0);
                enemy.SetActive(true);
                enmies.Add(enemy);
            }     
            for (int i = 4; i <= 7; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            mapState = MapState.Exploring;
        }
        else
        {            
            mapState = MapState.Explored;
            SetFenceAndRoad();
            MapSetter.Instance.mapNumber++;
        }
    }
    void Exploring()
    {
        foreach(GameObject enemy in enmies)
        {
            if (enemy.activeInHierarchy)
                return;
        }
        mapState = MapState.Explored;
        MapSetter.Instance.mapNumber++;
        if(MapSetter.Instance.mapNumber>=MapSetter.Instance.itemsToThisMap[0].itemAmount)
        {
            GameObject gate = Instantiate(Resources.Load<GameObject>("Gate"));
            gate.transform.position = transform.position;
        }
        SetFenceAndRoad();
    }
    void SetFenceAndRoad()
    {
        int tx = System.Convert.ToInt32(transform.position.x / MapSetter.Instance.mapDistance);
        int ty = System.Convert.ToInt32(transform.position.y / MapSetter.Instance.mapDistance);
        int x = MapSetter.Instance.nowPosx + tx;
        int y = MapSetter.Instance.nowPosy + ty;
        int amout = MapSetter.Instance.itemsToThisMap[0].itemAmount;
        if (x - 1 > 0 && MapSetter.Instance.dfsmap[x - 1, y])
        {
            transform.GetChild(5).gameObject.SetActive(false);//LeftFence
            transform.GetChild(9).gameObject.SetActive(true);//LeftRoad
        }
        else
        {
            transform.GetChild(5).gameObject.SetActive(true);//LeftFence
            transform.GetChild(9).gameObject.SetActive(false);//LeftRoad
        }
        if (x + 1 < amout - 1 && MapSetter.Instance.dfsmap[x + 1, y])
        {
            transform.GetChild(6).gameObject.SetActive(false);//RightFence
            transform.GetChild(10).gameObject.SetActive(true);//RightRoad
        }
        else
        {
            transform.GetChild(6).gameObject.SetActive(true);//RightFence
            transform.GetChild(10).gameObject.SetActive(false);//RightRoad
        }
        if (y - 1 > 0 && MapSetter.Instance.dfsmap[x, y - 1])
        {
            transform.GetChild(7).gameObject.SetActive(false);//DownFence
            transform.GetChild(12).gameObject.SetActive(true);//DownRoad
        }
        else
        {
            transform.GetChild(7).gameObject.SetActive(true);//DownFence
            transform.GetChild(12).gameObject.SetActive(false);//DownRoad
        }
        if (y + 1 < amout - 1 && MapSetter.Instance.dfsmap[x, y + 1])
        {
            transform.GetChild(4).gameObject.SetActive(false);//UpFence
            transform.GetChild(11).gameObject.SetActive(true);//UpRoad
        }
        else
        {
            transform.GetChild(4).gameObject.SetActive(true);//UpFence
            transform.GetChild(11).gameObject.SetActive(false);//UpRoad
        }
    }
}

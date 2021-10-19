using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundSet : MonoBehaviour
{
    // Start is called before the first frame update
    Tilemap tilemap;
    Tile tile;
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        tile = ScriptableObject.CreateInstance<Tile>();
        for (int i=0;i<10;i++)
        {
            for(int j=0;j<6;j++)
            {
                tile = Instantiate(Resources.Load<Tile>("f101_0"));
                tilemap.SetTile(new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z) + new Vector3Int(i - 5, j - 3, 0), tile);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void InitGround()
    {

    }
}

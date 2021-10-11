using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//using System.IO;

public class MapSetter : MonoBehaviour
{
    public Tilemap tilemap;
    private Dictionary<string, Tile> tiles;
    private List<string> tilename;
    string []tiletype;
    public int height;
    public int width;
    // Start is called before the first frame update
    void Start()
    {
        tiles = new Dictionary<string, Tile>();
        tilename = new List<string>();
        tilemap = GetComponent<Tilemap>();
        InitalTile();
        InitMapTilesInfo();
        InitData();
    }

    // Update is called once per frame
    void Update()
    {
        //print(Path.GetFileName("Assets/Resource/Sprite/map/floor/f101_0.asset"));
    }
    void InitalTile()
    {
        AddTile("floor1", "Assets/Resource/Sprite/map/floor/f101_0.asset");
        AddTile("floor2", "../Resource/Sprite/map/floor/f102");
        AddTile("floor3", "../Resource/Sprite/map/floor/f103.png");
    }
    void AddTile(string labelname,string spritePath)
    {
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        Sprite tmp = Resources.Load<Sprite>(spritePath);
        tile.sprite = tmp;
        tiles.Add(labelname, tile);
        tilename.Add(labelname);
    }
    void InitMapTilesInfo()
    {
        tiletype = new string[height * width];
        for(int i=0;i<height;i++)
        {
            for(int j=0;j<width;j++)
            {
                tiletype[i * width + j] = tilename[Random.Range(0, tilename.Count)];
            }
        }
    }
    void InitData()
    {
        for(int i=0;i<height;i++)
        {
            for(int j=0;j<width;j++)
            {
                tilemap.SetTile(new Vector3Int(j, i, 0), tiles[tiletype[i * width + j]]);
            }
        }
    }
}

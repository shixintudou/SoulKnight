using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

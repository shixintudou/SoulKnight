using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    // Start is called before the first frame update
    bool isexplored;
    GameObject Ground;
    public Collider2D collider;
    void Start()
    {
        collider = GetComponent<Collider2D>();
        if (SceneManager.GetActiveScene().buildIndex == 0)
            isexplored = true;
        else
        {
            isexplored = false;
            Ground = Instantiate(Resources.Load<GameObject>("Ground"));
            Ground.transform.position = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

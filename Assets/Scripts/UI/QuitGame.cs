using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuitGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Exit()
    {       
        Application.Quit();
    }
}

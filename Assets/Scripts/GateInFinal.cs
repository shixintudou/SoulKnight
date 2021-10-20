using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateInFinal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < 0.34f)
            {
                if (PlayerController.Instance.sceneIndex < 5)
                    PlayerController.Instance.sceneIndex++;
                else
                    PlayerController.Instance.sceneIndex = 0;
                SceneManager.LoadScene(6);
            }
               
        }
    }
}

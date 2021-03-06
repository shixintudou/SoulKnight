using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateWhenLoad : MonoBehaviour
{
    AsyncOperation async = null;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneCoroutine());
        PlayerController.Instance.transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LoadSceneCoroutine()
    {
        async = SceneManager.LoadSceneAsync(PlayerController.Instance.sceneIndex);
        async.allowSceneActivation = true;
        while(!async.isDone)
        {
            yield return null;
        }
    }
}

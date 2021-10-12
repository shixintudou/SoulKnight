using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float movespeed;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vector = Vector2.Lerp(transform.position, PlayerController.Instance.transform.position, PlayerController.Instance.speed * Time.deltaTime * movespeed);
        Vector3 cameraposition = new Vector3(vector.x, vector.y, transform.position.z);
        transform.position = cameraposition;
    }
}

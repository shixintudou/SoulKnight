using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCrystal : MonoBehaviour
{
    // Start is called before the first frame update
    public float distance = 0.5f;
    public float speed = 3.6f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < distance)
        {
            Vector2 vector = Vector2.Lerp(transform.position, PlayerController.Instance.transform.position, speed * Time.deltaTime);
            Vector3 vector3 = new Vector3(vector.x, vector.y, transform.position.z);
            transform.position = vector3;
            if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < 0.1f)
            {
                PlayerController.Instance.EP += 8;
                if (PlayerController.Instance.EP >= PlayerController.Instance.maxEP)
                    PlayerController.Instance.EP = PlayerController.Instance.maxEP;
                Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldStrip : MonoBehaviour
{
    RectTransform rect;
    float startx;
    void Start()
    {
        rect = GetComponent<RectTransform>();
        startx = rect.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        float a = (float)PlayerController.Instance.shield / PlayerController.Instance.maxshield;
        rect.localScale = new Vector3(startx * a, rect.localScale.y, rect.localScale.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponName : MonoBehaviour
{
    // Start is called before the first frame update
    public static WeaponName Instance;
    public List<string> weapons;
    public int amount;
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

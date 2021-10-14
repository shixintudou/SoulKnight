using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePool : MonoBehaviour
{
    public static BubblePool Instance;
    public Dictionary<string, List<GameObject>> Bubbles;
    public int amountToPool;
    public GameObject[] BubbleTypeToPool;
    public bool shouldexpand;
    // Start is called before the first frame update
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }
    void OnEnable()
    {
        Instance = this;
        shouldexpand = true;
        Bubbles = new Dictionary<string, List<GameObject>>(BubbleTypeToPool.Length);
        
        for(int i=0;i<BubbleTypeToPool.Length;i++)
        {
            Bubbles.Add(BubbleTypeToPool[i].name, new List<GameObject>());
            for(int j=0;j<amountToPool;j++)
            {
                GameObject obj = BubbleTypeToPool[i];
                Bubbles[obj.name].Add(obj);
                obj.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject GetBubble(string name)
    {
        if (Bubbles.ContainsKey(name))
        {
            for (int i = 0; i < Bubbles[name].Count; i++)
            {
                if (!Bubbles[name][i].activeInHierarchy)
                    return Bubbles[name][i];
            }
            if (shouldexpand)
            {
                GameObject obj = Bubbles[name][0];
                obj.SetActive(true);
                Bubbles[name].Add(obj);
                return obj;
            }
            else
                return null;
        }
        else
        {
            print("BubbleNameError");
            return null;
        }
    }
}

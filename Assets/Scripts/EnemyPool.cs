using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance;
    public Queue<GameObject> enemies;
    public int amountToPool;
    public GameObject[] enemiesTypeToPool;
    public bool shuldexpand;
    // Start is called before the first frame update
    void OnEnable()
    {
        Instance = this;
        shuldexpand = false;
        enemies = new Queue<GameObject>();
        for(int i=0;i<amountToPool;i++)
        {
            GameObject enemy = Instantiate(enemiesTypeToPool[Random.Range(0, enemiesTypeToPool.Length)]);
            enemy.SetActive(false);
            enemies.Enqueue(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject GetEnemyToMap()
    {
        for(int i=0;i<enemies.Count;i++)
        {
            if (!enemies.Peek().activeInHierarchy)
                return enemies.Peek();
            else
            {
                enemies.Enqueue(enemies.Dequeue());
            }
        }
        if (shuldexpand)
        {
            GameObject enemy = Instantiate(enemiesTypeToPool[Random.Range(0, enemiesTypeToPool.Length)]);
            enemy.SetActive(false);
            enemies.Enqueue(enemy);
            return enemy;
        }
        else
            return null;
    }
}

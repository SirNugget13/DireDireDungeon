using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawn : MonoBehaviour
{
    public GameObject[] EnemyList;

    private GameObject Enemy;

    private void Start()
    {
        int randomID = Random.Range(0, EnemyList.Length);

        Enemy = Instantiate(EnemyList[randomID], transform.position, Quaternion.identity);
    }
}

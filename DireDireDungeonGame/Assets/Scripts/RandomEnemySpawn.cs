using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawn : MonoBehaviour
{
    public GameObject[] EnemyList;
    public EnemyParent enemyParent;

    private GameObject Enemy;

    private void Start()
    {
        int randomID = Random.Range(0, EnemyList.Length);

        enemyParent = GameObject.FindWithTag("EnemyParent").GetComponent<EnemyParent>();

        if(EnemyList[randomID] != null && enemyParent.doEnemySpawn)
        {
            Enemy = Instantiate(EnemyList[randomID], transform.position, Quaternion.identity);
            Enemy.transform.parent = enemyParent.gameObject.transform;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject[] coinList;
    public int numCoinsToSpawn;
    public float coinSpread = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i <= numCoinsToSpawn; i++)
        {
            GameObject coin;
            int randomVal = Random.Range(0, coinList.Length);
            coin = Instantiate(coinList[randomVal], transform.position, Quaternion.identity);

            Vector2 randomVelo = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));

            coin.GetComponent<Rigidbody2D>().velocity = randomVelo * coinSpread;
            //coin.transform.parent = gameObject.transform;
        }

        Destroy(gameObject);
    }
}

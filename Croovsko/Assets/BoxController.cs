using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoxController : MonoBehaviour
{

    public int coinsToSpawn = 5;
    public CoinController coinPrefab;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<LeftRightController>())
        {
            Debug.Log("Destroy me noww");
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(-other.gameObject.GetComponent<Rigidbody2D>().velocity.normalized * 10f, ForceMode2D.Impulse);
            DestroyWithCoins();
        }
    }

    public void DestroyWithCoins()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        for (int i = 0; i < coinsToSpawn; i++)
        {
            var coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            var randomX = Random.Range(-1f, 1f);
            coin.AddForce(new Vector2(randomX, 1) * 10f);
        }
        Destroy(this.gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoxController : MonoBehaviour
{

    public int coinsToSpawn = 5;
    public CoinController coinPrefab;

    private ParticleSystem _particle;

    private void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<LeftRightController>())
        {
            var randomX = Random.Range(-1f, 1f);
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(randomX, 1) * 5f, ForceMode2D.Impulse);
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
        _particle.Emit(18);
        StartCoroutine(nameof(DestroyDelay));
    }

    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSecondsRealtime(_particle.main.duration);
        Destroy(this.gameObject);
    }
}

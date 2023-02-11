using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgingMiniGame : MiniGame
{
    [SerializeField] string[] fallingItems;
    [SerializeField] Collider2D spawnArea;
    [SerializeField] Collider2D destructorCollider;

    private bool isSpawning;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == PlayerCollider)
            StartCoroutine(base.StartMiniGame(StartDodging));
    }

    void StartDodging()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        isSpawning = true;

        while (isSpawning)
        {
            int randomNumber = Random.Range(0, fallingItems.Length);
            float xBounds = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            Vector2 position = new Vector2(xBounds, spawnArea.transform.position.y);

            SingletonManager.Get<ObjectPooler>().SpawnFromPool(fallingItems[randomNumber], position, Quaternion.identity);

            yield return new WaitForSeconds(1f);
        }
    }
}

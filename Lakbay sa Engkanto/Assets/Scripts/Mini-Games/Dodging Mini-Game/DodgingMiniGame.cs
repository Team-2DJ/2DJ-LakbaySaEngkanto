using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgingMiniGame : MiniGame
{
    [SerializeField] string[] fallingItems;
    [SerializeField] Collider2D spawnArea;
    [SerializeField] Collider2D destructorCollider;
    [SerializeField] private string id;

    [Header("Score System")]
    private int currentScore;
    [SerializeField] private int winningScore;

    private bool isSpawning;

    void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnPlayerCollectItem -= SeedCollected;
    }

    protected override void Start()
    {
        base.Start();

        SingletonManager.Get<GameEvents>().OnPlayerCollectItem += SeedCollected;
        currentScore = 0;
    }

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

    public void SeedCollected(string id)
    {
        if (id != this.id)
            return;

        currentScore++;
        Debug.Log("Current Score: " + currentScore);

        if (currentScore >= winningScore)
        {
            isSpawning = false;
            Debug.Log("You have engough seeds. Grow the Tree!!!");
            StartCoroutine(EndMiniGame());
        }
    }

    IEnumerator EndMiniGame()
    {
        // Trigger Tree Animation
        
        yield return new WaitForSeconds(3f);

        // Declare End of Game
        Debug.Log("End Game");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgingMiniGame : MiniGame
{
    [SerializeField] string[] fallingItems;
    [SerializeField] Collider2D spawnArea;
    [SerializeField] private string id;

    [SerializeField] private int spawnAmount = 2;

    [Header("Score System")]
    private int currentScore;
    [SerializeField] private int winningScore;

    private bool isSpawning;
    private bool hasEnded;

    private void OnEnable()
    {
        SingletonManager.Get<GameEvents>().OnPlayerCollectItem += SeedCollected;
    }

    private void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnPlayerCollectItem -= SeedCollected;
        isSpawning = false;
    }

    protected override void Start()
    {
        base.Start();

        currentScore = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == PlayerCollider)
        {
            if (hasEnded) return;
            if (isSpawning) return;

            StartCoroutine(base.StartMiniGame(StartDodging));
        }
    }

    void StartDodging()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        isSpawning = true;

        // Continue Spawning while Winning Conditions are Not Met
        while (isSpawning)
        {
            // Spawn a Certain Amount of Items
            for (int i = 0; i < spawnAmount; i++)
            {
                // Randomize Object Index
                int randomIndex = Random.Range(0, fallingItems.Length);

                // Randomize X Position
                float xBounds = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
                Vector2 randomPosition = new Vector2(xBounds, spawnArea.transform.position.y);

                // Randomize Rotation
                Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

                // Spawn GameObject From Pool
                GameObject go = SingletonManager.Get<ObjectPooler>().SpawnFromPool(fallingItems[randomIndex], randomPosition, randomRotation);
                go.transform.SetParent(spawnArea.transform);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public void SeedCollected(string id)
    {
        if (id != this.id)
            return;

        // Increment Score
        currentScore++;

        SingletonManager.Get<GameEvents>().ScoreChanged(id, currentScore, winningScore);

        Debug.Log("Current Score: " + currentScore);

        // If Winning Score has been Reached, Stop Spawning
        if (currentScore >= winningScore)
        {
            isSpawning = false;
            hasEnded = true;
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

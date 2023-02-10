using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class MiniGame : MonoBehaviour
{
    // On Start (On Collision)
    // Close the doors

    // Execute Game

    // On Win
    // Open the Doors

    public enum MiniGameType
    {
        JOURNAL_PAGE,
        BOSS
    }

    public Collider2D PlayerCollider { get; set; }

    [SerializeField] private MiniGameType type;
    [SerializeField] private float endDelay;

    // TO BE REMOVED
    [SerializeField] GameObject journalPage;
    [SerializeField] Transform journalPageSpawn;

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
    }

    protected IEnumerator StartMiniGame(Action miniGame)
    {
        yield return new WaitForSeconds(3f);

        // Close the Doors
        SingletonManager.Get<GameEvents>().CloseDoors();

        // Return the Function Call
        miniGame?.Invoke();
    }

    protected IEnumerator EndMiniGame()
    {
        yield return new WaitForSeconds(endDelay);

        if (type == MiniGameType.JOURNAL_PAGE)
        {
            // Spawn Journal Page
            Debug.Log("Here's your journal page");

            Instantiate(journalPage, journalPageSpawn.position, Quaternion.identity);

            // Open the Doors
        }
        else
        {
            // End Game (Level Complete!!!)
        }
    }
}

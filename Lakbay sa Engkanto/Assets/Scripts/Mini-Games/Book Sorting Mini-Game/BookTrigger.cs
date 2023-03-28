using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider2D))]
public class BookTrigger : MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] private Sprite sorted, notSorted;
    private bool isGameComplete;
    private SpriteRenderer spriteRenderer;

    [Header("Gameplay Settings")]
    [SerializeField] private string doorToOpen;                             // Door To Open string
    [SerializeField] private string bookCategory;                           // Sort Category
    [SerializeField] private List<string> correctTitles = new();        // Titles of the different books 
    [SerializeField] private List<string> wrongTitles = new();          // Titles of the different books

    private void OnEnable()
    {
        SingletonManager.Get<GameEvents>().OnSetCondition += GameCompleted;
    }

    private void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnSetCondition -= GameCompleted;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = notSorted;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isGameComplete) return;

        SingletonManager.Get<UIEvents>().AddButtonListener(EnableBooksMiniGame);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isGameComplete) return;

        SingletonManager.Get<UIEvents>().RemoveButtonListener(EnableBooksMiniGame);
    }

    private void EnableBooksMiniGame()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Book Sorting Panel");
        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(false);

        SingletonManager.Get<BookSortingMiniGame>().Initialize(id, doorToOpen, bookCategory, correctTitles, wrongTitles);
    }

    private void GameCompleted(string id, bool condition)
    {
        if (id != this.id) return;
        isGameComplete = condition;

        if (!isGameComplete) return;

        spriteRenderer.sprite = isGameComplete ? sorted : notSorted;
    }
}

using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PodiumMiniGame : MonoBehaviour
{
    [Header("Object Setup")]
    [SerializeField] private string id;
    [SerializeField] private PodiumSlot podiumSlot;
    [SerializeField] private GameObject inventorySlotHolder;
    [SerializeField] private GameObject inventoryItemPrefab;

    [Header("Gameplay Settings")]
    [SerializeField] private GameObject page;
    [SerializeField] private ItemData itemData;


    private Dictionary<ItemData, GameObject> itemDictionary = new();
    private List<InventorySlot> inventorySlots = new();

    public bool IsComplete { get; private set; }

    private void Awake()
    {
        InventorySlot[] tempInventorySlot = inventorySlotHolder?.GetComponentsInChildren<InventorySlot>() ?? new InventorySlot[0];

        foreach (InventorySlot slot in tempInventorySlot)
        {
            inventorySlots.Add(slot);
        }
    }

    private void OnEnable()
    {
        var itemDataList = SingletonManager.Get<PlayerManager>().PlayerInventory.ItemDataList;

        foreach (ItemData itemData in itemDataList)
        {
            ShowItem(itemData);
        }
    }

    private void OnDisable()
    {
        foreach (var item in itemDictionary)
        {
            Destroy(item.Value);
        }

        itemDictionary.Clear();
    }

    private void Start()
    {
        podiumSlot.Initialize(id, itemData);
    }

    private void ShowItem(ItemData itemData)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null)
            {
                SpawnInventorySlot(itemData, slot);
                return;
            }
        }
    }

    private void SpawnInventorySlot(ItemData itemData, InventorySlot slot)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(itemData);

        itemDictionary.TryAdd(itemData, newItem);
    }

    public void CheckOrder()
    {
        if (IsComplete) return;

        if (podiumSlot.IsRight)
        {
            // Event that Corresponds to page being given; 

            IsComplete = true;

            SingletonManager.Get<PlayerManager>().PlayerInventory.RemoveItem(podiumSlot.GetItemData());

            gameObject.SetActive(false);
        }
    }
}

using TMPro;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Includes all the Logic for the Podium Game 
/// </summary> 
public class PodiumMiniGame : MonoBehaviour
{
    [Header("Object Setup")]
    [SerializeField] private PodiumSlot podiumSlot;                     // Podium Slot Reference
    [SerializeField] private GameObject inventorySlotHolder;            // Object that holds the Inventory Slots
    [SerializeField] private GameObject inventoryItemPrefab;            // Item that will be instatiated by the Inventory Slots
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;           // TMPro reference


    private string id;                                 // Object ID
    private string doorToOpen;                         // Door To Open string
    private string question;                           // The Question that will be shown
    private GameObject page;                           // Journal Page to Instatiate
    private ItemData itemData;                         // itemData that will be used for checking


    private Dictionary<ItemData, GameObject> itemDictionary = new();    // player inventory reference
    private List<InventorySlot> inventorySlots = new();                 // List of all inventorySlots

    public bool IsComplete { get; private set; }

    public void Initialize(string _id, string _doorToOpen, string _question, GameObject _page, ItemData _itemData)
    {
        ClearData();

        id = _id;
        doorToOpen = _doorToOpen;
        question = _question;
        page = _page;
        itemData = _itemData;

        textMeshProUGUI.text = question;
        podiumSlot.Initialize(id, itemData);
    }

    private void ClearData()
    {
        id = null;
        doorToOpen = null;
        question = null;
        page = null;
        itemData = null;
        IsComplete = false;
    }


    private void Awake()
    {
        SingletonManager.Register(this);

        InventorySlot[] tempInventorySlot = inventorySlotHolder?.GetComponentsInChildren<InventorySlot>() ?? new InventorySlot[0];

        foreach (InventorySlot slot in tempInventorySlot)
        {
            inventorySlots.Add(slot);
        }
    }

    private void OnEnable()
    {
        // Temporary reference for all items present in the Players inventory
        var itemDataList = SingletonManager.Get<PlayerManager>().PlayerInventory.ItemDataList;

        foreach (ItemData itemData in itemDataList)
        {
            ShowItem(itemData);
        }
    }

    private void OnDisable()
    {
        // Once object gets disabled, destroy all InventoryItem found within the scene
        foreach (var item in itemDictionary)
        {
            Destroy(item.Value);
        }

        // Clears the ItemDictionary
        itemDictionary.Clear();
    }

    /// <summary>
    /// Shows Items in the inventory slots
    /// </summary>
    /// <param name = "itemData"> Used as a Key for the Dictionary </param> 
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

    /// <summary>
    /// Spawns items in the Inventory Slot based on the inventoryItemPrefab
    /// </summary>
    /// <param name = "itemData"> Used for intialization and as a key in the Dictionary </param> 
    /// <param name = "slot">  Will be used as a parent of an item being instantiated </param> 
    private void SpawnInventorySlot(ItemData itemData, InventorySlot slot)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(itemData);

        newItem.GetComponent<PodiumBook>().Initialize(id);

        itemDictionary.TryAdd(itemData, newItem);
    }

    public void CheckOrder()
    {
        if (IsComplete) return;

        if (podiumSlot.IsRight)
        {
            Debug.Log("CORRECT");

            SingletonManager.Get<GameEvents>().OpenDoor(doorToOpen);

            SingletonManager.Get<GameEvents>().SetCondition(id, true);

            IsComplete = true;

            gameObject.SetActive(false);

            SingletonManager.Get<PlayerManager>().PlayerInventory.RemoveItem(podiumSlot.GetItemData());

            // Turns back on PlayerMovement 
            SingletonManager.Get<PlayerEvents>().SetPlayerMovement(true);

            // Turns back on Game Panel
            SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel");
        }
        else
        {
            // Invoke a false boolean that listeners will receive. 
            SingletonManager.Get<GameEvents>().SetCondition(id, false);
        }
    }

    public void OnCloseButtonClicked()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel");
        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(true);
    }

    public string GetID()
    {
        return id;
    }
}

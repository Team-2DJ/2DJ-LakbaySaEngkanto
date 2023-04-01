using UnityEngine;
using TMPro;

public class DebugMode : MonoBehaviour
{
    private TextMeshProUGUI textBoxGUI;
    [SerializeField] private GameObject[] pages;
    private int currentPage;

    public void SetTextBoxGUI(TextMeshProUGUI value)
    {
        textBoxGUI = value;
    }

    private void OnEnable()
    {
        currentPage = 0;
        ActivatePage(currentPage);
    }

    public void OnExitButtonClicked()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Pause Panel");
        Time.timeScale = 0f;
    }

    public void OnNextButtonClicked()
    {
        currentPage++;

        if (currentPage >= pages.Length)
        {
            currentPage = 0;
        }

        ActivatePage(currentPage);
    }

    public void OnPreviousButtonClicked()
    {
        currentPage--;

        if (currentPage < 0)
        {
            currentPage = pages.Length - 1;
        }

        ActivatePage(currentPage);
    }

    public void ActivatePage(int pageNumber)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }

        pages[pageNumber].SetActive(true);
    }

    public void EnablePlayerMovement()
    {
        if (!SingletonManager.Get<PlayerManager>()?.Player) return;

        bool playerCanMove = !SingletonManager.Get<PlayerManager>().Player.PlayerMovement.canMove;

        textBoxGUI.text = playerCanMove ? "ENABLED" : "DISABLED";

        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(playerCanMove);
    }

    public void EnableDialogue()
    {
        if (!SingletonManager.Get<DebugEvents>()) return;

        bool condition = !SingletonManager.Get<DebugEvents>().IsDialogueEnabled;

        textBoxGUI.text = condition ? "ENABLED" : "DISABLED";

        SingletonManager.Get<DebugEvents>().EnableDialogue(condition);
    }

    public void EnableCutscene()
    {
        if (!SingletonManager.Get<DebugEvents>()) return;

        bool condition = !SingletonManager.Get<DebugEvents>().IsCutsceneEnabled;

        textBoxGUI.text = condition ? "ENABLED" : "DISABLED";

        SingletonManager.Get<DebugEvents>().EnableCutscene(condition);
    }

    public void AddRemoveBook(ItemData itemData)
    {
        if (!SingletonManager.Get<PlayerManager>()?.PlayerInventory) return;

        InventoryManager inventory = SingletonManager.Get<PlayerManager>().PlayerInventory;

        if (!SingletonManager.Get<PlayerManager>().PlayerInventory.ItemDataList.Contains(itemData))
        {
            inventory.AddItem(itemData);
            textBoxGUI.text = "REMOVE";
        }
        else
        {
            inventory.RemoveItem(itemData);
            textBoxGUI.text = "ADD";
        }
    }

    public void ChangeControls()
    {
        if (!SingletonManager.Get<PlayerManager>()) return;

        PlayerManager playerManager = SingletonManager.Get<PlayerManager>();

        if (!playerManager.IsTesting)
        {
            playerManager.IsTesting = true;
            textBoxGUI.text = "KEYBOARD";
        }
        else
        {
            playerManager.IsTesting = false;
            textBoxGUI.text = "TOUCH";
        }
    }

    public void SetPlayerSpawnPoint(Transform spawnPoint)
    {
        if (!SingletonManager.Get<PlayerManager>()) return;

        SingletonManager.Get<PlayerManager>().Player.transform.position = spawnPoint.position;

    }
}

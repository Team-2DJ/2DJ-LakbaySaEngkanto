using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] private DialogueData[] dialogueData;                           // Contains all Dialogue for this Trigger

    private Collider2D playerCollider;                                              // Player Collider Component Reference
    private bool hasActivated;                                                      // Checks if Player has Collided with this Trigger

    // Start is called before the first frame update
    void Start()
    {
        if (SingletonManager.Get<PlayerManager>().PlayerData.StringList.Contains(id))
        {
            Destroy(gameObject);
            return;
        }

        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
        hasActivated = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ensure that this Dialogue Trigger will Only be Triggered Once
        if (hasActivated)
            return;

        if (other == playerCollider)
        {
            SingletonManager.Get<PlayerManager>().PlayerData.AddString(id);

            // Disable Player Movement
            SingletonManager.Get<PlayerEvents>().SetPlayerMovement(false);

            // Activate Dialogue Panel
            SingletonManager.Get<PanelManager>().ActivatePanel("Dialogue Panel");

            // Pass Dialogue Data on to the Dialogue Manager
            SingletonManager.Get<DialogueManager>().InitializeDialogues(dialogueData);

            hasActivated = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueData[] dialogueData;                           // Contains all Dialogue for this Trigger
    
    private Collider2D playerCollider;                                              // Player Collider Component Reference
    private bool hasActivated;                                                      // Checks if Player has Collided with this Trigger

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
        hasActivated = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            // Ensure that this Dialogue Trigger will Only be Triggered Once
            if (hasActivated)
                return;
            
            // Disable Player Movement
            SingletonManager.Get<GameEvents>().DialogueStart();

            // Activate Dialogue Panel
            SingletonManager.Get<PanelManager>().ActivatePanel("Dialogue Panel", 0f);
            
            // Pass Dialogue Data on to the Dialogue Manager
            SingletonManager.Get<DialogueManager>().InitializeDialogues(dialogueData);
            
            hasActivated = true;
        }
    }
}
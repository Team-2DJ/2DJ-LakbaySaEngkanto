using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    private Collider2D playerCollider;

    public DialogueData[] dialogueData;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            SingletonManager.Get<PanelManager>().ActivatePanel("Dialogue Panel", 0f);
            SingletonManager.Get<DialogueManager>().InitializeDialogues(dialogueData);
        }
    }
}

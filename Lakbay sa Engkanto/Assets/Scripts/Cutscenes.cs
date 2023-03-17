using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cutscenes : MonoBehaviour
{
    protected Collider2D playerCollider;
    private bool hasActivated;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            if (hasActivated)
                return;
            
            // Disable Player Movement
            SingletonManager.Get<PlayerEvents>().SetPlayerMovement(false);

            Debug.Log("EXECUTE CUTSCENE");

            // Trigger the Cutscene
            ExecuteCutscene();

            hasActivated = true;
        }
    }

    protected virtual void ExecuteCutscene()
    {

    }
}

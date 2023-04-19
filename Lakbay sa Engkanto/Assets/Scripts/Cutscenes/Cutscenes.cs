using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cutscenes : MonoBehaviour
{
    [SerializeField] protected string id;
    protected Collider2D playerCollider;
    private bool hasActivated;

    private void OnEnable()
    {
        SingletonManager.Get<DebugEvents>().OnEnableCutscene += x => hasActivated = !x;

        if (!SingletonManager.Get<DebugEvents>().IsCutsceneEnabled && !hasActivated)
        {
            hasActivated = true;
            return;
        }
    }

    private void OnDisable()
    {
        SingletonManager.Get<DebugEvents>().OnEnableCutscene -= x => hasActivated = !x;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SingletonManager.Get<PlayerManager>().PlayerData.StringList.Contains(id))
        {
            Destroy(gameObject);
            return;
        }

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

            SingletonManager.Get<PlayerManager>().PlayerData.AddString(id);

            // Trigger the Cutscene
            ExecuteCutscene();

            hasActivated = true;
        }
    }

    protected virtual void ExecuteCutscene()
    {

    }
}

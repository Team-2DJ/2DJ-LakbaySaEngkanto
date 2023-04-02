using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]   // Requires the Box Collider to Function Correctly; 
public class MangoSeed : MonoBehaviour
{
    [SerializeField] private string effectId;
    [SerializeField] private string soundId;
    
    private Collider2D playerCollider;


    private void Start()
    {
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            // Play Sound
            SingletonManager.Get<AudioManager>().PlayOneShot(soundId);

            // Spawn VFX
            SingletonManager.Get<ObjectPooler>().SpawnFromPool(effectId, transform.position, Quaternion.identity, Vector3.one, this.transform);

            SingletonManager.Get<AudioManager>().Stop("Boss BGM");

            // Call Seed Collected Event
            SingletonManager.Get<GameEvents>().SeedCollected();

            // Destroy this GameObject
            Destroy(gameObject);
        }
    }
}

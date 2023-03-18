using UnityEngine;

public class PodiumBooks : Item
{
    [SerializeField] private ItemData bookData;

    protected override void ItemCollected()
    {
        SingletonManager.Get<PlayerEvents>().PlayerCollectPodiumBook(bookData);
        Destroy(this.gameObject);
    }
}

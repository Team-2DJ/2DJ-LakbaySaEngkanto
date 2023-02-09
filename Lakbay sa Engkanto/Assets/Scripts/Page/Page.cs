using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : Item
{
    protected override void ItemCollected()
    {
        SingletonManager.Get<GameEvents>().PlayerCollectItem(id);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    List<GameObject> PagesCollected = new List<GameObject>();

    /// <summary>
    /// Accepts a page object, adds the gameObject of page into the player's inventory
    /// </summary>
    public void CollectPage(Page page)
    {
        PagesCollected.Add(page.gameObject);
    }

}

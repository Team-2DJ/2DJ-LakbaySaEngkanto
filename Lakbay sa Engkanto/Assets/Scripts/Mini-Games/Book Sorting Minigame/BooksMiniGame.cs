using UnityEngine;
using System.Linq;

public class BooksMiniGame : MonoBehaviour
{
    [SerializeField] string id;
    [SerializeField] private BookSlot[] bookSlots;

    public void CheckOrder()
    {
        if (bookSlots.All(bookSlot => bookSlot.IsRight))
        {
            SingletonManager.Get<GameEvents>().PlayerCollectItem(id);
        }
        else
        {
            Debug.Log("RESETEVERYTHING");
        }
    }
}

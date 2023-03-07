using UnityEngine;
using System.Linq;

public class BooksMiniGame : MonoBehaviour
{
    [SerializeField] string id;                                     // Object ID 
    [SerializeField] private BookSlot[] bookSlots;                  // BookSlots Array

    /// <summary>
    /// Checks bookslot order whether they are correct or not. 
    /// </summary>
    public void CheckOrder()
    {
        // if all bookSlot in the bookSlots array have the right books, 
        // then call true, else if false 
        if (bookSlots.All(bookSlot => bookSlot.IsRight))
        {
            // Open the Door corresponding to this GameObjects ID.
            SingletonManager.Get<GameEvents>().OpenDoor(id);

            // Invoke a false boolean that listeners will receive.
            SingletonManager.Get<GameEvents>().SetCondition(false);
        }
        else
        {
            // Invoke a true boolean that listeners will receive. 
            SingletonManager.Get<GameEvents>().SetCondition(true);
        }
    }
}

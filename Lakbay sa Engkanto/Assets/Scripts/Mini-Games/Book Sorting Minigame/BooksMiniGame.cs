using UnityEngine;
using System.Linq;

public class BooksMiniGame : MonoBehaviour
{
    [SerializeField] private BookSlot[] bookSlots;


    private bool Checker()
    {
        if (bookSlots.All(bookSlot => bookSlot.IsRight))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

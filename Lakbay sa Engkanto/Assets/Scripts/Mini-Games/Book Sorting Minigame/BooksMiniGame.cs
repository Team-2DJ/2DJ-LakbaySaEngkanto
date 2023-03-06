using UnityEngine;
using System.Linq;

public class BooksMiniGame : MonoBehaviour
{
    [SerializeField] private BookSlot[] bookSlots;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Checker())
        {
            Debug.Log("All is Right");
        }
        else
        {
            Debug.Log("All is Wrong");
        }
    }

    private bool Checker()
    {
        return bookSlots.All(bookslot => bookslot.IsRight);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryBox : MonoBehaviour
{
    private Collider2D playerCollider;
    [SerializeField] private GameObject textBox;
    
    void Start()
    {
        textBox.SetActive(false);
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision == playerCollider)
        {
            textBox.SetActive(true);
        }
        
    }

  
   
    private void OnTriggerExit2D(Collider2D other)
    {
        textBox.SetActive(false);

    }
}

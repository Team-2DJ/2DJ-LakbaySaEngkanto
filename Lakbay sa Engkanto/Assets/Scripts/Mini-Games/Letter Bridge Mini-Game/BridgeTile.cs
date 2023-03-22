using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BridgeTile : MonoBehaviour
{
    public bool IsCorrect { get; private set; }
    
    [SerializeField] private TextMeshProUGUI letterText;
    [SerializeField] private GameObject popEffect;

    private GameObject player;

    private string alphabet = "abcdefghijklmnopqrstuvwxyz";

    // Start is called before the first frame update
    void Start()
    {
        player = SingletonManager.Get<PlayerManager>().Player.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (IsCorrect)
            return;
        
        if (other.gameObject == player)
        {
            // Play Popping Sound
            SingletonManager.Get<AudioManager>().PlayOneShot("Crumble");
            
            // Spawn Popping VFX
            Transform effect = Instantiate(popEffect, transform.position, Quaternion.identity, this.transform).transform;
            effect.SetParent(null);
            
            // Destroy this GameObject
            Destroy(gameObject);
        }
            
    }

    public void SetChance(int value, string word)
    {
        int chance = Random.Range(0, 100);

        // Check if chance is within probability Value
        if (chance < value)
        {
            IsCorrect = true;

            gameObject.layer = LayerMask.NameToLayer("Ground");

            SetText(word);
        }
        else
        {
            IsCorrect = false;

            gameObject.layer = LayerMask.NameToLayer("Default");

            // Remove all letters in the alphabet that are associated with the word
            foreach (char l in word)
                alphabet = alphabet.Replace(l.ToString(), "");

            SetText(alphabet);
        }
    }

    void SetText(string value)
    {
        char letter = value[Random.Range(0, value.Length)];

        letterText.text = letter.ToString();
    }
}

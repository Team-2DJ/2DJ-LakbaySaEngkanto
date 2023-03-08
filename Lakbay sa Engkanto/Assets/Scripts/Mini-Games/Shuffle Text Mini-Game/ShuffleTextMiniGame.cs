using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShuffleTextMiniGame : MonoBehaviour
{
    [System.Serializable]
    public struct ShuffleTextData
    {
        public Sprite AnswerImage;
        public string TextAnswer;
    }

    [Header("References")]
    [SerializeField] private Image picture;
    [SerializeField] private Transform letterSlotHolder;
    [SerializeField] private Transform letterItemHolder;

    [Space]
    [SerializeField] private GameObject letterSlot;
    [SerializeField] private GameObject letterItem;

    [SerializeField] private ShuffleTextData[] words; 

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        int randomIndex = Random.Range(0, words.Length);
        
        picture.sprite = words[randomIndex].AnswerImage;

        foreach(char c in words[randomIndex].TextAnswer)
        {
            Transform slot = Instantiate(letterSlot, letterSlotHolder.position, Quaternion.identity).transform;
            Transform item = Instantiate(letterItem, letterItemHolder.position, Quaternion.identity).transform;

            slot.SetParent(letterSlotHolder);
            item.SetParent(letterItemHolder);
        }
    }

    public void Shuffle()
    {
        Debug.Log("Shuffling...");
    }
    
}

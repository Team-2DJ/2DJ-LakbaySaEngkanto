using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterBridgeMiniGame : MonoBehaviour
{
    enum Direction
    {
        LEFT,
        RIGHT
    }
    
    [Header("References")]
    [SerializeField] private Transform startingPoint;
    [SerializeField] private LetterItem letters;
    [SerializeField] private TextMeshProUGUI wordToDisplay;
    [SerializeField] private Direction direction;

    [Header("Numbers")]
    [SerializeField] private string wordAnswer;
    [SerializeField] private int rows;
    [SerializeField] private float spacing;
    [SerializeField] [Range(0, 100)] private int probability;
    [SerializeField] private int guaranteedIndex;

    private int currentIndex;
    private List<LetterItem> letterList = new();
    
    // Start is called before the first frame update
    void Start()
    {
        currentIndex = Random.Range(0, guaranteedIndex + 1);

        if (direction == Direction.LEFT)
            spacing *= -1;
        else if (direction == Direction.RIGHT)
            spacing *= 1;

        // Display Word Answer to the "Word To Display" Text
        wordToDisplay.text = wordAnswer;
        
        for (int i = 0; i < rows; i++)
        {
            LetterItem tile = Instantiate(letters.gameObject, startingPoint.position, Quaternion.identity).GetComponent<LetterItem>();

            // Add Letter Tile to the List
            letterList.Add(tile);

            // X Position
            float x = startingPoint.position.x + (i * spacing);

            // Set Position of LetterItem
            tile.transform.position = new Vector2(x, startingPoint.position.y);
        }

        foreach (LetterItem item in letterList)
        {
            if (currentIndex-- <= 0)
            {
                item.SetChance(100, wordAnswer);
                currentIndex = Random.Range(0, guaranteedIndex + 1);
            }
            else
            {
                item.SetChance(probability, wordAnswer);
            }
        }
    }
}

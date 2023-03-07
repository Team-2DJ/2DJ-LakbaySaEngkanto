using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterBridgeMiniGame : MonoBehaviour
{
    enum Direction
    {
        LEFT = -1,
        RIGHT = 1
    }
    
    [Header("References")]
    [SerializeField] private Transform startingPoint;
    [SerializeField] private LetterTile letters;
    [SerializeField] private TextMeshProUGUI wordToDisplay;
    
    [Header("Properties")]
    [SerializeField] private Direction direction;
    [SerializeField] private string wordAnswer;
    [SerializeField] private int rows;
    [SerializeField] private float spacing;
    [SerializeField] [Range(0, 100)] private int probability;
    [SerializeField] private int defaultPityAmount;

    private int pityCounter;
    private List<LetterTile> letterList = new();
    
    // Start is called before the first frame update
    void Start()
    {
        pityCounter = Random.Range(0, defaultPityAmount + 1);

        // Display Word Answer to the "Word To Display" Text
        wordToDisplay.text = wordAnswer;

        SpawnLetterTiles();
        SetProbability();
    }

    void SpawnLetterTiles()
    {
        for (int i = 0; i < rows; i++)
        {
            // Set Position
            Vector2 position = new Vector2(startingPoint.position.x + (i * (spacing * (float)direction)), startingPoint.position.y);

            // Spawn the Letter Tile and Set Parent to this GameObject
            LetterTile letterTile = Instantiate(letters.gameObject, position, Quaternion.identity).GetComponent<LetterTile>();
            letterTile.transform.SetParent(transform);

            // Add Letter Tile to the List
            letterList.Add(letterTile);
        }
    }

    void SetProbability()
    {
        foreach (LetterTile item in letterList)
        {
            if (pityCounter-- <= 0)
            {
                item.SetChance(100, wordAnswer);
                pityCounter = Random.Range(0, defaultPityAmount + 1);
            }
            else
            {
                item.SetChance(probability, wordAnswer);
            }
        }
    }
}

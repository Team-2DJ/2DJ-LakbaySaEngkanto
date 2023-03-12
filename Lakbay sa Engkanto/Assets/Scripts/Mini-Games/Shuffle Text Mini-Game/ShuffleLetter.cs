using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShuffleLetter : MonoBehaviour
{
    public ShuffleTextMiniGame ShuffleTextMiniGame { get; private set; }                // ShuffleTextMiniGame Class Reference

    [Header("Refrences")]
    [SerializeField] private TextMeshProUGUI letterText;                                // Letter Text Reference

    private Button button;                                                              // Button Component Reference
    private char letterChar;                                                            // Letter Character Value                                                 

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    public void Initialize(char letter, ShuffleTextMiniGame reference)
    {
        ShuffleTextMiniGame = reference;
        letterText.text = letter.ToString();
        letterChar = letter;
    }

    /// <summary>
    /// Occurs When Player Pressed this Shuffle Letter
    /// </summary>
    public void OnPressed()
    {
        ShuffleTextMiniGame.TypeLetter(letterChar);
        SetButtonInteractable(false);
    }

    /// <summary>
    /// Makes the Button Interactive or Not
    /// </summary>
    /// <param name="value"></param>
    public void SetButtonInteractable(bool value)
    {
        button.interactable = value;
    }
}

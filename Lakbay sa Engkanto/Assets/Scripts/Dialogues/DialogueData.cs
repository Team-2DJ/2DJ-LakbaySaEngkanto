using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueData
{
    public Sprite CharacterImage;               // Image of the Character Soeaking
    public string Name;                         // Character's Name
    public string AudioID;                      // Character's Voice

    [TextArea(3, 10)]
    public string[] Sentences;                  // Character's Sentences
}

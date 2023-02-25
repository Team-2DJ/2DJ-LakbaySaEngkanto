using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueData
{
    public Sprite CharacterImage;
    public string Name;

    [TextArea(3, 10)]
    public string[] Sentences;
}

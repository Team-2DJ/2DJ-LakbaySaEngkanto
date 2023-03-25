using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueData
{
    public string Name;                                        // Character's Name
    public string AudioID;                                     // Character's Voice
    public CharacterDialogue[] CharacterDialogues;             // Character's Sentences and Designated Emotions

    [System.Serializable]
    public struct CharacterDialogue
    {
        public Sprite Image;             // Image of the Character Speaking
                                                  // to Indicate Current Emotion
        
        [TextArea(3, 10)]
        public string Sentences;                  // Character's Sentences
    }
}

using UnityEngine;

[System.Serializable]
public struct TutorialData
{
    public Sprite ReferenceImage;

    [TextArea(3, 10)]
    public string Description;
}

using UnityEngine;
using System;

public class DebugEvents : MonoBehaviour
{
    public event Action<bool> OnEnableCutscene;

    public event Action<bool> OnEnableDialogue;

    public bool IsCutsceneEnabled { get; private set; }
    public bool IsDialogueEnabled { get; private set; }

    private void Awake()
    {
        SingletonManager.Register(this);
        ResetData();
    }

    // Start is called before the first frame update
    public void EnableCutscene(bool condition)
    {
        IsCutsceneEnabled = condition;
        OnEnableCutscene?.Invoke(IsCutsceneEnabled);
    }

    public void EnableDialogue(bool condition)
    {
        IsDialogueEnabled = condition;
        OnEnableDialogue?.Invoke(IsDialogueEnabled);
    }

    public void ResetData()
    {
        IsCutsceneEnabled = true;
        IsDialogueEnabled = true;
    }
}

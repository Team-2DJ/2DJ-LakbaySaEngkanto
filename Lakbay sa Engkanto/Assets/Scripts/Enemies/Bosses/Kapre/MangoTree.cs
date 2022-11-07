using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MangoTree : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SingletonManager.Get<GameEvents>().OnPlayerCollectItem += GrowMangoTree;
    }

    private void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnPlayerCollectItem -= GrowMangoTree;
    }

    void GrowMangoTree()
    {
        var changeScale = new Vector3(0, 0.01f, 0);
        var changePos = new Vector3(0, 0.01f, 0f);

        while (transform.position.y < 3f)
        {
            transform.localScale += changeScale;
            transform.position += changePos;
        }

        Debug.Log("Tree is Growing");
    }
}

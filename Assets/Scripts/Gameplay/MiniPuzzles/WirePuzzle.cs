using System;
using UnityEngine;
using WallThrough.UI;

public class WirePuzzle : MiniPuzzle
{
    private GameObject parentObject; // Parent object for FlashCode
    bool complete = false;

    public override void Initialize(int[] colourCodes)
    {
        if (!colourCodeManager)
        {
            Debug.LogWarning("No ColourCodeManager found");
            return;
        }
        parentObject = colourCodeManager.Initialize(colourCodes);
        parentObject.SetActive(false);
    }

    public void OnPuzzleComplete()
    {
        if (!parentObject) Debug.LogWarning("No parent object");
        else if (!colourCodeManager) Debug.LogWarning("No ColourCodeManager");
        else if (!complete)
        {
            StartCoroutine(colourCodeManager.FlashCode(parentObject));
            complete = true;
        }
    }
}

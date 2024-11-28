using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WallThrough.UI;

public class WirePuzzle : MiniPuzzle
{
    [SerializeField] private ColourCodeManager colourCodeManager; // Reference to the ColourCodeManager
    private GameObject parentObject; // Store reference to parentObject for FlashCode
    private void Awake()
    {
        if (!colourCodeManager) colourCodeManager = FindObjectOfType<ColourCodeManager>();
    }

    public override void Initialize(int[] colourCodes)
    {
        if (!colourCodeManager)
        {
            Debug.LogWarning("No colourcodemanager found");
            return;
        }
        parentObject = colourCodeManager.Initialize(colourCodes);

        parentObject.SetActive(false);
    }
    public void OnPuzzleComplete()
    {
        if (parentObject == null)
        {
            Debug.Log("no parent object");
        }
        if (colourCodeManager == null)
        {
            Debug.Log("no colourcodemanager");
        }

        StartCoroutine(colourCodeManager.FlashCode(parentObject)); // Pass parentObject to FlashCode
    }
}

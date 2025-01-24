using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WallThrough.UI;

public class MiniPuzzlePushPuzzle : MiniPuzzle
{
    private GameObject parentObject; // Parent object for FlashCode

    [SerializeField] private GameObject[] pushableCubePressurePlates;
    private int currentlyPushedPressurePlates = 0;
    private bool complete = false;

    public override void Initialize(int[] colourCodes)
    {
        if (!colourCodeManager)
        {
            Debug.LogWarning("No ColourCodeManager found");
            return;
        }
        parentObject = colourCodeManager.Initialize(colourCodes);
        parentObject.SetActive(false);
        InitializePressurePlateLogic();
    }

    private void Update()
    {
        if (currentlyPushedPressurePlates == pushableCubePressurePlates.Length && !complete)
        {
            complete = true;
            OnPuzzleComplete();
        }
    }

    private void InitializePressurePlateLogic()
    {
        currentlyPushedPressurePlates = 0;
    }

    public void AddPushedPlate() => currentlyPushedPressurePlates += 1;
    public void RemovedPushedPlate() => currentlyPushedPressurePlates -= 1;

    public void OnPuzzleComplete()
    {
        if (!parentObject) Debug.LogWarning("No parent object");
        else if (!colourCodeManager) Debug.LogWarning("No ColourCodeManager");
        else StartCoroutine(colourCodeManager.FlashCode(parentObject));
    }
}

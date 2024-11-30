using UnityEngine;
using WallThrough.UI;
using WallThrough.Utility;

public abstract class MiniPuzzle : MonoBehaviour
{
    [SerializeField] protected ColourCodeManager colourCodeManager;

    private void Awake()
    {
        Util.FindOrLogError(this, ref colourCodeManager, "ColourCodeManager is missing. Assign it or ensure one exists in the scene.");
    }


    public abstract void Initialize(int[] colourCodes);
}
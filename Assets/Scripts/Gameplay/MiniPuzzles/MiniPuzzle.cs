using UnityEngine;
using WallThrough.UI;

public abstract class MiniPuzzle : MonoBehaviour
{
    [SerializeField] protected ColourCodeManager colourCodeManager;

    private void Awake() => colourCodeManager ??= FindObjectOfType<ColourCodeManager>();

    public abstract void Initialize(int[] colourCodes);
}
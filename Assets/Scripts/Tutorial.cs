using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using WallThrough.Gameplay.Pawn;
using WallThrough.UI;
using WallThrough.Gameplay;

public class Tutorial : MonoBehaviour
{
    public enum TutorialState
    {
        WASD,
        FindColour,
        WalkNearDoor,
        CollectCollectables,
        Exit,
        Neutral
    }

    [SerializeField] private TutorialState state;

    [SerializeField] private Movement playerMovement;
    [SerializeField] private UIManager uiManager;

    [Header("TutorialUI")]
    [SerializeField] private GameObject WASDText;
    [SerializeField] private GameObject findTheColourCodeText;
    [SerializeField] private GameObject walkNearDoorText;
    [SerializeField] private GameObject collectCollectableText;
    [SerializeField] private GameObject ExitPortalText;

    private void OnEnable()
    {
        TutorialHelper.OnColourCodeFind += UpdateState;
    }

    private void Start()
    {
        state = TutorialState.WASD; 

        if (uiManager == null)
        {
            try
            {
                uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
            }
            catch
            {
                Debug.LogWarning("Tutorial Could not find UI Manager");
            }
        }
    }

    private void Update()
    {
        switch (state)
        {
            case TutorialState.WASD:
                WASDText.SetActive(true);
                if (playerMovement.GetCurrentVelocity().x > 0 || playerMovement.GetCurrentVelocity().z > 0)
                {
                    WASDText.SetActive(false);
                    state = TutorialState.FindColour;
                }
                break;
            case TutorialState.FindColour:
                uiManager.DisplayTextGameObject(findTheColourCodeText, true, 2f);
                state = TutorialState.Neutral;
                break;
            case TutorialState.WalkNearDoor:
                uiManager.DisplayTextGameObject(walkNearDoorText, true, 2f);
                state = TutorialState.Neutral;
                break;
            case TutorialState.CollectCollectables:
                uiManager.DisplayTextGameObject(findTheColourCodeText, true, 2f);
                state = TutorialState.Neutral;
                break;
            case TutorialState.Exit:
                uiManager.DisplayTextGameObject(findTheColourCodeText, true, 2f);
                state = TutorialState.Neutral;
                break;
            case TutorialState.Neutral:
                break;
            default:
                break;
        }
    }

    private void UpdateState(TutorialState state)
    {
        this.state = state;
    }
}

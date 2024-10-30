using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using WallThrough.Gameplay.Pawn;

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

    [SerializeField] private TMP_Text WASDText;
    [SerializeField] private TMP_Text findTheColourCodeText;
    [SerializeField] private TMP_Text walkNearDoorText;
    [SerializeField] private TMP_Text collectCollectableText;
    [SerializeField] private TMP_Text ExitPortalText;

    [SerializeField] private TutorialState state;

    [SerializeField] private Movement playerMovement;

    private void Start()
    {
        state = TutorialState.WASD; 
    }

    private void Update()
    {
        switch (state)
        {
            case TutorialState.WASD:
                WASDText.gameObject.SetActive(true);     
                if(playerMovement.GetCurrentVelocity().x > 0 || playerMovement.GetCurrentVelocity().z > 0)
                {
                    WASDText.gameObject.SetActive(false);
                    state = TutorialState.FindColour;
                }
                break;
            case TutorialState.FindColour:
                StartCoroutine(FlashText(findTheColourCodeText.gameObject, 4));
                state = TutorialState.Neutral;
                break;
            case TutorialState.WalkNearDoor:
                StartCoroutine(FlashText(walkNearDoorText.gameObject, 2));
                state = TutorialState.Neutral;
                break;
            case TutorialState.CollectCollectables:
                StartCoroutine(FlashText(collectCollectableText.gameObject, 2));
                state = TutorialState.Neutral;
                break;
            case TutorialState.Exit:
                StartCoroutine(FlashText(ExitPortalText.gameObject, 2));
                state = TutorialState.Neutral;
                break;
            case TutorialState.Neutral:
                break;
            default:
                break;
        }
    }

    private IEnumerator FlashText(GameObject textGameobject, float timeToWait)
    {
        textGameobject.SetActive(true);
        yield return new WaitForSeconds(timeToWait);
        textGameobject.SetActive(false);
    }
}

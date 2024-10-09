using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WallThrough.Gameplay.Interactable;



public class PlayerInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // If the collision object has an interactable component, trigger the interaction it has
        IInteractable interactable = other.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.InteractionStart();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.InteractionEnd();
        }
    }
}

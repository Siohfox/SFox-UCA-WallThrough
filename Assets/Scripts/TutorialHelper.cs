using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tutorial;

public class TutorialHelper : MonoBehaviour
{
    public static event Action<TutorialState> OnColourCodeFind; // currently for tutorial

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnColourCodeFind?.Invoke(TutorialState.WalkNearDoor);
        }
    }
}

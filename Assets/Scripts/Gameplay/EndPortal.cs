using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WallThrough.Gameplay.Interactable;
using TMPro;

namespace WallThrough.Gameplay
{
    public class EndPortal : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private GameObject winText;
        private ObjectiveManager objectiveManager;

        private void Start()
        {
            objectiveManager = FindObjectOfType<ObjectiveManager>().GetComponent<ObjectiveManager>();
        }

        public void InteractionStart()
        {
            if (objectiveManager.CheckObjectives())
                winText.SetActive(true);
        }

        public void InteractionEnd()
        {
            // stuff
        }
    }
}


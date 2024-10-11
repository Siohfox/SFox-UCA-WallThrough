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
        [SerializeField]
        private GameObject adviceText;
        private ObjectiveManager objectiveManager;

        private void Start()
        {
            objectiveManager = FindObjectOfType<ObjectiveManager>().GetComponent<ObjectiveManager>();
        }

        public void InteractionStart()
        {
            if (objectiveManager.CheckObjectives())
                winText.SetActive(true);
            else
                StartCoroutine(GiveAdvice());
        }

        public void InteractionEnd()
        {
            // stuff
        }

        private IEnumerator GiveAdvice()
        {
            adviceText.SetActive(true);
            yield return new WaitForSeconds(2f);
            adviceText.SetActive(false);
        }
    }
}


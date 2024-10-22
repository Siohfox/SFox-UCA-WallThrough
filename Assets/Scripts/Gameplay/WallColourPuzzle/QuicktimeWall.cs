using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WallThrough.Gameplay.Interactable;
using WallThrough.Audio;
using WallThrough.Graphics;

namespace WallThrough.Gameplay
{
    /// <summary>
    /// Represents a quick time event wall that players can interact with.
    /// </summary>
    public class QuickTimeWall : Objective, IInteractable
    {
        [SerializeField] private GameObject quickTimeMenu;
        [SerializeField] private GameObject failCross;
        [SerializeField] private Animator wallAnimator;
        [SerializeField] private AudioClip wallOpenClip;
        [SerializeField] private AudioClip codeSuccess;
        [SerializeField] private AudioClip codeFail;
        [SerializeField] private MiniPuzzleColourCube miniPuzzle;

        private int[] colourCode;
        private string colourString;
        private bool isInteracting = false;
        private int requiredInputs = 4;

        public FloodManager floodManager; 
        public Room room;

        /// <summary>
        /// Initializes references and sets the objective type.
        /// </summary>
        private void Awake()
        {
            quickTimeMenu = FindObjectOfType<QuickTimeMenu>().gameObject;
            failCross = GameObject.Find("FailCross");
            floodManager = GameObject.Find("FloodManager").GetComponent<FloodManager>();
            var src = GetComponent<AudioSource>();

            if (!src)
            {
                Debug.LogWarning("No audio source found");
            }

            SetObjectiveType(ObjectiveType.WallPuzzle);
        }

        /// <summary>
        /// Initializes the quick time event when the script instance is loaded.
        /// </summary>
        private void Start()
        {
            InitializeQuickTimeEvent();
        }

        /// <summary>
        /// Sets up the quick time event, generates the colour code, and registers the objective.
        /// </summary>
        private void InitializeQuickTimeEvent()
        {
            isInteracting = false;
            quickTimeMenu.SetActive(false);
            failCross.SetActive(false);
            GenerateColourCode();

            if (colourCode.Length > 0)
            {
                ObjectiveManager.Instance.RegisterObjective(this, colourCode);
                miniPuzzle.InstantiateCubes(colourCode); // Instantiate cubes when generating color code
            }

            DebugColourInfo();
        }

        /// <summary>
        /// Generates a random colour code for the quick time event.
        /// </summary>
        private void GenerateColourCode()
        {
            colourCode = new int[UnityEngine.Random.Range(2, 6)];
            requiredInputs = colourCode.Length;

            for (int i = 0; i < colourCode.Length; i++)
            {
                colourCode[i] = UnityEngine.Random.Range(0, ObjectiveManager.Instance.colourData.Count);
            }
        }

        /// <summary>
        /// Logs the colour names and their corresponding integer values.
        /// </summary>
        private void DebugColourInfo()
        {
            List<string> colourNames = new();
            foreach (int code in colourCode)
            {
                ColourData colourData = ObjectiveManager.Instance.GetColourData(code);
                colourNames.Add(colourData.colourName);
            }

            colourString = string.Join(" ", colourNames);
            Debug.Log("Colour Names: " + colourString);
        }

        /// <summary>
        /// Updates the state of the quick time event.
        /// </summary>
        private void Update()
        {
            if (!isInteracting) return;
        }

        /// <summary>
        /// Activates the quick time menu for player interaction.
        /// </summary>
        /// <param name="menu">The quick time menu to activate.</param>
        private void ActivateQuickTimeMenu(QuickTimeMenu menu)
        {
            menu.SetCurrentWall(this, requiredInputs); // Pass the wall reference.
            menu.gameObject.SetActive(true);
            isInteracting = true;
        }

        /// <summary>
        /// Deactivates the quick time menu.
        /// </summary>
        private void DeactivateQuickTimeMenu()
        {
            quickTimeMenu.SetActive(false);
            isInteracting = false;
        }

        /// <summary>
        /// Compares the player's input with the generated colour code.
        /// </summary>
        /// <param name="codeInput">List of integers representing the player's input.</param>
        public void CompareCodes(List<int> codeInput)
        {
            for (int i = 0; i < colourCode.Length; i++)
            {
                if (codeInput[i] != colourCode[i])
                {
                    WallFail();
                    return;
                }
            }

            WallSuccess();
        }

        /// <summary>
        /// Handles successful input of the code.
        /// </summary>
        private void WallSuccess()
        {
            DeactivateQuickTimeMenu();
            wallAnimator.SetBool("Open", true);
            GetComponentInParent<Collider>().enabled = false;

            if (!IsCompleted)
            {
                base.CompleteObjective();
                ObjectiveManager.UpdateCompletedObjectives(ObjectiveManager.Instance.GetCompeletedObjectives().ToString());
            }

            AudioManager.Instance.PlaySound(codeSuccess, 1.0f, GetComponent<AudioSource>());
            AudioManager.Instance.PlaySound(wallOpenClip, 1.0f, GetComponent<AudioSource>());
            CameraShake.Instance.ShakeCamera(4f, 2f);
            floodManager.OpenDoor(room);
        }

        /// <summary>
        /// Handles failed input of the code.
        /// </summary>
        private void WallFail()
        {
            if (failCross)
            {
                StartCoroutine(FailCrossShow());
            }

            Debug.Log("Input was incorrect, correct input should've been: " + colourString);
        }

        /// <summary>
        /// Displays the fail cross indicator for a brief moment.
        /// </summary>
        /// <returns>IEnumerator for coroutine.</returns>
        private IEnumerator FailCrossShow()
        {
            failCross.SetActive(true);
            AudioManager.Instance.PlaySound(codeFail, 1.0f, GetComponent<AudioSource>());
            yield return new WaitForSeconds(1);
            failCross.SetActive(false);
        }

        /// <summary>
        /// Starts the interaction with the quick time event.
        /// </summary>
        public void InteractionStart()
        {
            ActivateQuickTimeMenu(quickTimeMenu.GetComponent<QuickTimeMenu>());
        }

        /// <summary>
        /// Ends the interaction with the quick time event.
        /// </summary>
        public void InteractionEnd()
        {
            DeactivateQuickTimeMenu();
            quickTimeMenu.GetComponent<QuickTimeMenu>().ClearInput();
        }
    }
}

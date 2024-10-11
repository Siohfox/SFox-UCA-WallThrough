using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using WallThrough.Gameplay.Interactable;
using WallThrough.Audio;

namespace WallThrough.Gameplay
{
    public class QuickTimeWall : Objective, IInteractable
    {
        private GameObject quickTimeMenu;
        private GameObject failCross;

        public FloodManager floodManager; // Reference to the FloodManager
        public Room room; // Reference to the room connected to this door

        [SerializeField]
        private Animator wallAnimator;


        [SerializeField]
        private AudioClip wallOpenClip;
        [SerializeField]
        private AudioClip codeSuccess;
        [SerializeField]
        private AudioClip codeFail;

        // Enum representing the colors
        public enum ColourMap { Red, Orange, Yellow, Green, Blue, Purple };

        private int[] colourCode;
        private string colourString;
        private bool isInteracting = false;

        private int requiredInputs = 4;

        private void Awake()
        {
            quickTimeMenu = FindObjectOfType<QuickTimeMenu>().gameObject;
            failCross = GameObject.Find("FailCross").gameObject;
            floodManager = GameObject.Find("FloodManager").GetComponent<FloodManager>();
            src = GetComponent<AudioSource>();
            if (!src) Debug.LogWarning("No audio source found");
        }

        // Start is called before the first frame update
        private void Start()
        {
            InitializeQuickTimeEvent();
        }

        // Initialize the quick time event
        private void InitializeQuickTimeEvent()
        {
            isInteracting = false;
            quickTimeMenu.SetActive(false);
            failCross.SetActive(false);
            GenerateColourCode();
            DebugColourInfo();
        }

        // Generate a random colour code
        private void GenerateColourCode()
        {
            colourCode = new int[UnityEngine.Random.Range(2, 6)];
            requiredInputs = colourCode.Length;
            for (int i = 0; i < colourCode.Length; i++)
            {
                colourCode[i] = UnityEngine.Random.Range(0, Enum.GetValues(typeof(ColourMap)).Length);
            }
        }

        // Log colour names and their integer values
        private void DebugColourInfo()
        {
            List<string> colourNames = new();
            foreach (int code in colourCode)
            {
                colourNames.Add(Enum.GetName(typeof(ColourMap), code));
            }

            colourString = string.Join(" ", colourNames);
            //string intResult = string.Join(" ", colourCode);
            Debug.Log("Colour Names: " + colourString);
            //Debug.Log("Integer Values: " + intResult);
        }

        // Update is called once per frame
        private void Update()
        {
            if (!isInteracting) return;
        }

        // Activate the quick time menu
        private void ActivateQuickTimeMenu()
        {
            quickTimeMenu.SetActive(true);
            isInteracting = true;
            quickTimeMenu.GetComponent<QuickTimeMenu>().SetCurrentWall(gameObject, requiredInputs);
        }

        // Deactivate the quick time menu
        private void DeactivateQuickTimeMenu()
        {
            quickTimeMenu.SetActive(false);
            isInteracting = false;
        }

        // Compare the input code with the generated colour code
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

            //Debug.Log("Input was correct, destroying door");
            WallSuccess();
        }

        private void WallSuccess()
        {
            DeactivateQuickTimeMenu();
            wallAnimator.SetBool("Open", true);
            GetComponentInParent<Collider>().enabled = false;
            if (!IsCompleted)
            {
                base.CompleteObjective();
            }
            AudioManager.Instance.PlaySound(codeSuccess, 1.0f, src);
            AudioManager.Instance.PlaySound(wallOpenClip, 1.0f, src);
            floodManager.OpenDoor(room);
        }

        private void WallFail()
        {
            if(failCross)
            {
                StartCoroutine(FailCrossShow());
            }
            Debug.Log("Input was incorrect, correct input should've been: " + colourString);
        }

        private IEnumerator FailCrossShow()
        {
            failCross.SetActive(true);
            AudioManager.Instance.PlaySound(codeFail, 1.0f, src);
            yield return new WaitForSeconds(1);
            failCross.SetActive(false);
        }

        public void InteractionStart()
        {
            ActivateQuickTimeMenu();
        }

        public void InteractionEnd()
        {
            DeactivateQuickTimeMenu();
            quickTimeMenu.GetComponent<QuickTimeMenu>().ClearInput();
        }
    }
}
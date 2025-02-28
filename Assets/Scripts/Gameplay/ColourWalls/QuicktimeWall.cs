using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WallThrough.Gameplay.Interactable;
using WallThrough.Audio;
using WallThrough.Graphics;
using WallThrough.Generation;

namespace WallThrough.Gameplay
{
    /// <summary>
    /// Represents a quick time event wall that players can interact with.
    /// </summary>
    public class QuickTimeWall : Objective, IInteractable
    {
        [SerializeField] private QuickTimeMenu quickTimeMenu;
        [SerializeField] private GameObject failCross;
        [SerializeField] private Animator wallAnimator;
        [SerializeField] private AudioClip wallOpenClip, codeSuccess, codeFail;
        [SerializeField] private MiniPuzzle associatedMiniPuzzle;
        [SerializeField] private bool usingProceduralGeneration = true;
        [SerializeField] private CountdownTimer countdownTimer;
        public float doorTimeWorth = 20.0f;

        private UI.ColourCodeManager colourCodeManager;

        private int[] colourCode;
        private int requiredInputs;

        private void Awake()
        {
            doorTimeWorth = 20.0f;

            if (!GetComponent<AudioSource>())
                Debug.LogWarning("No audio source found");
            SetObjectiveType(ObjectiveType.WallPuzzle);

            if(!quickTimeMenu)
            {
                try
                {
                    quickTimeMenu = FindObjectOfType<QuickTimeMenu>(true);
                }
                catch
                {
                    Debug.LogError("No quick time menu found");
                }
            }

            if (!failCross)
            {
                try
                {
                    failCross = GameObject.Find("GameplayCanvas").transform.Find("FailCross").gameObject;
                }
                catch
                {
                    Debug.LogError("No failCross found");
                }
            }

            if (!colourCodeManager)
            {
                colourCodeManager = FindObjectOfType<UI.ColourCodeManager>();
            }

            if (!countdownTimer)
            {
                countdownTimer = FindObjectOfType<CountdownTimer>();
            }
        }

        private void Start() => InitializeQuickTimeEvent();

        private void InitializeQuickTimeEvent()
        {
            quickTimeMenu.DeactivateQuickTimeMenu();
            failCross.SetActive(false);
            GenerateColourCode();

            if (usingProceduralGeneration) associatedMiniPuzzle = transform.parent.parent.GetComponent<RoomBehaviour>().GetRoomMiniPuzzle();

            if (colourCode.Length > 0 && associatedMiniPuzzle)
            {
                ObjectiveManager.Instance.RegisterObjective(this, colourCode);
                if (associatedMiniPuzzle) associatedMiniPuzzle.Initialize(colourCode);  
            }

            // DebugColourInfo();
        }

        private void GenerateColourCode()
        {
            requiredInputs = (colourCode = new int[Random.Range(2, 6)]).Length;
            for (int i = 0; i < requiredInputs; i++)
                colourCode[i] = Random.Range(0, ObjectiveManager.Instance.colourData.Count);
        }

        // Code to help debug colour info if needed
        private void DebugColourInfo()
        {
            List<string> colourNames = new();
            foreach (int code in colourCode)
                colourNames.Add(ObjectiveManager.Instance.GetColourData(code).colourName);
            Debug.Log("Colour Names: " + string.Join(" ", colourNames));
        }

        private void ActivateQuickTimeMenu()
        {
            quickTimeMenu.SetCurrentWall(this, requiredInputs);
            quickTimeMenu.gameObject.SetActive(true);
        }

        public void CompareCodes(List<int> codeInput)
        {
            for (int i = 0; i < colourCode.Length; i++)
                if (codeInput[i] != colourCode[i]) { WallFail(); return; }
            WallSuccess();
        }

        private void WallSuccess()
        {
            quickTimeMenu.DeactivateQuickTimeMenu();
            if (!wallAnimator) Debug.LogWarning("No animator");
            wallAnimator.SetBool("Open", true);
            GetComponentInParent<Collider>().enabled = false;
            if (!IsCompleted) base.CompleteObjective();
            PlaySuccessSounds();
            CameraShake.Instance.ShakeCamera(4f, 2f, "SixDShake");
            if(countdownTimer) countdownTimer.AddTime(doorTimeWorth);
            StartCoroutine(colourCodeManager.ClearHeldCode());
        }

        private void PlaySuccessSounds()
        {
            var audioSource = GetComponent<AudioSource>();
            AudioManager.Instance.PlaySound(codeSuccess, 1.0f, audioSource);
            AudioManager.Instance.PlaySound(wallOpenClip, 1.0f, audioSource);
        }

        private void WallFail()
        {
            if (failCross) StartCoroutine(FailCrossShow());
            FindObjectOfType<Pawn.PlayerStats>().TakeDamage(1);
            // Debug.Log("Input was incorrect, correct input should've been: " + colourString);
        }

        private IEnumerator FailCrossShow()
        {
            failCross.SetActive(true);
            AudioManager.Instance.PlaySound(codeFail, 1.0f, GetComponent<AudioSource>());
            yield return new WaitForSeconds(1);
            failCross.SetActive(false);
        }

        public void InteractionStart() => ActivateQuickTimeMenu();

        public void InteractionEnd()
        {
            quickTimeMenu.DeactivateQuickTimeMenu();
            quickTimeMenu.ClearInput();
        }
    }
}

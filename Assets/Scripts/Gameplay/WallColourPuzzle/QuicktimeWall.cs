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
        [SerializeField] private QuickTimeMenu quickTimeMenu;
        [SerializeField] private GameObject failCross;
        [SerializeField] private Animator wallAnimator;
        [SerializeField] private AudioClip wallOpenClip, codeSuccess, codeFail;
        [SerializeField] private MiniPuzzleColourCube associatedMiniPuzzle;

        private int[] colourCode;
        private int requiredInputs;

        private void Awake()
        {
            if (!GetComponent<AudioSource>())
                Debug.LogWarning("No audio source found");
            SetObjectiveType(ObjectiveType.WallPuzzle);
        }

        private void Start() => InitializeQuickTimeEvent();

        private void InitializeQuickTimeEvent()
        {
            quickTimeMenu.DeactivateQuickTimeMenu();
            failCross.SetActive(false);
            GenerateColourCode();

            if (colourCode.Length > 0)
            {
                ObjectiveManager.Instance.RegisterObjective(this, colourCode);
                associatedMiniPuzzle.Initialize(colourCode);
            }

            // DebugColourInfo();
        }

        private void GenerateColourCode()
        {
            requiredInputs = (colourCode = new int[Random.Range(2, 6)]).Length;
            for (int i = 0; i < requiredInputs; i++)
                colourCode[i] = Random.Range(0, ObjectiveManager.Instance.colourData.Count);
        }

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
            wallAnimator.SetBool("Open", true);
            GetComponentInParent<Collider>().enabled = false;
            if (!IsCompleted) base.CompleteObjective();
            PlaySuccessSounds();
            CameraShake.Instance.ShakeCamera(4f, 2f, "SixDShake");
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

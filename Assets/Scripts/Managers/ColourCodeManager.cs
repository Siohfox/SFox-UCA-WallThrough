using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using WallThrough.Gameplay;

namespace WallThrough.UI
{
    public class ColourCodeManager : MonoBehaviour
    {
        [Header("Parent Prefabs")]
        [SerializeField] private GameObject parentPrefabHolder; // Empty GameObject to hold parent prefabs
        [SerializeField] private GameObject parentPrefab; // Prefab for the parent object holding the images
        [SerializeField] private GameObject imagePrefab; // Prefab for the colored images (UI)

        [Header("Settings")]
        [SerializeField] private float showCodeTime = 1f; // Time to show the code  

        private void Awake()
        {
            parentPrefabHolder = gameObject;
        }

        public GameObject Initialize(int[] colourCodes)
        {
            GameObject parentObject; // Reference to the instantiated parent object

            // Check for null references
            if (!parentPrefabHolder)
            {
                Debug.LogError("Parent Prefab Holder is not assigned in ColourCodeManager.");
            }
            if (!parentPrefab)
            {
                Debug.LogError("Parent Prefab is not assigned in ColourCodeManager.");
            }

            // Create a new parent object for the images under the parentPrefabHolder
            parentObject = Instantiate(parentPrefab, parentPrefabHolder.transform); // Instantiate the parent prefab under the holder

            // Clear existing images in the parent (if needed)
            foreach (Transform child in parentObject.transform)
            {
                Destroy(child.gameObject);
            }

            // Instantiate images based on the colourCodes length
            for (int i = 0; i < colourCodes.Length; i++)
            {
                // Instantiate the image as a child of the parent object
                Color color = ObjectiveManager.Instance.GetColourData(colourCodes[i]).colour; // Get the color from the manager
                GameObject image = Instantiate(imagePrefab, parentObject.transform); // Instantiate image under the parent object

                // Set the image color (including alpha)
                Image imgComponent = image.GetComponentInChildren<Image>(); // Get the Image component
                if (imgComponent != null)
                {
                    color.a = 1; // Ensure alpha is set to 1 (fully opaque)
                    imgComponent.color = color; // Set the image color
                }

                // Adjust position of the image (if necessary)
                image.transform.localPosition = new Vector3(0, 0, i * 1.5f); // Adjust position as needed
            }

            return parentObject;
        }

        public IEnumerator FlashCode(GameObject parentObject)
        {
            parentObject.SetActive(true);
            Animator[] animArray = parentObject.GetComponentsInChildren<Animator>();
            Image[] imageArray = parentObject.GetComponentsInChildren<Image>();
            Animator parentAnimator = parentObject.GetComponent<Animator>();

            foreach (Image image in imageArray)
            {
                Color color = image.color;
                color.a = 0; // Set alpha to 0
                image.color = color;
            }

            foreach (Animator animator in animArray)
            {
                animator.SetBool("Activated", true);
                Image image = animator.GetComponent<Image>();
                if (image != null)
                {
                    Color color = image.color;
                    color.a = 1; // Set alpha to 1
                    image.color = color;
                }

                yield return new WaitForSeconds(GetAnimationDuration(animator) / animArray.Length);
            }

            yield return new WaitForSeconds(showCodeTime);

            parentAnimator.SetTrigger("Sweep");
        }

        public IEnumerator ClearHeldCode()
        {
            foreach (Transform child in gameObject.transform)
            {
                // Check if the child is active
                if (child.gameObject.activeSelf)
                {
                    // Get the Animator component of the active child
                    Animator parentAnimator = child.GetComponent<Animator>();
                    if (parentAnimator != null)
                    {
                        // Trigger the dismiss animation
                        parentAnimator.SetTrigger("Dismiss");

                        // Wait for the animation to finish, adding some padding
                        float paddedWaitTime = 0.2f;
                        yield return new WaitForSeconds(GetAnimationDuration(parentAnimator) + paddedWaitTime);

                        // Deactivate the child object
                        child.gameObject.SetActive(false);
                    }

                    // Exit the loop as we only expect one active child
                    yield break;
                }
            }
        }

        private float GetAnimationDuration(Animator animator)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.length;
        }
    }
}

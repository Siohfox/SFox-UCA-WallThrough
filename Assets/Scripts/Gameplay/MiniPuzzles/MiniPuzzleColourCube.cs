using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WallThrough.Gameplay
{
    public class MiniPuzzleColourCube : MonoBehaviour
    {
        [Header("Parent Prefabs")]
        [SerializeField] private GameObject parentPrefabHolder; // Empty GameObject to hold parent prefabs
        [SerializeField] private GameObject parentPrefab; // Prefab for the parent object holding the images
        [SerializeField] private GameObject imagePrefab; // Prefab for the colored images (UI)

        [Header("Pressure Plates")]
        [SerializeField] private Transform[] pressurePlateLocations; // Array of Transforms for pressure plate locations
        [SerializeField] private GameObject pressurePlatePrefab; // Prefab for the pressure plate

        [Header("Settings")]
        [SerializeField] private float showCodeTime = 1f; // Time to show the code

        private GameObject parentObject; // Reference to the instantiated parent object

        public void InstantiateCubes(int[] colourCodes)
        {
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

            // Set parentObject inactive initially
            parentObject.SetActive(false);

            // Spawn the pressure plate at a random location
            SpawnPressurePlate();
        }

        private void SpawnPressurePlate()
        {
            if (pressurePlateLocations.Length > 0)
            {
                // Select a random Transform from the assigned pressure plate locations
                int randomIndex = Random.Range(0, pressurePlateLocations.Length);
                Transform selectedLocation = pressurePlateLocations[randomIndex];

                // Instantiate the pressure plate at the selected location
                GameObject pressurePlate = Instantiate(pressurePlatePrefab, selectedLocation.position, Quaternion.identity);
                pressurePlate.AddComponent<PressurePlate>().Initialize(parentObject, showCodeTime); // Add a PressurePlate component
            }
        }
    }

    public class PressurePlate : MonoBehaviour
    {
        private GameObject parentObject;
        private float showCodeTime;

        public void Initialize(GameObject parent, float showCodeTime)
        {
            parentObject = parent; // Store the reference to the parent object
            this.showCodeTime = showCodeTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Check if the player collides with the pressure plate
            if (other.CompareTag("Player"))
            {
                StartCoroutine(FlashCode());  
            }
        }

        private IEnumerator FlashCode()
        {
            parentObject.SetActive(true);
            Animator[] animArray = parentObject.GetComponentsInChildren<Animator>();
            Image[] imageArray = parentObject.GetComponentsInChildren<Image>();

            // Set initial alpha to 0 for all images
            foreach (Image image in imageArray)
            {
                Color color = image.color;
                color.a = 0; // Set alpha to 0
                image.color = color;
            }

            foreach (Animator animator in animArray)
            {
                // Activate the animator
                animator.SetBool("Activated", true);

                // Set the corresponding image's alpha to 1
                Image image = animator.GetComponent<Image>();
                if (image != null)
                {
                    Color color = image.color;
                    color.a = 1; // Set alpha to 1
                    image.color = color;
                }

                // Wait for the duration of the animation
                float animationDuration = GetAnimationDuration(animator);
                yield return new WaitForSeconds(animationDuration);

                // Deactivate the animator
                animator.SetBool("Activated", false);
            }

            // Wait an additional second after the final animation
            yield return new WaitForSeconds(showCodeTime);

            // Finally, deactivate the parent object after all animations are done
            parentObject.SetActive(false);
        }

        // Helper method to get the animation duration
        private float GetAnimationDuration(Animator animator)
        {
            // Assuming the animation is on the first layer and you know the animation clip name
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.length; // Or a fixed duration if you know it
        }
    }
}

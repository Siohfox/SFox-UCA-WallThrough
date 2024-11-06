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

        private GameObject parentObject; // Reference to the instantiated parent object

        private void Awake()
        {
            parentPrefabHolder = gameObject;
        }

        public GameObject Initialize(int[] colourCodes)
        {
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

            //// Set parentObject inactive initially
            //parentObject.SetActive(false);

            return parentObject;
        }

        public IEnumerator FlashCode(GameObject parentObject)
        {
            parentObject.SetActive(true);
            Animator[] animArray = parentObject.GetComponentsInChildren<Animator>();
            Image[] imageArray = parentObject.GetComponentsInChildren<Image>();

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
                animator.SetBool("Activated", false);
            }

            yield return new WaitForSeconds(showCodeTime);
            parentObject.SetActive(false);
        }

        private float GetAnimationDuration(Animator animator)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.length;
        }
    }
}

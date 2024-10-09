using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WallThrough.Gameplay
{
    public class QuicktimeWall : MonoBehaviour
    {
        [SerializeField]
        private GameObject quickTimeMenu;
        public enum ColourMap { Red, Orange, Yellow, Green, Blue, Purple };
        private int[] colourCode = new int[4];
        private string colourString;
        bool isInteracting = false;


        // Start is called before the first frame update
        void Start()
        {
            isInteracting = false;
            quickTimeMenu.SetActive(false);

            for (int i = 0; i < colourCode.Length; i++)
            {
                // Generate a random number between 0 and the number of enum values
                colourCode[i] = UnityEngine.Random.Range(0, Enum.GetValues(typeof(ColourMap)).Length);
            }

            // Convert the colourCode array to enum names
            List<string> colourNames = new List<string>();
            foreach (int code in colourCode)
            {
                colourNames.Add(Enum.GetName(typeof(ColourMap), code));
            }

            // Join the enum names into string
            colourString = string.Join(" ", colourNames.ToArray());
            string intResult = string.Join(" ", colourCode);

            // result of colour code
            Debug.Log("### Wall Number " + UnityEngine.Random.Range(0, 1000));
            Debug.Log("Colour Names: " + colourString);
            Debug.Log("Integer Values: " + intResult);
        }

        // Update is called once per frame
        void Update()
        {
            if (!isInteracting) return;

            // Test code
            if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log("Pressing T");
                //replace with moving down animation
                Destroy(transform.parent.gameObject);
            }   
        }

        private void OnCollisionEnter(Collision collision)
        {
            quickTimeMenu.SetActive(true);
            isInteracting = true;

            quickTimeMenu.GetComponent<QuickTimeMenu>().SetCurrentWall(this.gameObject);
        }

        private void OnCollisionExit(Collision collision)
        {
            quickTimeMenu.SetActive(false);
            isInteracting = false;
        }

        public void CompareCodes(List<int> codeInput)
        {
            bool matching = false;
            for(int i = 0; i < 4; i++)
            {
                if (codeInput[i] != colourCode[i])
                {
                    Debug.Log("Input was incorrect, correct input should've been: " + colourString);
                    return;
                }

                if(i >= 3)
                {
                    Debug.Log("Input was correct, destroying door");
                    quickTimeMenu.SetActive(false);

                    // Finally open the door
                    Destroy(transform.parent.gameObject);
                }                
            }
        }
    }
}


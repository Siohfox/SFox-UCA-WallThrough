using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WallThrough.Gameplay
{
    public class QuicktimeWall : MonoBehaviour
    {

        public GameObject quickTimeMenu;
        public enum ColourMap { Red, Orange, Yellow, Green, Blue, Purple };
        int[] colourCode = new int[4];

        public List<int> codeInput;

        bool isInteracting = false;


        // Start is called before the first frame update
        void Start()
        {
            isInteracting = false;
            quickTimeMenu.SetActive(false);


            // Init colourArray
            codeInput = new List<int> { };

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
            string result = string.Join(" ", colourNames.ToArray());
            string intResult = string.Join(" ", colourCode);

            // result of colour code
            Debug.Log("Colour Names: " + result);
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

            if (codeInput.Count >= 4)
            {
                // Convert the colourCode array to enum names
                List<string> colourNames = new List<string>();
                foreach (int code in codeInput)
                {
                    colourNames.Add(Enum.GetName(typeof(ColourMap), code));
                }

                Debug.Log("added 4 things: " + string.Join(" ", colourNames.ToArray()));

                codeInput.Clear();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            quickTimeMenu.SetActive(true);
            isInteracting = true;
        }

        private void OnCollisionExit(Collision collision)
        {
            quickTimeMenu.SetActive(false);
            isInteracting = false;
        }

        public void InputColour(int colour)
        {
            codeInput.Add(colour);
        }
    }
}


using System.Collections.Generic;
using UnityEngine;

namespace WallThrough.Gameplay
{
    /// <summary>
    /// Manages the registration and retrieval of color codes associated with objectives.
    /// </summary>
    public class ColourCodeManager : MonoBehaviour
    {
        /// <summary>
        /// Singleton instance of the ColourCodeManager.
        /// </summary>
        public static ColourCodeManager Instance { get; private set; }

        

        /// <summary>
        /// Ensures that there is only one instance of ColourCodeManager in the scene.
        /// </summary>
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }



        
    }
}

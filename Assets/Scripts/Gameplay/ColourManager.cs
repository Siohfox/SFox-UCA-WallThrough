using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WallThrough.Gameplay;

namespace WallThrough.Utility
{
    public static class ColourManager
    {
        public struct ColourData
        {
            public Color colour;
            public string colourName;
        }
        private List<ColourData> GetColourDataList(List<Color> colours, List<string> colourStrings)
        {

        }

        public ColourData GetColourData(Objective objective)
        {
            return objective.ColourData;
        }
    }
}


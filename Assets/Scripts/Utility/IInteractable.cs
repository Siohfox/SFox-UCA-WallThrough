using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WallThrough.Gameplay.Interactable
{
    // Script needs to be added next to monobehaviour
    public interface IInteractable
    {
        public void InteractionStart();

        public void InteractionEnd();
    }
}

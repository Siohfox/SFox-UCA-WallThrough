using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WallThrough.Gameplay.Pawn
{
    [RequireComponent(typeof(Movement))]
    public class PlayerController : MonoBehaviour
    {
        private Movement _movement;

        private void Start()
        {
            _movement = GetComponent<Movement>();
        }

        private void FixedUpdate()
        {
            _movement.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }
}
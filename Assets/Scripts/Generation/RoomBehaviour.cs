using System;
using System.Collections.Generic;
using UnityEngine;

namespace WallThrough.Generation
{
    // Enum to represent possible directions in the room
    public enum Direction { Up, Down, Right, Left };

    public class RoomBehaviour : MonoBehaviour
    {
        // Public fields for room components and attributes
        public GameObject[] walls;
        public GameObject[] doors;
        public GameObject quickTimeWallPrefab;
        public GameObject exitPortalPrefab;
        public GameObject collectablePrefab;
        public GameObject puzzleParentObject;
        public List<GameObject> rotatableObjects;
        public Vector3 roomSize = new(24, 0, 24);
        public MiniPuzzle roomMiniPuzzle;

        // Assign MiniPuzzle component if it exists in child objects
        private void Awake()
        {
            Transform miniPuzzleChild = FindChildWithTag("MiniPuzzle");
            roomMiniPuzzle = miniPuzzleChild != null ? miniPuzzleChild.GetComponent<MiniPuzzle>() : null;
        }

        // Update the room's state based on the given cell data
        public void UpdateRoom(DungeonGenerator.Cell cell)
        {
            UpdateDoorsAndWalls(cell);
            RotateObjects(cell.roomRotation);
            HandleRoomSpecificBehaviors(cell);
        }

        // Place collectable items in random positions within the room
        private void PlaceCollectable(int amount)
        {
            const float padding = 4f;

            if (collectablePrefab == null)
            {
                Debug.LogWarning("No collectable prefab assigned to RoomBehaviour");
                return;
            }

            for (int i = 0; i < amount; i++)
            {
                Vector3 randomPosition = GetRandomPositionWithinRoom(padding);
                Instantiate(collectablePrefab, randomPosition, Quaternion.identity, transform);
            }
        }

        // Get the center of the room based on the bounds of its child renderers
        public Vector3 GetRoomCentre()
        {
            Bounds roomBounds = CalculateRoomBounds();
            return roomBounds.center;
        }

        // Expose the room's MiniPuzzle component
        public MiniPuzzle GetRoomMiniPuzzle() => roomMiniPuzzle;

        // Helper method to encapsulate child bounds into a room's total bounds
        private Bounds CalculateRoomBounds()
        {
            Bounds bounds = new(transform.position, Vector3.zero);
            foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
                bounds.Encapsulate(renderer.bounds);
            return bounds;
        }

        // Find the first child with the given tag
        private Transform FindChildWithTag(string tag)
        {
            foreach (Transform child in transform)
                if (child.CompareTag(tag))
                    return child;

            return null;
        }

        // Update the state of doors and walls based on the cell's door status
        private void UpdateDoorsAndWalls(DungeonGenerator.Cell cell)
        {
            for (int i = 0; i < cell.doorOpenStatus.Length; i++)
            {
                bool isDoorOpen = cell.doorOpenStatus[i];
                doors[i].SetActive(isDoorOpen);
                walls[i].SetActive(!isDoorOpen);

                if (ShouldInstantiateQuickTimeWall(cell, isDoorOpen, i))
                    InstantiateQuickTimeWallAtDoor(doors[i]);
            }
        }

        // Rotate objects in the room based on the specified rotation
        private void RotateObjects(float roomRotation)
        {
            if (rotatableObjects.Count == 0)
                return;

            foreach (GameObject rotatableObject in rotatableObjects)
                rotatableObject.transform.RotateAround(GetRoomCentre(), Vector3.up, roomRotation);
        }

        // Handle specific behaviors based on the room type
        private void HandleRoomSpecificBehaviors(DungeonGenerator.Cell cell)
        {
            if (cell.RoomType == RoomType.Collectable)
                PlaceCollectable(3);
        }

        // Determine if a quick-time wall should be instantiated
        private bool ShouldInstantiateQuickTimeWall(DungeonGenerator.Cell cell, bool isDoorOpen, int index)
        {
            return isDoorOpen
                   && cell.doorSpawnDirection == (Direction)index
                   && cell.RoomType != RoomType.Final
                   && quickTimeWallPrefab != null;
        }

        // Instantiate a quick-time wall at the specified door position
        private void InstantiateQuickTimeWallAtDoor(GameObject door)
        {
            Vector3 wallPosition = door.transform.position + new Vector3(0, 2, 0);
            GameObject quickTimeWall = Instantiate(quickTimeWallPrefab, wallPosition, door.transform.rotation);
            quickTimeWall.transform.SetParent(transform);
        }

        // Generate a random position within the room boundaries
        private Vector3 GetRandomPositionWithinRoom(float padding)
        {
            float xPos = UnityEngine.Random.Range(transform.position.x + padding, transform.position.x + roomSize.x - padding);
            float zPos = UnityEngine.Random.Range(transform.position.z - roomSize.z + padding, transform.position.z - padding);
            return new Vector3(xPos, 2, zPos);
        }
    }
}

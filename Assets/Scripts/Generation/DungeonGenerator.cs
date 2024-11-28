using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WallThrough.Core;
using WallThrough.Gameplay;
using WallThrough.Utility;

namespace WallThrough.Generation
{
    public enum RoomType { Basic, PushPuzzle, WiresPuzzle, Collectable, Special, Final }

    public class DungeonGenerator : MonoBehaviour
    {
        public int generationSize;
        public int startPos = 0;
        public Vector2 offset;
        public GameObject[] rooms;  // First room for main path, second room for branches, third for final room, needs to be expandable
        public float generationDelay = 0.3f;
        public int maxBranchLength = 3;  // Max length of branches
        public bool generateBranches = false;

        private List<Cell> dungeonGrid;
        private List<int> roomCells;
        private List<int> mainPathCells;

        public static event Action OnDungeonGenerated;

        public class Cell
        {
            public bool visited = false;
            public bool isRoom = false;
            public Quaternion innerRotation;
            public RoomType RoomType = RoomType.Basic;
            public bool[] status = new bool[4]; // Wall status for each direction         
            public Direction doorSpawnDirection;
        }

        private void Start()
        {
            GenerateDungeon(); 
        }

        private void GenerateDungeon()
        {
            if (generationSize < 1)
            {
                Debug.LogError("Generation size should not be zero!");
                return;
            }

            InitializeDungeonGrid();
            GenerateLinearPathWithBranches();
            //StartCoroutine(SpawnRooms());
            SpawnRooms();
        }

        private void InitializeDungeonGrid()
        {
            dungeonGrid = new List<Cell>(Util.MathSquare(generationSize));
            roomCells = new();
            mainPathCells = new();

            for (int i = 0; i < Util.MathSquare(generationSize); i++)
            {
                dungeonGrid.Add(new Cell());
            }
        }

        private void GenerateLinearPathWithBranches()
        {
            Stack<int> path = new();
            int currentCell = startPos;
            int pathLength = 0;

            while (pathLength < generationSize)
            {
                dungeonGrid[currentCell].visited = true;
                dungeonGrid[currentCell].isRoom = true;
                roomCells.Add(currentCell);
                mainPathCells.Add(currentCell);  // Add to main path list

                // Assign a random room type for main path cells (except the final room)
                if (pathLength != generationSize - 1)
                {
                    dungeonGrid[currentCell].RoomType = GetRandomMainPathRoomType();
                }

                List<int> neighbors = GetUnvisitedNeighbors(currentCell);

                if (neighbors.Count > 0) // If there is unvisited neighbors to the current cell, it has potential to be part of the path
                {
                    path.Push(currentCell);                                                 // Push the cell as part of the potential path
                    int nextCell = neighbors[UnityEngine.Random.Range(0, neighbors.Count)]; // Randomly select a neighbor to be the next cell in the path                                      
                    if (pathLength != generationSize - 1)                                   // Check if final room
                    {
                        ConnectCells(currentCell, nextCell); // Make sure the doors between the current cell and the newly selected neighbor are open
                    }
                    currentCell = nextCell;
                    pathLength++;
                }
                // No neighbors? Check if there are still cells in the path stack
                // If there is still cells in the path stack, pop a previous cell to backtrack
                // Note to self: Pop removes the current cell from the top of the stack (like a stack of plates), and
                // this removed plate becomes the current cell again, completing the backtrack.
                else if (path.Count > 0)
                {
                    currentCell = path.Pop();
                }

                // Chance to start a branch path
                if (pathLength > 2 && pathLength < generationSize-1 && UnityEngine.Random.value < 0.3f && generateBranches)
                {
                    GenerateBranch(currentCell);
                }
            }

            if (mainPathCells.Count > 0)
            {
                int finalRoomIndex = mainPathCells[^1]; // ^1 gets the last item in mainPathCells
                dungeonGrid[finalRoomIndex].RoomType = RoomType.Final; // Mark it as final room
            }
        }

        private void GenerateBranch(int startCell)
        {
            int branchLength = UnityEngine.Random.Range(1, maxBranchLength);
            int currentCell = startCell;

            for (int i = 0; i < branchLength; i++)
            {
                dungeonGrid[currentCell].visited = true;
                dungeonGrid[currentCell].isRoom = true;
                dungeonGrid[currentCell].RoomType = RoomType.Collectable;
                roomCells.Add(currentCell);

                List<int> neighbors = GetUnvisitedNeighbors(currentCell);
                if (neighbors.Count > 0)
                {
                    int nextCell = neighbors[UnityEngine.Random.Range(0, neighbors.Count)];
                    if (i != branchLength - 1) // Check if final room
                    {
                        ConnectCells(currentCell, nextCell); // Make sure the doors between the current cell and the newly selected neighbor are open
                    }
                    currentCell = nextCell;
                }
                else
                {
                    break;
                }
            }
        }

        private List<int> GetUnvisitedNeighbors(int cell)
        {
            List<int> neighbors = new();
            int width = generationSize;

            void AddNeighbor(int neighbor)
            {
                if (neighbor >= 0 && neighbor < dungeonGrid.Count && !dungeonGrid[neighbor].visited)
                {
                    neighbors.Add(neighbor);
                }
            }

            if (cell - width >= 0) AddNeighbor(cell - width); // Up
            if (cell + width < dungeonGrid.Count) AddNeighbor(cell + width); // Down
            if (cell % width != 0) AddNeighbor(cell - 1); // left
            if ((cell + 1) % width != 0) AddNeighbor(cell + 1); // right

            return neighbors;
        }


        private void ConnectCells(int currentCell, int newCell)
        {
            int width = generationSize;

            // Avoid connecting to outside cells
            if (newCell < 0 || newCell >= dungeonGrid.Count) return;

            if (newCell == currentCell + 1) // Right
            {
                if ((currentCell + 1) % width == 0) return; // Prevent connection at the edge
                dungeonGrid[currentCell].status[(int)Direction.Right] = true; // Disable Right Wall
                dungeonGrid[newCell].status[(int)Direction.Left] = true; // Disable Left Wall

                dungeonGrid[currentCell].doorSpawnDirection = Direction.Right;
            }
            else if (newCell == currentCell - 1) // Left
            {
                if (currentCell % width == 0) return; // Prevent connection at the edge
                dungeonGrid[currentCell].status[(int)Direction.Left] = true; // Disable Left Wall
                dungeonGrid[newCell].status[(int)Direction.Right] = true; // Disable Right Wall

                dungeonGrid[currentCell].doorSpawnDirection = Direction.Left;
            }
            else if (newCell == currentCell + width) // Down
            {
                if (newCell >= dungeonGrid.Count) return; // Prevent connection beyond bottom edge
                dungeonGrid[currentCell].status[(int)Direction.Down] = true;
                dungeonGrid[newCell].status[(int)Direction.Up] = true;

                dungeonGrid[currentCell].doorSpawnDirection = Direction.Down;
            }
            else if (newCell == currentCell - width) // Up
            {
                if (newCell < 0) return; // Prevent connection beyond top edge
                dungeonGrid[currentCell].status[(int)Direction.Up] = true;
                dungeonGrid[newCell].status[(int)Direction.Down] = true;

                dungeonGrid[currentCell].doorSpawnDirection = Direction.Up;
            }
        }

        private void SpawnRooms()
        {
            List<RoomBehaviour> spawnedRooms = new(); // Track all spawned rooms
            int roomCounter = 0;

            for (int x = 0; x < generationSize; x++)
            {
                for (int y = 0; y < generationSize; y++)
                {
                    int index = x + y * generationSize;

                    if (roomCells.Contains(index))
                    {
                        Cell cell = dungeonGrid[index];
                        int roomIndex = (int)cell.RoomType; // Determine the prefab based on the cell's RoomType

                        // Calculate position for the room
                        Vector3 position = new(x * offset.x, 0, -y * offset.y);

                        // Instantiate the correct prefab
                        var roomPrefab = rooms[roomIndex];
                        var room = Instantiate(roomPrefab, position, Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                        room.UpdateRoom(cell);

                        // Name the room appropriately
                        room.name = $"{roomPrefab.name}({roomCounter}) {x}-{y}";
                        roomCounter++;

                        spawnedRooms.Add(room);
                    }
                }
            }

            // Pass the list of spawned rooms to the GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SetCurrentRooms(spawnedRooms);
                //GameManager.Instance.DebugRoomList();
            }

            Debug.Log("Invoking dungeon generated");
            OnDungeonGenerated?.Invoke();
        }


        private Vector2 GetRoomCoordinates(int index)
        {
            int x = index % generationSize;
            int y = index / generationSize;
            return new Vector2(x, y);
        }

        private RoomType GetRandomMainPathRoomType()
        {
            RoomType[] mainPathRoomTypes = { RoomType.Basic, RoomType.PushPuzzle, RoomType.WiresPuzzle };
            return mainPathRoomTypes[UnityEngine.Random.Range(0, mainPathRoomTypes.Length)];
        }
    }
}

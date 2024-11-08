using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WallThrough.Generation;

public class DungeonGenerator : MonoBehaviour
{
    public int generationSize;
    public int startPos = 0;
    public GameObject finalBoss;
    public Vector2 offset;
    public GameObject[] rooms;  // First room for main path, second room for branches
    public float generationDelay = 0.3f;
    public int mainPathLength = 10;  // Length of the main path
    public int maxBranchLength = 3;  // Max length of branches

    private List<Cell> dungeonGrid;
    private List<int> roomCells;
    private List<int> mainPathCells;

    public class Cell
    {
        public bool visited = false;
        public bool isRoom = false;
        public bool[] status = new bool[4]; // Wall status for each direction
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
        StartCoroutine(SpawnRooms());
        PlaceFinalBoss();
    }

    private void InitializeDungeonGrid()
    {
        dungeonGrid = new List<Cell>(MathSquare(generationSize));
        roomCells = new();
        mainPathCells = new();

        for (int i = 0; i < MathSquare(generationSize); i++)
        {
            dungeonGrid.Add(new Cell());
        }
    }

    private void GenerateLinearPathWithBranches()
    {
        Stack<int> path = new();
        int currentCell = startPos;
        int pathLength = 0;

        while (pathLength < mainPathLength)
        {
            dungeonGrid[currentCell].visited = true;
            dungeonGrid[currentCell].isRoom = true;
            roomCells.Add(currentCell);
            mainPathCells.Add(currentCell);  // Add to main path list

            List<int> neighbors = GetUnvisitedNeighbors(currentCell);
            if (neighbors.Count > 0)
            {
                path.Push(currentCell);
                int nextCell = neighbors[UnityEngine.Random.Range(0, neighbors.Count)];
                ConnectCells(currentCell, nextCell);
                currentCell = nextCell;
                pathLength++;
            }
            else if (path.Count > 0)
            {
                currentCell = path.Pop();
            }

            // Chance to start a branch path
            if (pathLength > 2 && UnityEngine.Random.value < 0.3f)
            {
                GenerateBranch(currentCell);
            }
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
            roomCells.Add(currentCell);

            List<int> neighbors = GetUnvisitedNeighbors(currentCell);
            if (neighbors.Count > 0)
            {
                int nextCell = neighbors[UnityEngine.Random.Range(0, neighbors.Count)];
                ConnectCells(currentCell, nextCell);
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

        AddNeighbor(cell - width); // up
        AddNeighbor(cell + width); // down
        if (cell % width != 0) AddNeighbor(cell - 1); // left
        if ((cell + 1) % width != 0) AddNeighbor(cell + 1); // right

        return neighbors;
    }

    private void ConnectCells(int currentCell, int newCell)
    {
        int width = generationSize;
        if (newCell == currentCell + 1) // Right
        {
            dungeonGrid[currentCell].status[2] = true;
            dungeonGrid[newCell].status[3] = true;
        }
        else if (newCell == currentCell - 1) // Left
        {
            dungeonGrid[currentCell].status[3] = true;
            dungeonGrid[newCell].status[2] = true;
        }
        else if (newCell == currentCell + width) // Down
        {
            dungeonGrid[currentCell].status[1] = true;
            dungeonGrid[newCell].status[0] = true;
        }
        else if (newCell == currentCell - width) // Up
        {
            dungeonGrid[currentCell].status[0] = true;
            dungeonGrid[newCell].status[1] = true;
        }
    }

    private IEnumerator SpawnRooms()
    {
        for (int x = 0; x < generationSize; x++)
        {
            for (int y = 0; y < generationSize; y++)
            {
                int index = x + y * generationSize;
                if (roomCells.Contains(index))
                {
                    Cell cell = dungeonGrid[index];
                    int roomIndex = mainPathCells.Contains(index) ? 0 : 1;  // Main path uses first room, branches use second
                    Vector3 position = new(x * offset.x, 0, -y * offset.y);
                    var room = Instantiate(rooms[roomIndex], position, Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    room.UpdateRoom(cell.status);
                    room.name += $" {x}-{y}";
                }
                yield return new WaitForSeconds(generationDelay);
            }
        }
    }

    private void PlaceFinalBoss()
    {
        int finalRoomIndex = mainPathCells[^1]; // ^1 means last element in the array
        Vector2 finalPos = GetRoomCoordinates(finalRoomIndex);
        Vector3 bossPosition = new(finalPos.x * offset.x + offset.x / 2, 0, -finalPos.y * offset.y - offset.y / 2);
        Instantiate(finalBoss, bossPosition, Quaternion.identity, transform);
    }

    private Vector2 GetRoomCoordinates(int index)
    {
        int x = index % generationSize;
        int y = index / generationSize;
        return new Vector2(x, y);
    }

    public T MathSquare<T>(T numberToSquare) where T : IConvertible
    {
        // Convert to double for arithmetic operations
        double num = Convert.ToDouble(numberToSquare);
        return (T)Convert.ChangeType(num * num, typeof(T));  // Convert back to original type.
    }

}

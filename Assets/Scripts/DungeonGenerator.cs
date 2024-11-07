using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WallThrough.Gameplay;

public class DungeonGenerator : MonoBehaviour
{
    public Vector2Int generationSize;
    public int startPos = 0;
    public GameObject finalBoss;
    public Vector2 offset;
    public Rule[] rooms;
    public float generationDelay = 0.3f;

    private List<Cell> dungeonGrid;
    private List<int> roomCells; // Cells marked for room instantiation

    public class Cell
    {
        public bool visited = false;
        public bool isRoom = false; // Indicates if this cell will be a room
        public bool[] status = new bool[4]; // Wall status for each direction
    }

    [System.Serializable]
    public class Rule
    {
        public GameObject room;
        public Vector2Int minPosition;
        public Vector2Int maxPosition;
        public bool obligatory;

        public SpawnType GetSpawnType(int x, int y)
        {
            if (x >= minPosition.x && x <= maxPosition.x && y >= minPosition.y && y <= maxPosition.y)
            {
                return obligatory ? SpawnType.Required : SpawnType.Optional;
            }
            return SpawnType.None;
        }
    }

    public enum SpawnType { None, Optional, Required }

    private void Start()
    {
        GenerateDungeon();
    }

    private void GenerateDungeon()
    {
        if (generationSize.x < 1 || generationSize.y < 1)
        {
            Debug.LogError("Generation size should not be zero!");
            return;
        }

        InitializeDungeonGrid();
        GenerateMazeWithRooms();
        StartCoroutine(SpawnRooms());
        PlaceFinalBoss();
    }

    private void InitializeDungeonGrid()
    {
        dungeonGrid = new List<Cell>(generationSize.x * generationSize.y);
        roomCells = new List<int>();

        for (int i = 0; i < generationSize.x * generationSize.y; i++)
        {
            dungeonGrid.Add(new Cell());
        }
    }

    private void GenerateMazeWithRooms()
    {
        Stack<int> path = new Stack<int>();
        int currentCell = startPos;

        for (int i = 0; i < generationSize.x * generationSize.y; i++)
        {
            dungeonGrid[currentCell].visited = true;
            dungeonGrid[currentCell].isRoom = true;
            roomCells.Add(currentCell);

            List<int> neighbors = GetUnvisitedNeighbors(currentCell);
            if (neighbors.Count > 0)
            {
                path.Push(currentCell);
                int newCell = neighbors[UnityEngine.Random.Range(0, neighbors.Count)];
                ConnectCells(currentCell, newCell);
                currentCell = newCell;
            }
            else if (path.Count > 0)
            {
                currentCell = path.Pop();
            }
            else
            {
                break;
            }
        }
    }

    private List<int> GetUnvisitedNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();
        int width = generationSize.x;

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
        int width = generationSize.x;
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
        for (int x = 0; x < generationSize.x; x++)
        {
            for (int y = 0; y < generationSize.y; y++)
            {
                int index = x + y * generationSize.x;
                if (roomCells.Contains(index))
                {
                    Cell cell = dungeonGrid[index];
                    int roomIndex = GetRoomIndex(x, y);
                    if (roomIndex != -1)
                    {
                        Vector3 position = new Vector3(x * offset.x, 0, -y * offset.y);
                        var room = Instantiate(rooms[roomIndex].room, position, Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                        room.UpdateRoom(cell.status);
                        room.name += $" {x}-{y}";
                    }
                }
                yield return new WaitForSeconds(generationDelay);
            }
        }
    }

    private int GetRoomIndex(int x, int y)
    {
        foreach (var rule in rooms)
        {
            SpawnType type = rule.GetSpawnType(x, y);
            if (type == SpawnType.Required) return System.Array.IndexOf(rooms, rule);
            if (type == SpawnType.Optional) return UnityEngine.Random.Range(0, rooms.Length);
        }
        return 0; // Default room
    }

    private void PlaceFinalBoss()
    {
        int finalRoomIndex = roomCells[roomCells.Count - 1];
        Vector2 finalPos = GetRoomCoordinates(finalRoomIndex);
        Vector3 bossPosition = new Vector3(finalPos.x * offset.x + offset.x / 2, 0, -finalPos.y * offset.y - offset.y / 2);
        Instantiate(finalBoss, bossPosition, Quaternion.identity, transform);
    }

    private Vector2 GetRoomCoordinates(int index)
    {
        int x = index % generationSize.x;
        int y = index / generationSize.x;
        return new Vector2(x, y);
    }
}
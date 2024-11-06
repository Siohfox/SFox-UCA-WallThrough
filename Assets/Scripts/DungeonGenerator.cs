using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using WallThrough.Gameplay;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    public Vector2 generationSize;
    public int startPos = 0;
    public GameObject finalBoss;
    public Vector2 offset;
    public Rule[] rooms;
    List<Cell> dungeonGrid;

    [System.Serializable]
    public class Rule
    {
        public GameObject room;
        public Vector2Int minPosition;
        public Vector2Int maxPosition;
        

        public bool obligatory;

        public int ProbabilityOfSpawning(int x, int y)
        {
            // 0 - cannot spawn 1 - can spawn 2 - HAS to spawn

            if (x >= minPosition.x && x <= maxPosition.x && y >= minPosition.y && y <= maxPosition.y)
            {
                return obligatory ? 2 : 1;
            }

            return 0;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
    }

    // Coroutine to generate rooms with a 1-second delay between each
    IEnumerator GenerateDungeon()
    {
        for (int i = 0; i < generationSize.x; i++)
        {
            for (int j = 0; j < generationSize.y; j++)
            {
                Cell currentCell = dungeonGrid[Mathf.FloorToInt(i + j * generationSize.x)];
                if (currentCell.visited)
                {
                    int randomRoom = -1;
                    List<int> availableRooms = new List<int>();

                    for (int k = 0; k < rooms.Length; k++)
                    {
                        int p = rooms[k].ProbabilityOfSpawning(i, j);

                        if (p == 2)
                        {
                            randomRoom = k;
                            break;
                        }
                        else if (p == 1)
                        {
                            availableRooms.Add(k);
                        }
                    }

                    if (randomRoom == -1)
                    {
                        if (availableRooms.Count > 0)
                        {
                            randomRoom = availableRooms[UnityEngine.Random.Range(0, availableRooms.Count)];
                        }
                        else
                        {
                            randomRoom = 0;
                        }
                    }


                    var newRoom = Instantiate(rooms[randomRoom].room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    newRoom.UpdateRoom(currentCell.status);
                    newRoom.name += " " + i + "-" + j;


                    yield return new WaitForSeconds(0.3f);
                }
            }
        }

        yield return new WaitForSeconds(0.3f);
    }

    void MazeGenerator()
    {
        dungeonGrid = new List<Cell>();

        if (generationSize.x < 1 || generationSize.y < 1)
        {
            Debug.LogError("Generation size should not be 0!");
            return;
        }

        for (int i = 0; i < generationSize.x; i++)
        {
            for (int j = 0; j < generationSize.y; j++)
            {
                dungeonGrid.Add(new Cell());
            }
        }

        int currentCell = startPos;
        Stack<int> path = new Stack<int>();
        int k = 0;

        while (k < 1000)
        {
            k++;

            dungeonGrid[currentCell].visited = true;

            if (currentCell == dungeonGrid.Count - 1)
            {
                break;
            }

            if (generationSize.x < 1 && generationSize.y < 1)
            {
                Debug.LogError("Invalid size");
                return;
            }

            if (currentCell == dungeonGrid.Count - 1)
            {
                break;
            }

            // Check the cell's neighbors
            List<int> neighbors = CheckNeighbors(currentCell);

            if (neighbors.Count == 0)
            {
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);

                int newCell = neighbors[UnityEngine.Random.Range(0, neighbors.Count)];

                if (newCell > currentCell)
                {
                    // down or right
                    if (newCell - 1 == currentCell)
                    {
                        dungeonGrid[currentCell].status[2] = true;
                        currentCell = newCell;
                        dungeonGrid[currentCell].status[3] = true;
                    }
                    else
                    {
                        dungeonGrid[currentCell].status[1] = true;
                        currentCell = newCell;
                        dungeonGrid[currentCell].status[0] = true;
                    }
                }
                else
                {
                    // up or left
                    if (newCell + 1 == currentCell)
                    {
                        dungeonGrid[currentCell].status[3] = true;
                        currentCell = newCell;
                        dungeonGrid[currentCell].status[2] = true;
                    }
                    else
                    {
                        dungeonGrid[currentCell].status[0] = true;
                        currentCell = newCell;
                        dungeonGrid[currentCell].status[1] = true;
                    }
                }
            }
        }

        // Start generating the dungeon with delays
        StartCoroutine(GenerateDungeon());

        // After the dungeon is generated, place the boss in the final room
        PlaceFinalBoss();
    }

    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        // check up neighbor
        if (cell - generationSize.x >= 0 && !dungeonGrid[Mathf.FloorToInt(cell - generationSize.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - generationSize.x));
        }

        // check down neighbor
        if (cell + generationSize.x < dungeonGrid.Count && !dungeonGrid[Mathf.FloorToInt(cell + generationSize.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + generationSize.x));
        }

        // check right neighbor
        if ((cell + 1) % generationSize.x != 0 && !dungeonGrid[(cell + 1)].visited)
        {
            neighbors.Add((cell + 1));
        }

        // check left neighbor
        if (cell % generationSize.x != 0 && !dungeonGrid[(cell - 1)].visited)
        {
            neighbors.Add((cell - 1));
        }

        return neighbors;
    }

    public Vector2 GetRoomCoordinates(int index)
    {
        int x = index % (int)generationSize.x;
        int y = index / (int)generationSize.x;
        return new Vector2(x, y);
    }


    void PlaceFinalBoss()
    {
        // Get coordinates of the final room
        int finalRoomIndex = dungeonGrid.Count - 1;
        Vector2 finalRoomCoordinates = GetRoomCoordinates(finalRoomIndex);

        // Instantiate the boss in the middle of the final room
        Vector3 bossPosition = new Vector3(finalRoomCoordinates.x * offset.x + offset.x / 2, 0, -finalRoomCoordinates.y * offset.y - offset.y / 2);
        Instantiate(finalBoss, bossPosition, Quaternion.identity, transform);
    }
}

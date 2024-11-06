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
    public GameObject room;
    public Vector2 offset;

    List<Cell> board;

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
                Cell currentCell = board[Mathf.FloorToInt(i + j * generationSize.x)];

                if (currentCell.visited)
                {
                    var newRoom = Instantiate(room, new Vector3(i * offset.x, 0, -j * offset.y), quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    newRoom.UpdateRoom(currentCell.status);
                    newRoom.name += " " + i + " " + j;
                }

                // Wait for 1 second before continuing to the next room
                yield return new WaitForSeconds(0.3f);
            }
        }

        yield return new WaitForSeconds(1f);
    }

    void MazeGenerator()
    {
        board = new List<Cell>();

        if (generationSize.x < 1 || generationSize.y < 1)
        {
            Debug.LogError("Generation size should not be 0!");
            return;
        }

        for (int i = 0; i < generationSize.x; i++)
        {
            for (int j = 0; j < generationSize.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;
        Stack<int> path = new Stack<int>();
        int k = 0;

        while (k < 1000)
        {
            k++;

            board[currentCell].visited = true;

            if (currentCell == board.Count - 1)
            {
                break;
            }

            if (generationSize.x < 1 && generationSize.y < 1)
            {
                Debug.LogError("Invalid size");
                return;
            }
            board[currentCell].visited = true;

            if (currentCell == board.Count - 1)
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
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else
                    {
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    // up or left
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else
                    {
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
                    }
                }
            }
        }

        // Start generating the dungeon with delays
        StartCoroutine(GenerateDungeon());
    }

    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        // check up neighbor
        if (cell - generationSize.x >= 0 && !board[Mathf.FloorToInt(cell - generationSize.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - generationSize.x));
        }

        // check down neighbor
        if (cell + generationSize.x < board.Count && !board[Mathf.FloorToInt(cell + generationSize.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + generationSize.x));
        }

        // check right neighbor
        if ((cell + 1) % generationSize.x != 0 && !board[(cell + 1)].visited)
        {
            neighbors.Add((cell + 1));
        }

        // check left neighbor
        if (cell % generationSize.x != 0 && !board[(cell - 1)].visited)
        {
            neighbors.Add((cell - 1));
        }

        return neighbors;
    }
}

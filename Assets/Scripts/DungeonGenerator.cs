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
        public bool[] status = {
            false,
            false,
            false,
            false
        };
    }

    public Vector2 size;
    public int startPos = 0;

    public GameObject room;

    public Vector2 offset;

    List<Cell> board;

    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateDungeon()
    {
        for(int i = 0; i < size.x; i++)
        {
            for(int j = 0; j < size.y; j++)
            {
                var newRoom = Instantiate(room, new Vector3(i*offset.x, 0, j*offset.y), quaternion.identity, transform).GetComponent<RoomBehaviour>();
                newRoom.UpdateRoom(board[Mathf.FloorToInt(i+j*size.x)].status);
            }
        }
    }

    void MazeGenerator()
    {
        board = new();
        
        for(int i = 0; i < size.x; i++)
        {
            for(int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }
        Debug.Log(board[0].status[0]);

        int currentCell = startPos;

        Stack<int> path = new();

        int k = 0;

        while(k<1000)
        {
            k++;

            board[currentCell].visited = true;

            // Check the cell's neighbors
            List<int> neighbors = CheckNeighbors(currentCell);

            if(neighbors.Count == 0)
            {
                if(path.Count == 0)
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

                if(newCell>currentCell)
                {
                    // down or right
                    if(newCell - 1 == currentCell)
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
                    if(newCell+1 == currentCell)
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
        GenerateDungeon();
    }

    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new();

        // check up neighbor
        if(cell - size.x >= 0 && !board[Mathf.FloorToInt(cell - size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell-size.x));
        }

        // check down neighbor
        if(cell + size.x < board.Count && !board[Mathf.FloorToInt(cell + size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + size.x));
        }

        // check right neighbor
        if((cell + 1) % size.x != board.Count && !board[Mathf.FloorToInt(cell + 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + size.x));
        }

        // check left neighbor
        if(cell % size.x != 0 && !board[Mathf.FloorToInt(cell - 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - 1));
        }


        return neighbors;
    }
}

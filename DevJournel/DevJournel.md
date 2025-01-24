# Game Project (PuzzleCrypt)

FGCT7010: Game Engine Programming and Development

Ross Bates

SID: 1911666

# Major Sources

- Game URL: https://rossb2k.itch.io/puzzlecrypt
- Github: https://github.com/Siohfox/SFox-UCA-WallThrough
- Gameplay video: https://youtu.be/zrWTK9pZsRc

# Introduction
PuzzleCrypt, previously known as 'Wallthrough' is a bird's-eye-view 3D puzzle solving game where the player must traverse through a procedurally generated dungeon like environment, solving puzzles to unlock a code of colours to input into the randomly generated colour code which will open the door into the next room, until they reach the exit portal at the end of the dungeon. The puzzles at the time of release are limited but can easily be expanded and improved due to the ease of the creation of the puzzles.

### Research

#### Identified resources

For the task I decided to focus on doing a top down puzzle game for PC and have it adaptable for mobile. To achieve this, i needed to research how to implement games on different devices. I wanted to look at specific ways that controls and other factors that might be changed when it comes to making a game that can be adapted for various environments.

Additionally, I needed to decide on which engine I wanted to implement my game in. I had a clear choice between Unity 3D and Unreal Engine.

Unreal:
Unreal engine provides good 3D graphics and implementations. It provides simple ways to implement games using their Blueprints coding system as well as a choice of implementing C++ classes.
On the downside, I am less experienced with Unreal Engine, so it would take mroe time to research how to use the engine properly to achieve the design of my game.

Unity is an engine I am confident in and have been taught fluently. Additionally, the intuitive C# makes it easier than Unreal's blueprints and c++ logic to write the code for code breaking and animations in Unity.

I wanted to avoid sources that led to conviluted game design, or options that involved heavy change to the engine or runtime. Sources like these could be detrimental to the implementation of the project, as it would make it more difficult to design and create a game if the design of the controls and structure for most devices is not fluent and easy. 

I wanted to create a birds eye view top down style as pictured below in my intial draft art:

<img src="https://raw.githubusercontent.com/Siohfox/SFox-UCA-WallThrough/refs/heads/main/Screenshots/IntialDesign.png" alt="drawing" width="1000"/>

Given all this, the clear resources I need to identify and research were:
- Unity's new Input system
- Virtual Joysticks for mobile implementation
- Puzzle game mechanics

Since I already have prior knowledge of implementation on Unity's animation system and also knowledge on coding in general, I will be able to make the puzzle mechanics without external research and create moving doors and UI animations.


### Sources

#### Mobile Adaptability
I used some videos on how to implement games for Android to see if I could get it working for mobile. I used the videos below to identify and research how to do this. The source is fairly reputable with many views. I analysed how the design was implemented in the video's version and applied it to my project.

[![A video](https://img.youtube.com/vi/Nb62z3J4A_A/0.jpg)](https://www.youtube.com/watch?v=Nb62z3J4A_A)

When implementing this feature, it became clear that using unity's new Input system would be challenging and create some problems in the code leading to loss in some functionality and difficulty in implementation, however I stuck with it anyway because it could allow the project to be more adaptable to different environments.
While I dislike the new input system and its implementation and use, I appreciate that it allows for mobile and pc controls at the same time, and there are ways to identify which platform you are using and switch accordingly.


Additionally I needed a way to use these controls to move my player around. This meant I needed to see how the new input system worked and how I could implement a way for the player to move. I decided on using a virtual joystick because it would allow mobile users to move around and also drag stuff on the screen if needed. 
I used a website from Micha Davis for help in implementing a virtual joystick. This blog tutorial allowed me to figure out how to implement the system in my game. The site goes into detail on how to use the Input action system and how to create the virtual joystick.

Source:
- Davis, M. (no date) Mobile Virtual Joystick Movement with Unity’s Input System, Medium. Available at: https://micha-l-davis.medium.com/ (Accessed: Oct 15, 2024). 

#### Procedural Generation

I decided that I would make the generation of the levels fully procedural. This not only includes the colour code for the doors which will be randomized on every playthrough, but also the dungeon itself.
While I will be able to make a random colour system myself, I needed to research how to make a procedurally generated dungeon with doors that open to the next room, while placing my own implementation of my colour-code doors inbetween.
I used a video by Youtuber SilveryBee to research how to make a procedural dungeon using a DFS (Depth-First Search) algorithm with dungeon rooms. This video went into detail about the code used as well as some assets which can implement such a mechanic.

[![A video](https://img.youtube.com/vi/gHU5RQWbmWE/0.jpg)](https://www.youtube.com/watch?v=gHU5RQWbmWE)

(Accessed: November 4, 2024). 

#### Cinemachine Cameras & Top down view

I started by watching the video titled "THIRD PERSON MOVEMENT in Unity" from a well known youtuber named 'Brackeys', a person known for their helpful contributions towards the Unity game dev community, to get a solid understanding of how to implement a third-person camera. Since I was looking for a smooth and dynamic camera system for my project, I wanted to combine this knowledge of a well known and widely used tool called Cinemachine, a software for making smooth camera systems. To research more into Cinemachine cameras, I found a video by Unity's official Youtube channel, where they instructed on how to efficiently set up a third person camera for Unity using Cinemachine.

[![A video](https://img.youtube.com/vi/4HpC--2iowE/0.jpg)](https://www.youtube.com/watch?v=4HpC--2iowE)

[![A video](https://img.youtube.com/vi/537B1kJp9YQ/0.jpg)](https://www.youtube.com/watch?v=537B1kJp9YQ)

(Accessed: October 8, 2024).
(Accessed: October 8, 2024).

#### Settings menu

Finding another video by the Youtuber 'Brackeys' I researched on how to implement a settings menu into my game, to allow the change of graphics, resolution, fullscreen and volume. Since Brackeys is a reputable youtuber, I put faith into the fact he would be able to guide me successfully.

[![A video](https://img.youtube.com/vi/YOaYQrN1oYQ/0.jpg)](https://www.youtube.com/watch?v=YOaYQrN1oYQ)

#### Puzzle Games

To research puzzle games, as I have not had much experience with the genre, I looked at previously made puzzle games, such as 'Mini Metro', a puzzle game with a simplistic design where you must create train tracks to allow people to get to their distinctly coloured destinations. The game seems to have been fairly popular on free websites as a simplistic game. I analysed the simple design and the puzzle design itself to see how the game functioned and how people engaged.

Another puzzle game I looked at was 'Stack n Sort', a free game made in unity where the player must stack and sort (ha) various coloured rings onto different poles. The game itself is not very reputable and if i were to ask anyone about it they likely would not have heard of it. The website I played it on had no ratings either which made it sketchy, however it functioned as expected and the gameplay was fine.

An interesting note to take away is that a lot of the games I looked at introduced colours into their gameplay mechanics, similar as I planned to do. This was ideal as it helped me understand how colours could be used in puzzle games.

I liked how the games made you think about what you needed to do to achieve a victory, and while puzzles introduce boredom for me personally after a while, I can see why people might enjoy spending some time solving these puzzles and even being rewarded for them in game.
There are many different types of puzzles so the genre is broad, however they always imply strategy and creativity in how the player might solve the puzzle, even if there's only one way to solve it.

Sources:
- https://www.miniplay.com/game/mini-metro
- https://simple.game/play/stack-n-sort/

I also wanted to implement a puzzle game where you connect a wire to a socket to complete the puzzle. I don't know anything about wires and smooth physics so I had to research this. I came across some tutorials which I later attempted to implement in my project, but with minimal success:

[![A video](https://img.youtube.com/vi/8rI1D1YQmhM/0.jpg)](https://www.youtube.com/watch?v=8rI1D1YQmhM)

## Implementation

### Design

#### Gameplay Loop

I created an initial design with practices such as a flow chart for the gameplay loop, as seen below. The design for the game is to let the player solve 'mini puzzles' which give the codes to open up the door into the next room.

![Initial Image](https://raw.githubusercontent.com/Siohfox/SFox-UCA-WallThrough/refs/heads/main/Screenshots/Diagram.png)

#### Structure of code

First: it's important to note that the diagram here is accurate to the final design as it needed updates when I added some unforeseen mechanics such as the timer used to end the game, and the removal of a flooding mechanic which was originally supposed to pressure the player into proceeding quickly instead, but made visibility very difficult. (This will be discussed in the conclusion)

I created a diagram using the website 'Figma', a website designed for creating and sharing diagrams. I used it's features and software to create a structure that my code will be written in, what it will do and how it might be implemented.
The diagram shows that I will use a heavy group of managers at the top which will control most aspects of the game, from the gameplay loop to the graphics, audio 

<img src="https://raw.githubusercontent.com/Siohfox/SFox-UCA-WallThrough/refs/heads/main/Screenshots/DesignDiagram.jpg" alt="Initial Image" width="1800" height="1200">

#### Creating game

##### Initial implementation
First I created an initial tutorial area, the game simply consisted of a line of coloured cubes, the player could use these to identify what the colour code was, and had to quickly implement it by entering it into the UI that pops up when they walk near the previously-named QuickTimeWall. The room will flood with water slowly, and the player had an oxygen meter which works similar to the drowning mechanic from the game 'Minecraft', where over time they would run out of breath, and then slowly tick down in health. The camera reacted by shaking when the player took damage, to let them know they were dying. When the door opens, the water level would balance out as it flooded into the second room, giving the player time to recover breath and open the next door.

##### Feedback
The feedback for the game was that it was fine, however the flooding mechanic made it hard to see. To resolve this, I ended up removing the flooding mechanic entirely and replacing it with a timer that counted down over time, and when it reached 0 the player would lose, instead of drowing. This made the breath mechanic redundant, so it was also removed.

![Initial Image](https://raw.githubusercontent.com/Siohfox/SFox-UCA-WallThrough/refs/heads/main/Screenshots/Timer.jpg)

A second feedback was that the colours were hard to remember, and my intention was to make a puzzle game not a memory game, so I ended up making it so the code appeared on the screen, then moved out of the way but remained visible so the player would not have to remember the colour code when they were entering it into the door. While this makes the colour mechanic of the door slightly less useful and takes away from teh original idea of the game, I think this turned out to be a better implementation which led to more fun. Testers also reported that they liked the animation of the code appearing and moving on the screen.


#### Procedural Generation

In my project, I used procedural generation to generate the game. This is used in the dungeon generating mechanic as researched from the video by Youtuber SilveryBee. By implementing some of their design, along with heavy inspiration by the game The Binding of Isaac's dungeon layout generation (see figure below), I created a DFS (Depth-First Search) generator.
The code below defines a DungeonGenerator class for generating a dungeon layout in Unity. The dungeon is represented as a grid, with each cell potentially containing a room. The generation process begins by creating a linear main path, with the option to add branch paths at random intervals. Each room type (e.g., basic, puzzle, or collectable) is assigned randomly to cells on the main path, except for the final room, which is always marked as the "Final" room. Branch paths can also be generated, leading to additional collectable rooms. The grid is populated with room prefabs, with rooms connected by doors, and a rotation is applied based on the connection direction. Once the dungeon is generated, an event is triggered to notify other scripts that the dungeon has been created so they can do their tasks which require the dungeon to exist, such as the objective counting by the ObjectiveManager class. The final output is a procedurally generated dungeon layout in Unity with connected rooms and varied room types.

![Initial Image](https://raw.githubusercontent.com/Siohfox/SFox-UCA-WallThrough/refs/heads/main/Screenshots/DungeonGameLayout.jpg)

Figure above shows the initial draft for how the dungeon should be generated

```c#
using System;
using System.Collections.Generic;
using UnityEngine;
using WallThrough.Core;
using WallThrough.Utility;

namespace WallThrough.Generation
{
    public enum RoomType { Basic, PushPuzzle, WiresPuzzle, Collectable, Special, Final }

    public class DungeonGenerator : MonoBehaviour
    {
        public int generationSize;
        public int startPos = 0;
        public Vector2 offset; // The amount of space that the room takes up
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
            public bool[] doorOpenStatus = new bool[4]; // Door open status: Up, Down, Left, Right       
            public Direction doorSpawnDirection;
            public float roomRotation;
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
                dungeonGrid[currentCell].doorOpenStatus[(int)Direction.Right] = true; // Disable Right Wall
                dungeonGrid[newCell].doorOpenStatus[(int)Direction.Left] = true; // Disable Left Wall

                dungeonGrid[currentCell].doorSpawnDirection = Direction.Right;

                dungeonGrid[currentCell].roomRotation = 90.0f;
            }
            else if (newCell == currentCell - 1) // Left
            {
                if (currentCell % width == 0) return; // Prevent connection at the edge
                dungeonGrid[currentCell].doorOpenStatus[(int)Direction.Left] = true; // Disable Left Wall
                dungeonGrid[newCell].doorOpenStatus[(int)Direction.Right] = true; // Disable Right Wall

                dungeonGrid[currentCell].doorSpawnDirection = Direction.Left;

                dungeonGrid[currentCell].roomRotation = 270.0f;
            }
            else if (newCell == currentCell + width) // Down
            {
                if (newCell >= dungeonGrid.Count) return; // Prevent connection beyond bottom edge
                dungeonGrid[currentCell].doorOpenStatus[(int)Direction.Down] = true;
                dungeonGrid[newCell].doorOpenStatus[(int)Direction.Up] = true;

                dungeonGrid[currentCell].doorSpawnDirection = Direction.Down;

                dungeonGrid[currentCell].roomRotation = 180.0f;
            }
            else if (newCell == currentCell - width) // Up
            {
                if (newCell < 0) return; // Prevent connection beyond top edge
                dungeonGrid[currentCell].doorOpenStatus[(int)Direction.Up] = true;
                dungeonGrid[newCell].doorOpenStatus[(int)Direction.Down] = true;

                dungeonGrid[currentCell].doorSpawnDirection = Direction.Up;

                dungeonGrid[currentCell].roomRotation = 0.0f;
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
                // GameManager.Instance.DebugRoomList();
            }

            // Tell anything waiting that the dungeon is finished generating
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
```

To define each room for the generation, i needed a class to define the rooms. This class was named RoomBehaviour, as it defined both the room and its behaviour, detailing how it should work and what should spawn in it. The RoomBehaviour class manages the behaviour and attributes of individual rooms within the dungeon. It is linked to the DungeonGenerator by handling the instantiation and configuration of rooms based on the cell data generated by the dungeon grid. Specifically, the UpdateRoom method is called from the DungeonGenerator to set up the room’s open doors which allow the rooms to connect to one another, and specific room behaviours (such as placing collectables or spawning a quick-time wall). The room's rotation and door states are updated based on the DungeonGenerator.Cell data, with walls being activated or deactivated depending on whether the corresponding doors are open. Each room can also contain rotatable objects, collectables, or puzzles, which are positioned and instantiated accordingly. In summary, RoomBehaviour takes the layout and configurations provided by DungeonGenerator and brings the rooms to life in the game world by managing their visual and interactive elements.

The RoomBehaviour class is detailed below:

```c#
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
        public Vector3 roomSize = new(48, 0, 48);
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

        // Helper method to encapsulate child bounds into a room's total bounds - necessary because of unity's terrible rotation around center
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
            Vector3 wallPosition = door.transform.position;
            GameObject quickTimeWall = Instantiate(quickTimeWallPrefab, wallPosition, door.transform.rotation);
            //quickTimeWall.GetComponent<Gameplay.QuickTimeWall>().doorTimeWorth = 15.0f;
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
```

#### Saving, Settings and Managers

Notable elements of my code include the key manager classes that handle saving the game, saving settings, and loading data. The interactions between these managers are outlined in the Figma diagram under the "Structure of Code" section above.

To explain how they function: The GameManager holds the game state and requests the SaveManager to load the game, returning the relevant data. The SaveManager, in turn, asks the OptionsManager to fetch saved settings from a .json file, allowing the settings to be applied. Finally, the GameManager interacts with the LoadManager to load the next scene, triggering an asynchronous load with a loading bar and a fade-out transition. These managers form the core of the game and are located in the Core namespace.

A summary of the three code blocks below shows how they interact as part of the core game management system in a Unity game, handling game state, options, and data saving/loading.

- GameManager: This class oversees the overall game flow, including managing the game state (e.g., playing, paused, or game over), score tracking, and level progression. It also handles saving and loading game progress via the SaveManager. The GameManager maintains references to the RoomBehaviour objects (representing the dungeon rooms), which are managed using SetCurrentRooms and accessed via GetCurrentRooms. The GameManager relies on the SaveManager to persist game state (score, level, unlocked levels).

- OptionsManager: This class manages game settings, such as audio volume, graphical quality, and fullscreen mode. It loads and saves these settings through the SaveManager, applying them using Unity’s built-in settings (e.g., audio mixer, quality settings). The OptionsManager listens for an event that is triggered when options data is loaded, ensuring the game applies saved settings when it starts. It interacts with the SaveManager to persist user preferences between sessions.

- SaveManager: This class handles reading and writing both game and options data to persistent storage. It uses JSON serialization to save and load game progress (score, level, unlocked levels) and user preferences (volume, quality, fullscreen). The SaveManager works with both the GameManager and OptionsManager, facilitating the saving and loading of relevant data as needed.

### Movement

The movement script is a separate player script that handles player movement and rotation in a Unity-based game. It requires a Rigidbody component to apply physics-based movement. The script allows the player to move in a direction relative to the camera's orientation. The Move function takes input in the form of a Vector2 from both keyboard and mobile touch input, which determines the direction of movement on the XZ plane. It then calculates the movement direction based on the camera's forward and right vectors, ignoring vertical components, and adjusts the player's velocity accordingly. The player is also smoothly rotated to face the movement direction using the RotateTowards function, which ensures a smooth turn rather than an instant snap. If no movement input is detected (i.e., when the magnitude of the input is too small), the player will stop moving by setting the horizontal velocity to zero while maintaining the vertical velocity (such as gravity). The script ensures the player character moves and rotates fluidly within the game world.
I believe this movement is fun and ideal, and feels good instead of snapping to turn, and i'm happy with how it turned out. The movement code is detailed below:

```cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WallThrough.Gameplay.Pawn
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {
        private Rigidbody _rb;

        public float moveSpeed = 5f;
        public float rotationSpeed = 720f; // Degrees per second
        public Transform cameraTransform; // Reference to the camera transform

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void Move(Vector2 moveDir)
        {
            // Use a threshold to prevent very small inputs from affecting movement
            if (moveDir.magnitude < 0.1f)
            {
                // Stop the player's movement when there is no input
                _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
                return;
            }

            // Get the camera's forward and right vectors (projected to the XZ plane)
            Vector3 cameraForward = cameraTransform.forward;
            cameraForward.y = 0; // Ignore the vertical component (we don't want to move up/down)
            cameraForward.Normalize();

            Vector3 cameraRight = cameraTransform.right;
            cameraRight.y = 0; // Ignore the vertical component
            cameraRight.Normalize();

            // Calculate the movement direction relative to the camera's orientation
            Vector3 targetDirection = cameraForward * moveDir.y + cameraRight * moveDir.x;

            // Rotate towards the target direction
            RotateTowards(targetDirection);

            // Move in the direction the player is facing
            Vector3 forward = transform.forward;
            _rb.velocity = new Vector3(forward.x * moveSpeed, _rb.velocity.y, forward.z * moveSpeed);
        }

        private void RotateTowards(Vector3 direction)
        {
            if (direction.magnitude > 0)
            {
                // Calculate the target rotation
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // Smoothly rotate towards the target rotation
                float step = rotationSpeed * Time.fixedDeltaTime; // Calculate the step size
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
            }
        }

        public Vector3 GetCurrentVelocity() => _rb.velocity;
    }
}
```

### Colour wall logic with puzzles and how they are created
The scripts create, coordinate, and set up QuickTimeWalls for each room by utilising a combination of room generation, event handling, and UI interaction.

RoomBehaviour (Generation):
When a room is generated, the RoomBehaviour class determines whether a QuickTimeWall should be instantiated. It checks conditions such as door openness and the room type to decide if the wall should appear.
If the conditions are met (e.g., the door is open and the room is not of type "Final"), it instantiates a QuickTimeWall at the door's position using the quickTimeWallPrefab.

QuickTimeWall (Gameplay):
Each QuickTimeWall contains the logic for triggering the quick-time event. This is done by presenting the player with a colour code puzzle through the QuickTimeMenu. The QuickTimeWall is responsible for managing the colour code, comparing player inputs against a randomly generated colour code, and responding with success or failure.
When the player interacts with the wall, the InteractionStart method is triggered, which activates the QuickTimeMenu for input. The CompareCodes method checks if the player's input matches the required code. If successful, it opens the wall, disables the collider, and rewards the player by adding time to a countdown.

QuickTimeMenu (Gameplay/UI):
The QuickTimeMenu handles the user interface for the colour code puzzle. It tracks the player's inputs and visually updates the input fields. Once the required number of inputs is reached, it processes the code and compares it against the one stored in the QuickTimeWall.
The menu is dynamically set up to match the number of inputs required for the current wall, and the InputColour method updates the UI as the player provides input.

In summary, these scripts coordinate the creation of QuickTimeWalls in rooms by setting up procedural generation (in RoomBehaviour), handling player interaction and input (in QuickTimeWall and QuickTimeMenu), and triggering events such as success or failure based on the player's performance. The walls themselves are spawned dynamically depending on the room configuration, allowing for varying gameplay experiences.


![Initial Image](https://raw.githubusercontent.com/Siohfox/SFox-UCA-WallThrough/refs/heads/main/Screenshots/QuickTimeWallLogicWithPuzzles.jpg)

### Additional Implemented/Unimplemented Feedback

Some extra feedback included that the game was too dark. I attempted to make the game lighter and introduced some post processing. I cannot say it was too successful, but it was improved a bit.
Feedback given about the damage system included taking damage when inputting the code wrong, which was swiftly implemented and I believe it is a nice change, allowing the player health to have a use and the development time not to be wasted.
Someone also suggested having a sprint key, as they didn't like how slow the character was, so this was implemented too.
Some people asked for colour-blindness accessibility options, however I ran out of time before implementing this change, I would add it if I had more time.
Finally, feedback was given about the push puzzle, where two people suggested one of the cubes be a sphere for more difficultness. This was added.

## Critical Reflection

### What did or did not work well and why?

The colour generation and input mechanic worked really well and i'm very proud of it, even if it ended up not being the main part of the game. It was initially supposed to be the point of the game, but quickly became secondary the experience when minipuzzles were added. I should've focused more on how that element would fit in to the game.

I also really enjoyed coding the way that the dungeon was generated, creating the algorithm and implementing it into the design was both frustrating and fun, as when it worked it was very fun and impressive to test and watch. It did cause many problems though, a lot of things break when trying to do procedural generation, and it is difficult to correct. I had to go through many iterations of the creation process, as it would often break, not rotate, or lead to a dead end. I spent at least 2 weeks trying to fix the mechanic where the rooms would not rotate, however I finally fixed that issue with some hidden and obscure code i found online.

The puzzle game creation process did not go exceedingly well. As expected, puzzles were difficult to create when I had little experience in puzzle games, or puzzles in general. I found creating puzzles very difficult without a lot of knowledge on how most puzzles work, function and finish.

Assets and asset creation was also difficult, only having access to free assets I found online for every model and sound I used created an environment where most assets clashed and did not work well together. This is because I have little experience in creation of art and assets myself, as I am much better with mechanic implementation than creative design.

A mini puzzle I implemented included a wire attaching puzzle, where you had to drag a wire and attach it to a socket. The wire creation itself took a lot more time than I hoped and didn't function correctly, nor was I able to fully fix it. I am disappointed I could not get the mesh generation working, and I wish I had more experience in this.

### What would you do differently next time?

If I were to have done a game creation differently, I would have likely picked a genre I am more familiar with, as it would both provide better motivation and also be a smoother experience as i'd have prior knowledge of what to expect and what mechanics to implement for the optimal experience. If i were working with a team, i would likely have an easier time.

Additionally, I would have focused on adding stuff like enemies and utilizing some of the mechanics I've added but were not used, such as game state saving and player stats and health. I'm happy they work but i'm disappointed I didn't have enough time to implement them in a meaningful way.

- Are there any new approaches, methodologies or different software that you wish to incorporate if you have another chance?

There are different approaches to dungeon generation, however I am happy with the DFS algorithm I implemented, as it makes the most sense in a 2D grid layout I used in my game. While I would have liked to have used Unreal Engine 5 for the project, I don't have enough experience to create a whole game, and I would not have had enough time to both learn and create a whole game in the deadline.
I would also have liked to have used a music software to create some of my own music, however I did not have access to the paid software I needed, and ended up using free assets instead.

- Is there another aspect you believe should have been the focus?

I believe the focus should have been more about the puzzles and puzzle mechanics as the game was originally designed for, along with the colour code mechanics, however the game ended up being not too puzzle oriented in the end due to my inexperience with puzzle games, despite there being a lot of puzzle games out there, not many worked for my idea and I found it hard to get inspiration or motivation for the puzzles I created.

## Declared Assets

- **Freesound Correct sound** - ‘buttonchime02up.wav’ by justinbw. Available at: [Freesound](https://freesound.org/people/JustinBW/sounds/80921/) (Accessed: 11 October 2024).
- **Freesound Incorrect sound** - ‘IncorrectSound.wav’ by RICHERlandTV. Available at: [Freesound](https://freesound.org/people/RICHERlandTV/sounds/216090/) (Accessed: 11 October 2024).
- **Freesound Hint sound** - ‘hint.wav’ by dland. Available at: [Freesound](https://freesound.org/people/dland/sounds/320181/) (Accessed: 11 October 2024).
- **Freesound Button Click sound** - ‘Button Click.wav’ by . Available at: [Freesound](https://freesound.org/people/KorgMS2000B/sounds/54405/) (Accessed: 11 October 2024).
- **Freesound Door open sound** - ‘conrect blocks moving2.wav’ by FreqMan. Available at: [Freesound](https://freesound.org/people/FreqMan/sounds/25846/) (Accessed: 11 October 2024).
- **Freesound Intense music** - ‘Epic Music loop’ by FusionWolf3740. Available at: [Freesound](https://freesound.org/people/FusionWolf3740/sounds/570463/) (Accessed: 11 October 2024).
- **Freesound DoorSuccess sound** - ‘Powerup/success.wav’ by GabrielAraujo. Available at: [Freesound](https://freesound.org/people/GabrielAraujo/sounds/242501/) (Accessed: 11 October 2024).
- **Freesound Damage sound** - ‘Damage sound effect’ by Raclure. Available at: [Freesound](https://freesound.org/people/Raclure/sounds/458867/) (Accessed: 11 October 2024).
- **Freesound Player death sound** - ‘player_death_ui_show.wav’ by nfsmaster821. Available at: [Freesound](#) (Accessed: 11 October 2024).
- **Freesound Whoosh sound** - ‘Swinging staff whoosh (low) 10.wav’ by Nightflame. Available at: [Freesound](https://freesound.org/people/Nightflame/sounds/422502/) (Accessed: 11 October 2024).
- **Old wooden door** - 3D model by Ellie3D, Sketchfab. Available at: [Sketchfab](https://sketchfab.com/3d-models/old-wooden-door-1cec090cd02142b094802d242e03ee7c) (Accessed: 21 January 2025).
- **Portal frame** - 3D model by soidev, Sketchfab. Available at: [Sketchfab](https://sketchfab.com/3d-models/portal-frame-da34b37a224e4e49b307c0b17a50af2c) (Accessed: 21 January 2025).
- **Fantasy Wooden gui: Free: 2d Gui (2019)** - Unity Asset Store. Available at: [Unity Asset Store](https://assetstore.unity.com/packages/2d/gui/fantasy-wooden-gui-free-103811#content) (Accessed: 21 January 2025).
- **Allsky free - 10 sky / skybox set: 2d sky (2024)** - Unity Asset Store. Available at: [Unity Asset Store](https://assetstore.unity.com/packages/2d/textures-materials/sky/allsky-free-10-sky-skybox-set-146014) (Accessed: 29 October 2024).
- **Dungeon modular pack: 3D dungeons (2024)** - Unity Asset Store. Available at: [Unity Asset Store](https://assetstore.unity.com/packages/3d/environments/dungeons/dungeon-modular-pack-295430) (Accessed: 29 October 2024).
- **Maxwell Cat** - 3D model by popuitwe1, Sketchfab. Available at: [Sketchfab](https://sketchfab.com/3d-models/maxwell-cat-70680512cfcd4aa68d2c20548bb4bb8f) (Accessed: 2 October 2024).

The following assets were modified with the use of GPT 4o:
- LevelManager.cs
- SettingsManager.cs
- MiniPuzzlePressurePlate.cs
- Movement.cs
- Development Journal.html

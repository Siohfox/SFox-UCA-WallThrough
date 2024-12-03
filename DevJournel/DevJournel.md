# Game Project (Wallthrough)

FGCT7010: Game Engine Programming and Development

Ross Bates

SID: 1911666
## Week 1

### Research

#### Identified resources

```Markdown
# Identified resources

For the task I decided to focus on doing a top down puzzle game for PC and mobile. To achieve this, i needed to research how to implement games on Android. I wanted to look at specific ways that controls and other factors that might be changed when it comes to making a joint game for both PC and Android Mobile. This research is justified because game design architechture changes when designing for multiple platforms.

Additionally, I needed to decide on which engine I wanted to implement my game in. I had a clear choice between Unity 3D and Unreal Engine.

Unreal:
Unreal engine provides good 3D graphics and implementations. It provides simple ways to implement games using their Blueprints coding system as well as a choice of implementing C++ classes.
On the downside, I am less experienced with Unreal Engine, so it would take mroe time to research how to use the engine properly to achieve the design of my game.

Unity is an engine I am confident in and have been taught fluently. Additionally, the intuitive C# makes it easier than Unreal's blueprints and c++ logic to write the code for code breaking and animations in Unity.

I wanted to avoid sources that led to conviluted game design, or options that involved heavy change to the engine or runtime. Sources like these could be detrimental to the implementation of the project, as it would make it more difficult to design and create a game if the design of the controls and structure for mobile and pc is not fluent and easy. 

```

#### Sources

- An opening paragraph about the source stating the author, developer or organisation, this paragraph should explain the source's influence, credentials, critical reception, awards, reputation or any issues with the source. For example, if the source is not reputable. If the source is a game, the issues that occurred during development or if had a poor launch.
- List the aspects analysed in reference to the current task.
- An ending paragraph stating what you enjoyed or disliked, what you agreed with or not agree with.

```Markdown
# Documentation

I wanted to create a dungeon type game where the player must traverse through rooms, finding a 'colour code' to enter into a door and enter the next room, until they reach the end. To do this, i applied techniques I have already learnt about the Unity engine such as the implimentation of simple codebreaking mechanics applied with mobile controls.

I created a simple 

# Example Game Source of Top Down dungeon traversing Game

Binding of Isaac is a top down view dungeon traversing game known as a Roguelike(What is a Roguelike? The Beginner's Guide, LifeWire.com, May 28, 2020) by independent developers Edmund McMillen and Florian Himsl. The game features a character who traverses through a basement setting, a structure of rooms separated by walls. The player has to defeat enemies in the room before being able to move to the next. The dungeon is generated procedurally. (The Binding of Isaac, 2011).

I found the implementation of this top down view game where the player traverses a dungeon inspirational and similar in mechanics to what I want to achieve. I didn't want to focus on procedural generation yet, as I wasn't sure if it would help achieve the vision of the game I wanted to make.
```

## Implementation

### Task: Implement simple movement with a third person controller

- What was the process of completing the task at hand? Did you do any initial planning?
- Did you receive any feedback from users, peers or lecturers? How did you react to it?

#### Task 1 - Implement Camera
```Markdown
I needed a third person controller for my player. I decided to use my knowledge of creating controllers to implement my own third person controller.

I used an asset called Cinemachine to create a third person camera. Using the asset, I added a 'CinemachineBrain' to the Main Camera to allow Cinemachine to implement its own Camera system to override Unity's main camera using their Documentation. (Cinemachine Documenation, https://docs.unity3d.com/Packages/com.unity.cinemachine@2.3/manual/index.html, Accessed 2024)
Additionally, I added a virtual camera which would be the camera that follows the player. This Camera was given a Transposer method for following the Body with a binding mode of World Space.
I added the Aim to be Composer, allowing the Camera to aim at the Player no matter where it is moving.

Utilizing the functions built in to the asset to create a camera that smoothly follows the player from afar.

```

<br>

```csharp
using UnityEngine;
public class HelloWorld : MonoBehaviour 
{
    public void Start() 
    {
        Debug.Log("Hello World!");
    }
}
```


*Figure 1. An example of using a script as a figure. This script has a `Start()` method!*

### What creative or technical approaches did you use or try, and how did this contribute to the outcome?

- Did you try any new software or approaches? How did the effect development?

<br>

![onhover image description](https://beforesandafters.com/wp-content/uploads/2021/05/Welcome-to-Unreal-Engine-5-Early-Access-11-16-screenshot.png)
*Figure 2. An example of an image as a figure. This image shows where to package your Unreal project!.*

### Did you have any technical difficulties? If so, what were they and did you manage to overcome them?

- Did you have any issues completing the task? How did you overcome them?

## Outcome

Here you can put links required for delivery of the task, ensure they are properly labelled appropriately and the links function. The required components can vary between tasks, you can find a definative list in the Assessment Information. Images and code snippets can be embedded and annotated if appropriate.

- [Example Video Link](https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstley)
- [Example Repo Link](https://github.com/githubtraining/hellogitworld)
- [Example Build Link](https://samperson.itch.io/desktop-goose)

<iframe width="560" height="315" src="https://www.youtube.com/embed/dQw4w9WgXcQ?si=C4v0qHaYuEISAC01" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>

*Figure 3. An example of an embedded video using a HTML code snippet.*

<iframe frameborder="0" src="https://itch.io/embed/2374819" width="552" height="167"><a href="https://bitboyb.itch.io/nephilim-resurrection">Nephilim Resurrection (BETA) by bitboyb</a></iframe>

*Figure 4. An example of a itch.io widget*

## Critical Reflection

### What did or did not work well and why?

- What did not work well? What parts of the assignment that you felt did not fit the brief or ended up being lacklustre.
- What did you think went very well? Were there any specific aspects you thought were very good?

### What would you do differently next time?

- Are there any new approaches, methodologies or different software that you wish to incorporate if you have another chance?
- Is there another aspect you believe should have been the focus?

## Bibliography

- Please use the [harvard referencing convention](https://mylibrary.uca.ac.uk/referencing).

Video game development (2024) In: Wikipedia. At: https://en.wikipedia.org/w/index.php?title=Video_game_development&oldid=1240603537 (Accessed  03/09/2024).

## Declared Assets

- Please use the [harvard referencing convention](https://mylibrary.uca.ac.uk/referencing).

Infinity Blade: Adversaries in Epic Content - UE Marketplace (s.d.) At: https://www.unrealengine.com/marketplace/en-US/product/infinity-blade-enemies (Accessed  09/09/2024).

---

```Markdown
# General Tips

- Use plenty of images and videos to demonstrate your point. You can embed YouTube tutorials, your own recordings, etc.
- Always reference! Even documentation, tutorials and anything you used for your assignment. Use an inline reference for the sentence and a bibliography reference at the end.
- Word count is not important, you can also chose to use bullet points. As long as it is clear and readable, the format your decide to use can be flexible.
- You are free to use AI but please ensure you have made a note in the declared assets, for example if you have a script called Test.cs , you should note that AI was used to in the creation of this script. You can use a bullet point list for each asset used like:

The following assets were created or modified with the use of GPT 4o:
- Test.cs
- AnotherScript.cs
- Development Journal.html

```

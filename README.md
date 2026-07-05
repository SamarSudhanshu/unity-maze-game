# Simple Mobile Maze Game

## Project Overview

This project is a simple mobile maze game developed in Unity 6 using C#. The objective of the game is to guide a physics-based ball from the START zone to the FINISH zone while avoiding obstacles and maintaining the highest possible score.

The project was built as part of the Reliance Games Game Programmer Intern technical assessment. The focus was on implementing core Unity systems, writing organized code, and demonstrating gameplay programming fundamentals rather than creating a visually complex game.

---

## Features

### Player
- Physics-based sphere movement using Rigidbody.
- Keyboard controls in the Unity Editor.
- Accelerometer controls for Android devices.
- Adjustable accelerometer sensitivity through the pause menu.

### Maze
- Multiple navigation paths.
- Clearly marked START and FINISH zones.
- Single life gameplay.

### Obstacles
- Static obstacles.
- AI obstacle with:
  - Patrol between multiple waypoints.
  - Chase player when nearby.
- Obstacle collisions apply score penalties.

### Checkpoint System
- Checkpoints save the player's respawn position.
- Restart resets all checkpoints.

### Camera System
- Follow Camera.
- Top Down Camera.
- Camera can be switched using both a keyboard shortcut and a UI button.

### UI
- Main Menu
- HUD
- Pause Menu
- Win Screen
- Lose Screen
- Camera Button
- Sensitivity Slider

### Scoring System
Score is calculated using:
- Completion time
- Obstacle penalties
- Remaining score

The game displays:
- Current Score
- Final Score
- Best Scores

### Local Leaderboard
- Stores the Top 5 scores.
- Scores are sorted automatically.
- Leaderboard data is stored locally using JSON serialization and persists between sessions.

---

## Controls

### Unity Editor
- **WASD / Arrow Keys** - Move Player
- **Camera Switch Key** - Toggle Camera

### Android
- Tilt device to move the player.
- Camera button switches camera mode.
- Pause menu allows sensitivity adjustment.

---

## Technical Decisions

- Separate manager scripts were used for game flow, UI, camera, and leaderboard functionality to keep responsibilities separated.
- Unity's new Input System was used for keyboard and accelerometer input.
- Rigidbody physics was used for player movement.
- JSON serialization was chosen for saving leaderboard data because it is lightweight and easy to maintain.
- A simple state-based AI was implemented for the patrol enemy using Patrol and Chase behaviours.
- Scene management was separated into a Main Menu scene and a Game scene.

---

## Challenges Faced

Some of the main challenges during development included:

- Balancing accelerometer sensitivity across mobile devices.
- Resetting all game systems correctly during restart, including checkpoints and enemy states.
- Implementing persistent leaderboard storage.
- Making the UI work properly on different Android screen sizes.
- Designing the AI patrol and chase behaviour while keeping the code simple and modular.

---

## Future Improvements

Given more development time, I would like to add:

- Better mobile UI scaling and Safe Area support.
- Improved enemy AI with additional states.
- Audio effects and background music.
- Particle effects and visual polish.
- Difficulty levels.
- Online leaderboard support.
- Settings menu for graphics and audio.
- Better camera transitions.

---

## Unity Version

- Unity 6 LTS

---

## Project Structure

Scripts are organized by responsibility:

- PlayerController
- GameManager
- UIManager
- CameraManager
- PatrolEnemy
- CheckPoint
- LeaderboardManager

This structure keeps gameplay systems independent and easier to maintain.

---

## Build Platform

- Android
- Unity Editor (for testing)

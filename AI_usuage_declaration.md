# AI Usage Declaration

## AI Tool Used

- ChatGPT (OpenAI)

---

## Development Usage

ChatGPT was used as a development assistant throughout the project. It was mainly used to discuss implementation approaches, understand Unity concepts, review code, and troubleshoot issues encountered during development.

AI assistance included:

- Planning the overall project architecture.
- Guidance on Unity scene management.
- Implementing gameplay systems such as checkpoints, scoring, UI states, and camera switching.
- Designing and implementing a simple AI obstacle using Patrol and Chase behaviours.
- Implementing a local leaderboard using JSON serialization.
- Android build configuration and troubleshooting.
- Reviewing and improving code organization and readability.
- Debugging compilation errors and runtime issues.

The AI was used as a learning and productivity tool rather than replacing the implementation process.

---

## Manual Development

Although AI assisted with guidance and code suggestions, the project was implemented and integrated manually in Unity.

Manual work included:

- Creating the maze and gameplay environment.
- Building the UI using Unity's Canvas and TextMeshPro.
- Setting up scenes, prefabs, materials, colliders, and physics.
- Integrating scripts with Unity components through the Inspector.
- Testing gameplay mechanics and fixing bugs.
- Adjusting gameplay values such as movement force, camera settings, penalties, and accelerometer sensitivity.
- Building and testing the Android APK.
- Verifying all assignment requirements were completed.

AI-generated code was reviewed, modified where necessary, and integrated only after testing.

---

## Representative Prompts

### Prompt 1

> Help me design a clean architecture for a Unity mobile maze game with separate GameManager, UIManager, CameraManager, and PlayerController scripts.

### Prompt 2

> Implement a local leaderboard in Unity that stores the Top 5 scores using JSON serialization and persists between application sessions.

### Prompt 3

> Design and implement a simple AI obstacle that patrols between waypoints and follows the player when they enter a detection range.

---

## Validation Process

All AI-generated suggestions were validated before being accepted into the project.

Validation included:

- Testing features inside the Unity Editor.
- Building and testing on an Android device.
- Fixing compilation errors and runtime issues.
- Verifying gameplay behaviour after every major change.
- Testing restart logic, checkpoints, scoring, leaderboard persistence, camera switching, and UI state transitions.
- Tuning gameplay values based on practical testing rather than accepting generated values directly.

Several AI-generated suggestions required modification or adjustment after testing, particularly gameplay tuning such as accelerometer sensitivity, camera behaviour, restart flow, and UI layout on mobile devices.

---

## Engineering Judgment

AI was used to improve development efficiency, but every significant implementation was understood, tested, and adapted before inclusion in the final project.

The final submission reflects my understanding of Unity fundamentals, gameplay programming, and software design rather than relying solely on AI-generated code.

# Ralph Agent Configuration - Emberveil

## Project Overview

**Emberveil: The Valley of Sisters** is a 2D cozy narrative adventure game built in Unity 2022.3 LTS (URP) with C#. You play as Mouse, using context-sensitive Gloves to fix, mend, and explore a pastoral fantasy world.

## Technology Stack

| Component | Technology |
|-----------|-----------|
| Engine | Unity 2022.3 LTS |
| Renderer | Universal Render Pipeline (URP), 2D profile |
| Language | C# |
| Art Style | Pixel art (32x32 tiles) |
| Dialogue | Yarn Spinner |
| Version Control | Git + GitHub |

## Project Structure

```
Emberveil/
├── Assets/
│   ├── Scripts/
│   │   ├── Player/        # PlayerController.cs, GloveController.cs
│   │   ├── Interaction/   # IInteractable, Interactable, LiftableObject, TalkableNPC, ExaminableObject
│   │   ├── Systems/       # GameManager, InventoryManager, ItemData, SceneTransition
│   │   └── UI/            # (planned) DialogueUI, InventoryUI
│   ├── Scenes/            # Burrow, Workshop, Overworld, MainMenu
│   ├── Sprites/           # Characters/, Tiles/, UI/
│   ├── Dialogue/          # Yarn Spinner .yarn files
│   ├── Prefabs/
│   ├── ScriptableObjects/ # Items/
│   └── Audio/             # SFX/, Music/
├── Docs/
│   └── Implementation_Plan.md
└── .ralph/
    ├── PROMPT.md          # High-level project goals
    ├── fix_plan.md        # Prioritized task list
    ├── AGENT.md           # This file
    └── specs/             # Detailed system specifications
```

## Build Instructions

```bash
# Unity projects are built via the Unity Editor, not CLI
# For CI/headless builds:
# unity -batchmode -projectPath ./Emberveil -buildTarget StandaloneWindows64 -executeMethod BuildScript.Build -quit
echo "Unity project - open in Unity Editor to build"
```

## Test Instructions

```bash
# Unity tests run via Unity Test Runner
# For CI/headless test execution:
# unity -batchmode -projectPath ./Emberveil -runTests -testResults results.xml
echo "Unity project - use Unity Test Runner in Editor"
```

## Run Instructions

```bash
# Open in Unity Editor and press Play
echo "Unity project - open in Unity Editor and press Play"
```

## Development Guidelines

### Script Conventions
- All scripts use C# with XML documentation comments
- Singleton pattern for managers (GameManager, InventoryManager)
- Component-based architecture following Unity patterns
- Interface-driven interactables (IInteractable)
- ScriptableObjects for data-driven design (ItemData)
- Event-driven UI updates (OnItemAdded, OnInventoryChanged, etc.)

### Key Design Rules
- **No combat** - This is a cozy adventure about fixing, not fighting
- **Context-sensitive Gloves** are the core mechanic - every interaction goes through them
- **Companions** unlock new abilities (Bear = strength, Hawk = aerial view)
- **Kid-compatible** - sessions designed for 45-60 minute chunks
- **Ship early, ship often** - each phase must be playable

### Code Quality Standards
- Extensive XML documentation on public methods
- Null checking and error handling
- Debug logging for playtesting
- Configurable parameters exposed in Unity Inspector via [SerializeField]
- No external dependencies beyond Unity packages and Yarn Spinner

## Notes
- Scripts are in `Emberveil/Assets/Scripts/` (not the repo root)
- Scene files (.unity) and most Unity assets are binary - focus on .cs scripts
- Update this file when build process or structure changes

# Emberveil: The Valley of Sisters

A cozy narrative adventure game built in Unity 2D, based on the *Mouse, Hawk, and Bear* story series.

## About

You play as **Mouse**, an ancient being who wakes after a long slumber to find the valley transformed. Armed with her mysterious **Gloves**—technology that blurs the line between magic and engineering—you'll explore a pastoral world, help its inhabitants, and uncover what happened while you slept.

This is a game about fixing things, not fighting. You don't carry a sword; you carry curiosity.

## Status

🚧 **Early Development** - Building core systems

## Tech Stack

- Unity 2022.3 LTS (Universal Render Pipeline)
- C#
- Yarn Spinner for dialogue
- Pixel art visuals

## Getting Started

1. Install [Unity Hub](https://unity.com/download)
2. Install Unity 2022.3 LTS with the 2D (URP) template
3. Clone this repository
4. Open the project in Unity
5. Open `Assets/Scenes/Burrow.unity` to start

## Project Structure

```
Assets/
├── Scripts/
│   ├── Player/         # Mouse movement and Gloves
│   ├── Interaction/    # Interactable objects system
│   ├── Systems/        # Game manager, inventory, scene transitions
│   └── UI/             # Dialogue and inventory display
├── Scenes/             # Game levels
├── Sprites/            # Art assets
├── Dialogue/           # Yarn Spinner dialogue files
└── ScriptableObjects/  # Item definitions
```

## Core Mechanics

- **The Gloves**: Context-sensitive interaction. Right-click to activate, then interact with objects. Lift heavy things, talk to NPCs, examine the world.
- **No Combat**: This is a cozy adventure. Puzzles, dialogue, exploration.
- **Companions**: Recruit friends like Bear and Hawk who unlock new abilities.

## Based On

This game adapts the *Mouse, Hawk, and Bear* story series. The narrative explores themes of AI consciousness, memory, and what it means to care for a world you didn't create.

## License

Story and game design © Ben Corpron. All rights reserved.
Code is available for educational purposes.

---

*Built with kids, for anyone who wants to fix things instead of break them.*

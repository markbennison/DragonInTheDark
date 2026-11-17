# Dragon in the Dark

A C# console game used in **Dungeons & Devs: An Algorithm Adventure**, a hands-on introduction to algorithms, programming and game development.

## Getting started

1. Open `DragonInTheDark.sln` in Visual Studio on Windows (with .NET Framework 4.8 development support).
2. Run the project. Select **1** for the fixed training dungeon or **2** for a randomly generated dungeon.
3. Use **W** (up), **S** (down), **A** (left), **D** (right), or **M** (return to menu).
4. Find the hidden Portal Stone to escape. Avoid traps: they wake a dragon that pursues the player.

**The student starter intentionally has incomplete movement logic.** Start with the coding challenges below before expecting the game to work fully.

## Student coding challenges

Open `DragonInTheDark/Program.cs` and search for `TODO`.

1. **Move the Adventurer** — implement the four directions in `MakeMove()`. Moving up/down changes Y; moving left/right changes X.
2. **Stay in the Dungeon** — implement the boundary checks in `CheckValidMove()`. X ranges from 0 to `MAP_WIDTH - 1`; Y ranges from 0 to `MAP_HEIGHT - 1`.
3. **Give the Dragon Blindsight** *(extension)* — implement `MakeDragonMove()` so the dragon moves one cell toward the player per call. The game calls this twice per player move once awake.

The coordinate convention is **X = horizontal, Y = vertical**. The grid is allocated as `new char[MAP_WIDTH, MAP_HEIGHT]` and accessed as `Cavern[X, Y]`.

## Facilitator notes

- The solution is in `DragonInTheDark/ProgramComplete.cs`, which is **excluded from the project build**. Do not add it to compilation alongside `Program.cs` because both declare `Program` and `Position`.
- To run the completed solution, temporarily substitute its contents for `Program.cs` in a separate working copy, then restore the student version before distribution.
- `GameSetup.cs`, `GameDisplay.cs` and `GameSave.cs` contain supporting functionality and are shared by both versions.
- The first two challenges are the core of the session; the dragon AI is an extension for students who progress faster.
- The main menu deliberately hides Load and Save, although the supporting code remains present.

## Project layout

- `DragonInTheDark/Program.cs` — student starter
- `DragonInTheDark/ProgramComplete.cs` — completed reference
- `DragonInTheDark/GameSetup.cs` — map and item placement
- `DragonInTheDark/GameDisplay.cs` — console presentation
- `DragonInTheDark/GameSave.cs` — optional binary save/load

## Testing checklist

- Complete the student TODOs (or temporarily run the completed reference).
- Verify movement in all directions and rejection at all four map boundaries.
- Test the training map and multiple random new games.
- Step on a trap and check that the dragon wakes and moves twice.
- Find the Portal Stone and confirm the win message.
- Return to the main menu using M.

The project targets **.NET Framework 4.8**.

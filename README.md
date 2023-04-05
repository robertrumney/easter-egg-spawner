# EasterEggSpawner

This is a Unity script that spawns Easter eggs in a random location around the player object within a certain radius. The Easter egg prefabs are randomly selected from a list of available prefabs.

## Features

- Spawns Easter eggs in a random location around the player object within a certain radius.
- The number of Easter eggs spawned can be limited by setting the `maxEasterEggs` variable.
- The Easter egg prefabs are randomly selected from a list of available prefabs.

## How to Use

1. Attach the script to an empty GameObject in the Unity scene.
2. Set the Easter egg prefabs in the inspector by adding them to the "Easter Egg Prefabs" list.
3. Set the maximum number of Easter eggs to spawn by setting the "Max Easter Eggs" variable.
4. Run the scene.

## Dependencies

This script has no external dependencies but you might want to create an EasterEgg.cs class for your eggs.
One has been included as an example - adjust to fit your needs.

## License

This script is licensed under the [MIT License](LICENSE).

using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class EasterEggSpawner : MonoBehaviour
{
    private int numEasterEggs;
    public int maxEasterEggs = 500;
    
    private List<GameObject> spawnedEggs;
    public List<GameObject> easterEggPrefabs;

    // Delay to allow for any player repositioning
    private readonly float delayTime = 2f;

    // Call Start() as a coroutine on level start
    IEnumerator Start()
    {
        yield return new WaitForSeconds(delayTime);

        numEasterEggs = 0;
        spawnedEggs = new List<GameObject>();

        // Find the player object by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null)
        {
            Debug.LogError("No object tagged 'Player' found in the scene!");
            yield break;
        }

        // Find all terrains in the scene
        Terrain[] terrains = FindObjectsOfType<Terrain>();

        // Calculate the spawn area as a sphere around the player
        Vector3 spawnCenter = playerObj.transform.position;
        float spawnRadius = 1000f;

        // Spawn Easter eggs until the maximum number is reached
        while (numEasterEggs < maxEasterEggs)
        {
            // Generate a random position within the spawn area
            Vector3 spawnPos = spawnCenter + Random.insideUnitSphere * spawnRadius;

            // Calculate the height of the terrain at the spawn position
            float terrainHeight = 0f;
            foreach (Terrain terrain in terrains)
            {
                terrainHeight = Mathf.Max(terrain.SampleHeight(spawnPos), terrainHeight);
            }

            // Offset the y-coordinate of the spawn position to be above the terrain surface
            spawnPos.y = terrainHeight + 0.1f;

            // Spawn a random Easter egg prefab at the position
            GameObject easterEggPrefab = easterEggPrefabs[Random.Range(0, easterEggPrefabs.Count)];
            GameObject easterEgg = Instantiate(easterEggPrefab, spawnPos, Quaternion.identity);
            easterEgg.name = "Egg";

            // Make the egg a child of the egg hunt root container
            easterEgg.transform.SetParent(this.transform);

            // Add the spawned Easter egg to the list
            spawnedEggs.Add(easterEgg);
            
            // Increment total eggs spawned counter
            numEasterEggs++;
        }
    }
}

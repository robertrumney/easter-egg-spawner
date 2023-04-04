using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEggSpawner : MonoBehaviour
{
    public List<GameObject> easterEggPrefabs;
    public int maxEasterEggs;

    private int numEasterEggs;
    private List<GameObject> spawnedEggs;

    // Start is called before the first frame update
    void Start()
    {
        numEasterEggs = 0;
        spawnedEggs = new List<GameObject>();

        // Find the player object by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null)
        {
            Debug.LogError("No object tagged 'Player' found in the scene!");
            return;
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
            spawnPos.y = 0f;

            // Check if the spawn position is on the terrain
            bool onTerrain = false;
            foreach (Terrain terrain in terrains)
            {
                if (terrain.terrainData.bounds.Contains(spawnPos))
                {
                    onTerrain = true;
                    break;
                }
            }
            if (!onTerrain)
            {
                continue; // Skip this iteration of the while loop
            }

            // Spawn a random Easter egg prefab at the position
            GameObject easterEggPrefab = easterEggPrefabs[Random.Range(0, easterEggPrefabs.Count)];
            GameObject easterEgg = Instantiate(easterEggPrefab, spawnPos, Quaternion.identity);
            easterEgg.name = ":egg:";

            // Rotate the Easter egg to lie on the terrain surface
            RaycastHit hit;
            if (Physics.Raycast(easterEgg.transform.position + Vector3.up * 100f, Vector3.down, out hit, 200f, LayerMask.GetMask("Terrain")))
            {
                // Rotate the Easter egg to a jaunty angle
                Quaternion rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
                easterEgg.transform.rotation = rotation;

                // Set the Easter egg position to be exactly on the terrain
                easterEgg.transform.position = hit.point;
                easterEgg.transform.position += hit.normal * (easterEgg.transform.localScale.y * 0.5f);

                // Add the spawned Easter egg to the list
                spawnedEggs.Add(easterEgg);
            }

            numEasterEggs++;
        }
    }
}

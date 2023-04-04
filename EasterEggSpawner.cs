using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEggSpawner : MonoBehaviour
{
    public List<GameObject> easterEggPrefabs;
    public int maxEasterEggs;

    private int numEasterEggs;

    // Start is called before the first frame update
    void Start()
    {
        numEasterEggs = 0;

        // Find all terrains in the scene
        Terrain[] terrains = FindObjectsOfType<Terrain>();

        // Calculate the combined size of all the terrains
        Vector3 minPos = Vector3.one * float.MaxValue;
        Vector3 maxPos = Vector3.one * float.MinValue;
        foreach (Terrain terrain in terrains)
        {
            minPos = Vector3.Min(minPos, terrain.transform.position);
            maxPos = Vector3.Max(maxPos, terrain.transform.position + terrain.terrainData.size);
        }

        // Spawn Easter eggs until the maximum number is reached
        while (numEasterEggs < maxEasterEggs)
        {
            // Generate a random position within the combined terrain size
            Vector3 spawnPos = new Vector3(Random.Range(minPos.x, maxPos.x), 0f, Random.Range(minPos.z, maxPos.z));

            // Spawn a random Easter egg prefab at the position
            GameObject easterEggPrefab = easterEggPrefabs[Random.Range(0, easterEggPrefabs.Count)];
            GameObject easterEgg = Instantiate(easterEggPrefab, spawnPos, Quaternion.identity);
            easterEgg.name = ":egg:";

            numEasterEggs++;
        }
    }
}

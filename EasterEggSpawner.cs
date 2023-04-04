using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEggSpawner : MonoBehaviour
{
    public List<GameObject> easterEggPrefabs;
    public LayerMask raycastLayerMask;
    public int maxEasterEggs;
    public float spawnRadius;

    private int numEasterEggs;

    // Start is called before the first frame update
    void Start()
    {
        numEasterEggs = 0;

        // Spawn Easter eggs until the maximum number is reached
        while (numEasterEggs < maxEasterEggs)
        {
            // Generate a random position within the spawn radius
            Vector3 spawnPos = Random.insideUnitSphere * spawnRadius;
            spawnPos.y = 0;

            // Raycast to check if the position intersects with anything
            RaycastHit hit;
            if (Physics.Raycast(spawnPos + Vector3.up * 100, Vector3.down, out hit, 200, raycastLayerMask))
            {
                // Check if the intersection object is not an Easter egg
                if (!hit.collider.CompareTag("EasterEgg"))
                {
                    // Spawn a random Easter egg prefab at the position
                    GameObject easterEggPrefab = easterEggPrefabs[Random.Range(0, easterEggPrefabs.Count)];
                    GameObject easterEgg = Instantiate(easterEggPrefab, hit.point, Quaternion.identity);
                    easterEgg.tag = "EasterEgg";

                    numEasterEggs++;
                }
            }
        }
    }
}

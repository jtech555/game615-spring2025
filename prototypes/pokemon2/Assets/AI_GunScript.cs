using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_GunScript : MonoBehaviour
{
    public Transform enemy;           // Reference to the player's transform
    public GameObject objectToSpawn;   // The object to instantiate
    public Transform player;           // Reference to the player's transform
    public float spawnInterval = 2f;   // Time interval in seconds
    public float spawnDistance = 5f;   // Distance threshold

    private Coroutine spawnRoutine;

    void Update()
    {
        // Check distance between this object and the player
        if (Vector3.Distance(transform.position, player.position) <= spawnDistance)
        {
            // Start spawning if not already running
            if (spawnRoutine == null)
            {
                spawnRoutine = StartCoroutine(SpawnObject());
            }
        }
        else
        {
            // Stop spawning if the distance is greater
            if (spawnRoutine != null)
            {
                StopCoroutine(spawnRoutine);
                spawnRoutine = null;
            }
        }
    }

    IEnumerator SpawnObject()
    {
        while (true)
        {
            GameObject newObject = Instantiate(objectToSpawn, player.position, Quaternion.identity);
            newObject.transform.position = enemy.position; // Ensure the object spawns at the player's position
            newObject.GetComponent<Rigidbody>().linearVelocity = enemy.forward * 1f;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
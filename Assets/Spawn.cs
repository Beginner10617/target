using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject objectPrefabs; // Array of object prefabs to spawn
    public Transform[] spawnPoints;   // Array of spawn points
    public float initialSpawnInterval = 2f; // Initial time interval between spawns
    public float minimumSpawnInterval = 0.5f; // Minimum time interval (to limit difficulty)
    public float spawnIntervalDecrement = 0.05f; // Decrease in spawn interval per difficulty increment
    public float difficultyIncreaseInterval = 10f; // Time interval to increase difficulty
    public float objectSpeed = 5f; // Speed of the spawned objects

    private float currentSpawnInterval; // Current spawn interval
    private float timeSinceLastSpawn;   // Time since the last object was spawned
    private float difficultyTimer;      // Timer to track difficulty increments

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        timeSinceLastSpawn = 0f;
        difficultyTimer = 0f;
    }

    void Update()
    {
        // Increment timers
        timeSinceLastSpawn += Time.deltaTime;
        difficultyTimer += Time.deltaTime;

        // Spawn object if enough time has passed
        if (timeSinceLastSpawn >= currentSpawnInterval)
        {
            SpawnObject();
            timeSinceLastSpawn = 0f;
        }

        // Increase difficulty if the difficulty timer exceeds the threshold
        if (difficultyTimer >= difficultyIncreaseInterval)
        {
            difficultyTimer = 0f;
            currentSpawnInterval = Mathf.Max(currentSpawnInterval - spawnIntervalDecrement, minimumSpawnInterval);
        }
    }

    void SpawnObject()
    {
        // Select a random prefab and spawn point
        GameObject prefab = objectPrefabs;
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instantiate the object
        GameObject spawnedObject = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

        // Apply velocity to the object using its Rigidbody2D
        Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(-objectSpeed, 0); // Move towards the -x direction
        }
    }
}

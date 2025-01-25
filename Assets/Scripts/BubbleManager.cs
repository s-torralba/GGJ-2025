using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public GameObject bubblePrefab;
    public Vector2 spawnArea = new Vector2(8f, 5f);
    public float spawnInterval = 2f;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= spawnInterval)
        {
            SpawnBubble();
            _timer = 0f;
        }
    }

    void SpawnBubble()
    {
        // Spawn at random position within the area
        float x = Random.Range(-spawnArea.x / 2, spawnArea.x / 2);
        float y = Random.Range(-spawnArea.y / 2, spawnArea.y / 2);

        Vector3 spawnPosition = new Vector3(x + transform.position.x, y + transform.position.y, 0f);

        // Instantiate the bubble prefab
        Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Bubble spawned");
    }
}

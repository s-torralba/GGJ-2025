using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public GameObject bubblePrefab;

    public Vector2 spawnArea = new Vector2(8f, 5f);
    public float spawnInterval = 2f;
    public Vector2 scaleRange;

    private float _timer;
    private string letterPath;
    [SerializeField] private LetterCollector letterCollector;

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
        GameObject bubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity, transform);

        Debug.Log("Bubble spawned");
        
        // Scale it
        float randomScale = Random.Range(scaleRange.x, scaleRange.y);
        Vector3 randomScaleVector = new Vector3(randomScale, randomScale, randomScale);
        bubble.transform.localScale = randomScaleVector;

        bubble.GetComponent<Bubble>().ChangeLetter("B");
        bubble.GetComponent<Bubble>().AssignCollector(letterCollector);
    }


}

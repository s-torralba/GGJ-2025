using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Vector2 speed;
    public float frequency = 1f;

    private Rigidbody2D _rb;
    private float _time;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;

        float sinSpeedx = Mathf.Sin(_time * frequency) * speed.x;

        _rb.velocity = new Vector2(sinSpeedx, speed.y);
    }

    void OnMouseDown()
    {
        // Example: Play sound or animation here
        Debug.Log("Bubble popped!");

        // Destroy the bubble
        Destroy(gameObject);
    }

}

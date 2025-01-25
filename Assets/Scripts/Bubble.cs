using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Vector2 speed;
    public float frequency = 1f;
    private int directionX= 1;

    [SerializeField] private GameObject letter;
    public float maxLetterRotation;
    public float speedLetterRotation;

    private Rigidbody2D _rb;
    private float _time;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        directionX *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DestroyArea")
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;

        _RotateLetter();
        _MoveBubble();
    }

    void OnMouseDown()
    {
        // Example: Play sound or animation here
        Debug.Log("Bubble popped!");

        // Destroy the bubble
        Destroy(gameObject);
    }

    private void _RotateLetter()
    {
        if (letter != null)
        {
            float letterRotationY = Mathf.Sin(_time * speedLetterRotation) * maxLetterRotation;
            letter.transform.localRotation = Quaternion.Euler(0f, letterRotationY, 0f);
        }

    }

    private void _MoveBubble()
    {
        float sinSpeedx = Mathf.Sin(_time * frequency) * speed.x * directionX;
        _rb.velocity = new Vector2(sinSpeedx, speed.y);
    }
}

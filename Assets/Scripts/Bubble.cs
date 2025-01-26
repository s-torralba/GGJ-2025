using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bubble : MonoBehaviour
{
    public Vector2 speed;
    public float frequency = 1f;
    private int directionX= 1;

    [SerializeField] private GameObject letter;
    public float maxLetterRotation;
    public float speedLetterRotation;
    private string letterName;
    private LetterCollector letterCollector;

    private Rigidbody2D _rb;
    private float _time;

    private static string spritePath = "kenney_letter-tiles (1)/PNG/Wood/letter_";

    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject components;

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

        if (letterCollector != null)
        {
            letterCollector.CollectLetter(letterName);
        }

        audioSource.Play();
        components.SetActive(false);
        // Destroy the bubble
        Destroy(gameObject, audioSource.clip.length);
    }

    void OnDestroy()
    {
        
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

    public void ChangeLetter(string uppercaseLetter)
    {
        SpriteRenderer spriteRenderer = letter.GetComponent<SpriteRenderer>();
        letterName = uppercaseLetter;

        string computedPath = spritePath + uppercaseLetter;
        if (spriteRenderer != null)
        {
            // Load the new sprite from the Resources folder
            Sprite newSprite = Resources.Load<Sprite>(computedPath);

            if (newSprite != null)
            {
                // Assign the new sprite to the SpriteRenderer
                spriteRenderer.sprite = newSprite;
                Debug.Log($"Sprite successfully changed to: {computedPath}");
            }
            else
            {
                Debug.LogError($"Sprite not found at path: {computedPath}. Make sure the sprite is in a 'Resources' folder.");
            }
        }
        else
        {
            Debug.LogError("SpriteRenderer not found on the 'Letter' GameObject.");
        }
    }

    public void AssignCollector (LetterCollector collector)
    {
        letterCollector = collector;
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour
{
    public float thrust = 1.0f;
    public float rotSpeed = 1.0f;
    public float minPitch = 5f;
    public float maxPitch = 10f;
    public float pitchIncrement = 0.2f;
    public GameObject explosion;
    public GameObject smoke;
    public AudioClip explosionSound;
    public float timer = 10f;

    AudioSource audioSource;

    private float currentPitch;
    private Rigidbody2D rb2D;
    private Sprite mySprite;
    private Animator anim;
    private float timerFixed;

    void Awake()
    {
        currentPitch = minPitch;
        rb2D = gameObject.AddComponent<Rigidbody2D>();
        rb2D.constraints = RigidbodyConstraints2D.FreezePositionX;
    }

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        //Fetch the AudioSource from the GameObject
        audioSource = GetComponent<AudioSource>();
        explosionSound = GetComponent<AudioClip>();

        timerFixed = timer;
    }

    void Update()
    {
        if (timer <= 0)
        {
            timer = timerFixed;
        }

        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton0))
        {
            rb2D.AddRelativeForce(transform.up * thrust);
            anim.speed = 2.5f;
            if (rb2D.position.y > 3.5f)
            {
                rb2D.MoveRotation(Mathf.LerpAngle(rb2D.rotation, 0, rotSpeed * Time.deltaTime));
            }
            else
            {
                rb2D.MoveRotation(Mathf.LerpAngle(rb2D.rotation, 15, rotSpeed * Time.deltaTime));
            }

            currentPitch += pitchIncrement;
            if (currentPitch >= maxPitch)
            {
                currentPitch = maxPitch;
            }
        }
        else
        {
            anim.speed = 1.5f;
            if (rb2D.position.y < -3.5f)
            {
                rb2D.MoveRotation(Mathf.LerpAngle(rb2D.rotation, 0, rotSpeed * Time.deltaTime));
            }
            else
            {
                rb2D.MoveRotation(Mathf.LerpAngle(rb2D.rotation, -15, rotSpeed * Time.deltaTime));
            }

            currentPitch -= pitchIncrement;
            if (currentPitch <= minPitch)
            {
                currentPitch = minPitch;
            }
        }

        while (timer == timerFixed)
        {
            Instantiate(smoke, new Vector3(transform.position.x - 2f, transform.position.y + 0.4f, transform.position.z - 1), transform.rotation);
            timer -= Time.deltaTime;
        }
        timer -= Time.deltaTime;

        audioSource.pitch = currentPitch;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ground")
        {
            Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z-1), transform.rotation);
            GetComponent<AudioSource>().Play();
            Destroy(transform.gameObject);
        }
    }
}

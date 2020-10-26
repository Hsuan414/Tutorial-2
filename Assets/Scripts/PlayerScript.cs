using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text winText;
    public Text livesText;
    private int scoreValue = 0;
    private int lives;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        SetScore();
        lives = 3;
        SetLivesText();
        winText.text ="";
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            SetScore();
            Destroy(collision.collider.gameObject);
        }
        else if(collision.collider.tag == "Enemy")
        {
            lives = lives - 1;
            SetLivesText();
            Destroy(collision.collider.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void SetScore()
    {
        if (scoreValue == 4)
        {
            transform.position = new Vector3(30.0f, 0.0f, 0.0f);
            lives = 3;
            SetLivesText();
        }
        if(scoreValue >= 8)
        {
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = false;
            Destroy(this);
            winText.text = "You win! Game by Anita Kuo.";
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if(lives == 0)
        {
            Destroy(this);
            winText.text = "You lost!";
        }
    }
}

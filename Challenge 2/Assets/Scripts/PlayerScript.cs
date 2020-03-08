using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    Animator anim;

    private bool facingRight = true;   
    
    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;
    
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    public Text lives;

    public Text winText;

    private int scoreValue = 0;

    private int livesValue = 3;

    // Start is called before the first frame update
    void Start()
    {
	anim = GetComponent<Animator>();
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
	lives.text = livesValue.ToString();
	winText.text = "";




    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
	if (Input.GetKeyDown(KeyCode.D))
	   {
		anim.SetInteger("State", 1);
		musicSource.clip = musicClipOne;
                musicSource.Play();
	   }
	if (Input.GetKeyUp(KeyCode.D))
          {
               anim.SetInteger("State", 0);
		musicSource.Stop();
          }
	if (Input.GetKeyDown(KeyCode.A))
	  {
		anim.SetInteger("State", 1);
		musicSource.clip = musicClipOne;
                musicSource.Play();
	  }
	if (Input.GetKeyUp(KeyCode.A))
          {
               anim.SetInteger("State", 0);
		musicSource.Stop();
          }

	if (facingRight == false && hozMovement > 0)
	  {
	    Flip();
	  }
	else if (facingRight == true && hozMovement <0)
	  {
	    Flip();
	  }


	
	
	

    }

     private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
	if (scoreValue == 4)
	{
	winText.text = "You Win! Game created by Alec Nicholson";
	musicSource.clip = musicClipOne;	
	musicSource.Stop();
	musicSource.clip = musicClipTwo;
	musicSource.Play();
	}


	


	 	
	if (collision.collider.tag == "Enemy") 
	{ 
		livesValue -= 1; 
		lives.text = livesValue.ToString(); 
		Destroy(collision.collider.gameObject);
	}
	if (livesValue == 0)
	{
	winText.text = "You Lose!";
	Destroy(gameObject);
	}


    }

    void Flip()
	{
		facingRight = !facingRight;
		Vector2 Scaler = transform.localScale;
		Scaler.x = Scaler.x* -1;
		transform.localScale = Scaler;
	}

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
		anim.SetInteger("State", 2);
	
            }
        }
      }
    }
  

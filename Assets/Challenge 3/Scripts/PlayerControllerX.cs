using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    private float gravityModifier = 1.9f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip bounceSound;

    private Animator ballonAnimator; 

    public bool isBallonLowEnough;


    // Start is called before the first frame update
    void Start()
    {   playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        ballonAnimator = GetComponent<Animator>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 1, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (transform.position.y < 14)
        {
            isBallonLowEnough = false;
        }  else { isBallonLowEnough =true;}

        if (Input.GetKey(KeyCode.Space) && !gameOver  )
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
        }

        /*if(transform.position.y < 17){ 
            isBallonLowEnough = false;
        }
        else{ 
            isBallonLowEnough = true; 
        }*/
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }

        else if (other.gameObject.CompareTag("Ground")){ 

            playerRb.AddForce(Vector3.up * 20, ForceMode.Impulse);
            playerAudio.PlayOneShot(bounceSound, 1.9f);
        }

        
    }

}

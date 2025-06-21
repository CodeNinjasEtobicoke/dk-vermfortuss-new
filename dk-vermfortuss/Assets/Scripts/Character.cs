using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public float Speed = 5f;
    public float RotationSpeed = 20f;
    Animator anim;

    public Vector3 Drag;

    public bool running = false;
    public bool canMove = true;

    public AudioClip footSteps;

    private bool isGameOver;
    private bool isPlayerHit;
    private CharacterController _controller;
    private Vector3 _velocity;

    private AudioSource playerAudio;

    void Start()
    {
        isGameOver = Laser.playerDeath;
        isPlayerHit = GameOver.playerTouchedLaser;
        _controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    void Update()

    {
        if (canMove)
        {

            Vector3 move = transform.forward * Input.GetAxis("Vertical") * Speed;

            _controller.Move(move * Time.deltaTime);
            transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * Time.deltaTime * RotationSpeed, 0));
            if (move != Vector3.zero)
            {
                //transform.forward = move;
                running = true;
            }
            else
            {
                running = false;
            }

            _velocity.x /= 1 + Drag.x * Time.deltaTime;

            _velocity.y = 0f;

            _velocity.z /= 1 + Drag.z * Time.deltaTime;
            


            _controller.Move(_velocity * Time.deltaTime);

            anim.SetBool("isRunning", running);

            if(!playerAudio.isPlaying && running)
            {
                playerAudio.clip = footSteps;
                playerAudio.Play();
            } else if (!running)
            {
                playerAudio.Stop();
            }
        }
    }
}

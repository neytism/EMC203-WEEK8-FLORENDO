using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public Transform[] lanes;
    public Animator _anim;

    private int currentLane = 1; // Start in the middle lane

    public int CurrentLane => currentLane;

    private bool isJumping = false;
    private bool isDucking = false;
    private Vector3 targetPosition;

    private static bool _isDead = false;
    public static bool IsDead => _isDead;

    private State _state;

    public State GetState => _state;

    private void OnEnable()
    {
        Obstacle.Hit += Death;
    }

    private void OnDisable()
    {
        Obstacle.Hit -= Death;
    }

    public enum State
    {
        Running,
        Jumping,
        Ducking,
        Dead
    }
    
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        _state = State.Running;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_isDead) return;
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.3f);
        
        // Move left
        if (Input.GetKeyDown(KeyCode.A) && currentLane > 0)
        {
            currentLane--;
            targetPosition = lanes[currentLane].position;
        }

        // Move right
        if (Input.GetKeyDown(KeyCode.D) && currentLane < lanes.Length - 1)
        {
            currentLane++;
            targetPosition = lanes[currentLane].position;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && !isDucking)
        {
            isJumping = true;
            _anim.SetBool("IsJumping",isJumping);
            _state = State.Jumping;
        }

        // Duck
        if (Input.GetKeyDown(KeyCode.S) && !isJumping && !isDucking)
        {
            isDucking = true;
            _anim.SetBool("IsDucking",isDucking);
            _state = State.Ducking;
        }

        // Return to normal position after jumping or ducking
        if (Input.GetKeyUp(KeyCode.Space) && isJumping)
        {
            _anim.SetBool("IsJumping",false);

            StartCoroutine(JumpDelay());
        }
        
        if (Input.GetKeyUp(KeyCode.S) && isDucking)
        {
            isDucking = false;
            _anim.SetBool("IsDucking",isDucking);
            _state = State.Running;
        }
    }

    private void Death()
    {
        _anim.SetTrigger("Death");
        StartCoroutine(DeathDelay());

    }
    

    IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(1f);
        isJumping = false;
        _state = State.Running;
    }
    
    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(1f);
        _isDead = true;
        Time.timeScale = 0f;
        //game over here
    }
}

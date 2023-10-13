using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public Transform[] lanes;

    private int currentLane = 1; // Start in the middle lane
    private bool isJumping = false;
    private bool isDucking = false;
    private Vector3 targetPosition;

    private State _state;
    
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
       Debug.Log(_state);
       Move();
    }

    private void Move()
    {
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
            _state = State.Jumping;
        }

        // Duck
        if (Input.GetKeyDown(KeyCode.S) && !isJumping && !isDucking)
        {
            isDucking = true;
            _state = State.Ducking;
        }

        // Return to normal position after jumping or ducking
        if (Input.GetKeyUp(KeyCode.Space) && isJumping)
        {
            isJumping = false;
            _state = State.Running;
        }
        
        if (Input.GetKeyUp(KeyCode.S) && isDucking)
        {
            isDucking = false;
            _state = State.Running;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static event Action Hit;
    public float threshold = 0.001f;
    private Player _player;
    private Item _item;
    private float _playerZValue;
    [SerializeField] private ObstacleType _obstacleType;
    
    private enum ObstacleType
    {
        Barricade,
        Rock
    }
    
    
    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _item = GetComponent<Item>();
        _playerZValue = _player.transform.position.z;
    }

    private void Update()
    {
        if (_item.lanePosition != _player.CurrentLane) return;
        
        if (Mathf.Abs(_playerZValue - _item.transform.position.z) <= threshold)
        {
            if (_obstacleType == ObstacleType.Barricade)
            {
                if (_player.GetState != Player.State.Ducking)
                {
                    Hit?.Invoke();
                }
            }
            
            if (_obstacleType == ObstacleType.Rock)
            {
                if (_player.GetState != Player.State.Jumping)
                {
                    Hit?.Invoke();
                }
            }
            
           
        }
    }
}

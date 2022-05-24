using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControle : MonoBehaviour
{
    private Gamecontrole _gamecontrole;
    private PlayerInput _playerInput;
    private Camera _mainCamera;
    private Vector2 _moveInput;
    private Rigidbody _rigidbody;
    public float moveMultiplier;


    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _gamecontrole = new Gamecontrole();

        _playerInput = GetComponent<PlayerInput>();

        _mainCamera = Camera.main;

        _playerInput.onActionTriggered += onActionTriggered;
    }

    private void OnDisable()
    {
        _playerInput.onActionTriggered -= onActionTriggered;
    }

    private void onActionTriggered(InputAction.CallbackContext obj)
    {
        if (obj.action.name.CompareTo(_gamecontrole.Gameplay.Movement.name) != 0)
        {
            _moveInput = obj.ReadValue<Vector2>();
        }
    }

    private void Move()
    {
        _rigidbody.AddForce((_mainCamera.transform.forward * _moveInput.y +
                             _mainCamera.transform.right * _moveInput.x) *
                            moveMultiplier * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        Move();
    }

}


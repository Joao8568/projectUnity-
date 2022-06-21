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

    public float maxVelocity;
    public float rayDistance;
    public LayerMask layerMask;
    private bool _isGrounder;
    public float jumpForce;
    
    
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
        if (obj.action.name.CompareTo(_gamecontrole.Gameplay.Movement.name) == 0)
        {
            _moveInput = obj.ReadValue<Vector2>();
        }

        if (obj.action.name.CompareTo(_gamecontrole.Gameplay.Jump.name) == 0)
        {
            if(obj.performed) Jump();
        }
    }

    private void Move()
    {   
        Vector3 camForward = _mainCamera.transform.forward;
        Vector3 camRight = _mainCamera.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        
        _rigidbody.AddForce((_mainCamera.transform.forward * _moveInput.y +
                             _mainCamera.transform.right * _moveInput.x) *
                            moveMultiplier * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        Move();
        LimiteVelocity();
    }
    //Fução que vai limitar a velocidade máxima do jogador
    private void LimiteVelocity()
    {
        Vector3 velocity = _rigidbody.velocity;
        
        //compara velocidade no eixo x (usando a função Abs para ignorar a sinal negativo caso tenha)
        //utiliza a função sign para recuperar o valor do sinal da velocidade 
        if (Mathf.Abs(velocity.x) > maxVelocity) velocity.x = Mathf.Sign(velocity.x) * maxVelocity;

        velocity.z = Mathf.Clamp(value: velocity.z, min: -maxVelocity, maxVelocity);
        
        //Atribui o vetor que alteramos de volta ao rigid bady
        _rigidbody.velocity = velocity;
    }
    
    /*o que é preciso para pular?
     * - jogaodr estar no chão
     * 1 -- colisão rigidbody do jogador com o chão e, se a colisão estiver acontecendo, faz uma váriavel
     * 2 -- que checa se o jogador esta no chão ficar verdadeira
     * 3 -- usar o raycast = () ---
     * 4 -- Atira um raio em alguma direção ( no nosso caso, vai ser sempre para baixo)
     * 5 -- Caso atinja algum objeto, ela retorna uma colisão que podemos usar para fazer a variavel que checa se
     *      jogado está no chão ficar verdadeira, caso o objeto colidido seja um objeto que represente o chão
     * 6 -- Podemos usar o LayerMask para somente verificar colisões com certos tipos de objetos
     * - jogador precisa apertar o botão para pular
     *  --> usamos a função OnAcctionTrigered e comparamos se o nome da ação tem o memso nome da ação de pulo
     *  -- caso tenha, checamos se o botão foi apertado (started), foi solto (canceled) ou foi pressionado e solto (performed) 
      */

    private void CheckGround()
    {
        RaycastHit collision;
        if (Physics.Raycast(transform.position, Vector3.down, out collision, rayDistance, layerMask))
        {
            _isGrounder = true;
        }
        else
        {
            _isGrounder = false;
        }
    }
    
    private void Jump()
    {
        if (_isGrounder)
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        CheckGround();
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(start: transform.position, dir: Vector3.down * rayDistance, Color.yellow);
    }
}


using System;
using NF.Main.Core;
using NF.Main.Core.PlayerStateMachine;
using Sirenix.OdinInspector;
using Unity.Cinemachine;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


namespace NF.Main.Gameplay.PlayerInput
{
    public class PlayerController : MonoExt
    {
        [TabGroup("References")][SerializeField] private PlayerInputReader _playerInput;
        [TabGroup("References")][SerializeField] private Animator _animator;

        
        [SerializeField] private GameManager _gameManager;
        private StateMachine _stateMachine;
        [SerializeField] public PlayerState PlayerState { get; set; }
        
        //player and camera rotation variables
        [SerializeField] private CinemachineCamera _playerCamera;
        private Vector2 _lookDirection;
        public float _cameraXSensitivity;
        public float _cameraYSensitivity;

        //player movement variables
        private Vector2 _moveDirection;
        public PlayerMovement _playerMovement;
        public PlayerMovementStrafeJumping _playerMovementStrafe;

        //Reset Event
        public delegate void OnResetPlayer();
        public event OnResetPlayer _onResetPlayer;
       
        private void Start()
        {
            Initialize();
            OnSubscriptionSet();
        }

        private void Awake()
        {
            SetupStateMachine();
        }

        //Call the current states update method
        private void Update()
        {
            _stateMachine.Update();
        }

        //Call the current states fixed update method
        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }

        //Initialize needed data
        public override void Initialize(object data = null)
        {
            base.Initialize(data);
            _playerInput.EnablePlayerActions();
        }

        // Set up all the events
        public override void OnSubscriptionSet()
        {
            base.OnSubscriptionSet();
            AddEvent(_playerInput.Attack, _ => OnAttack());
            AddEvent(_playerInput.Jump, _ => OnJump());
            AddEvent(_playerInput.Dash, _ => OnDash());
            AddEvent(_playerInput.Reset, _ => OnReset());
            AddEvent(_playerInput.Movement, OnPlayerMove);
            AddEvent(_playerInput.Look, OnLook);
        }


        //Sets up animation states and transitions
        private void SetupStateMachine()
        {
            // State Machine
            _stateMachine = new StateMachine();
            
            // Declare Player States
            var idleState = new PlayerIdleState(this, _animator);
            var movingState = new PlayerMovingState(this, _animator);
            var deathState = new PlayerDeathState(this, _animator);
            
            // Define Player State Transitions
            At(idleState, movingState, new FuncPredicate(() => PlayerState == PlayerState.Moving));
            At(idleState, deathState, new FuncPredicate(() => PlayerState == PlayerState.Death));
            At(movingState, deathState, new FuncPredicate(() => PlayerState == PlayerState.Death));
            Any(idleState, new FuncPredicate(ReturnToIdleState));
            
            // Set Initial State
            _stateMachine.SetState(idleState);
        }
        
        private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
        private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
        
        //Method that handles the condition if the player should return to idle state
        private bool ReturnToIdleState()
        {
            return PlayerState == PlayerState.Idle;
        }
        
        //Method that handles logic when the attack button is pressed
        private void OnAttack()
        {
            Debug.Log($"Attack Performed");
        }
        
        //Player movement logic is handled here
        private void OnPlayerMove(Vector2 movementDirection)
        {
            if(PlayerState != PlayerState.Death)
            {
                _moveDirection = movementDirection;
                if(_moveDirection != Vector2.zero)
                {
                    PlayerState = PlayerState.Moving;
                }
                else
                {
                    PlayerState = PlayerState.Idle;
                }
            }
        }

        //player jump logic
        private void OnJump()
        {
            if(PlayerState == PlayerState.Death)
            {
                return;
            }

            PlayerState = PlayerState.Moving;
            _playerMovement.Jump();
        }

        // player dash logic
        private void OnDash()
        {
            if(PlayerState == PlayerState.Death)
            {
                return;
            }

            if(_moveDirection == Vector2.up)
            {
                _playerMovement.Dash(_playerCamera.transform.forward);
            }
            else if (_moveDirection == Vector2.zero)
            {
                _playerMovement.Dash(_playerCamera.transform.forward);
            }
            else
            {
                _playerMovement.Dash(new Vector3(_moveDirection.x, 0, _moveDirection.y));
            }
        }

        private void OnReset()
        {
            _playerMovement._rb.linearVelocity = Vector3.zero;
            _onResetPlayer.Invoke();
        }

        // player camera look direction change
        private void OnLook(Vector2 LookDirection)
        {
            //Debug.Log($"Player Movement: {LookDirection.normalized}");
            _lookDirection.x = LookDirection.normalized.x * Time.deltaTime * _cameraXSensitivity;
            _lookDirection.y = LookDirection.normalized.y * Time.deltaTime * _cameraYSensitivity;
            _playerMovement.Turn(_lookDirection);
        }

        //get movement for moving
        public Vector2 GetMovementDirection()
        {
            return _moveDirection;
        }
    }
}
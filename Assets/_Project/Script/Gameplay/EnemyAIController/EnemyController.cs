using NF.Main.Core;
using NF.Main.Core.EnemyStateMachine;
using NF.Main.Gameplay;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyController : MonoExt
{
    [TabGroup("References")][SerializeField] private Animator _animator;

    [SerializeField] private GameManager _gameManager;
    private StateMachine _stateMachine;
    [SerializeField] public EnemyState EnemyState { get; set; }

    public EnemyMovement _enemyMovement;

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

        public override void Initialize(object data = null)
        {
            base.Initialize(data);
        }

        public override void OnSubscriptionSet()
        {
            // base.OnSubscriptionSet();
            // AddEvent(_playerInput.Attack, _ => OnAttack());
            // AddEvent(_playerInput.Jump, _ => OnJump());
            // AddEvent(_playerInput.Dash, _ => OnDash());
            // //AddEvent(_playerInput.ScrollWheel, _ => OnJump());
            // AddEvent(_playerInput.Movement, OnPlayerMove);
            // AddEvent(_playerInput.Look, OnLook);
        }

        private void SetupStateMachine()
        {
            // State Machine
            _stateMachine = new StateMachine();
            
            // Declare Player States
            var patrolState = new EnemyPatrolState(this, _animator);
            
            // Define Player State Transitions
            Any(patrolState, new FuncPredicate(ReturnToPatrolState));
            
            // Set Initial State
            _stateMachine.SetState(patrolState);
        }

        private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
        private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
        
        //Method that handles the condition if the player should return to idle state
        private bool ReturnToPatrolState()
        {
            return EnemyState == EnemyState.Patrol;
        }
}

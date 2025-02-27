using NF.Main.Core;
using NF.Main.Core.GameStateMachine;
using UnityEngine.Events;


namespace NF.Main.Gameplay
{
    public class GameManager : SingletonPersistent<GameManager>
    {
        public GameState _gameState;
        public UnityEvent _onWin;
        public UnityEvent _onDeath;
        public UnityEvent _onReset;
        private StateMachine _stateMachine;

        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        public override void Initialize(object data = null)
        {
            base.Initialize(data);
            _gameState = GameState.Playing;
            SetupStateMachine();
        }

        private void SetupStateMachine()
        {
            // State Machine
            _stateMachine = new StateMachine();

            // Declare states
            var pausedState = new GamePausedState(this, GameState.Paused);
            var playingState = new GamePlayingState(this, GameState.Playing);
            var gameOverState = new GameOverState(this, GameState.GameOver);
            var gameWinState = new GameWinState(this, GameState.GameOver);


            // Define transitions
            At(playingState, pausedState, new FuncPredicate(() => _gameState == GameState.Paused));
            At(playingState, gameOverState, new FuncPredicate(() => _gameState == GameState.GameOver));
            At(playingState, gameWinState, new FuncPredicate(() => _gameState == GameState.GameWin));
            
            Any(playingState, new FuncPredicate(() => _gameState == GameState.Playing));

            // Set initial state
            _stateMachine.SetState(playingState);
        }

        private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
        private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
    }
}
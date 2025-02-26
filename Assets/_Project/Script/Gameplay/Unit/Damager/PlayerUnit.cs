using NF.Main.Core.PlayerStateMachine;
using NF.Main.Gameplay;
using NF.Main.Gameplay.PlayerInput;
using UnityEngine;

public class PlayerUnit : BaseUnit
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Vector3 _startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startPosition = transform.position;
        _playerController._onResetPlayer += ResetPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnDeath()
    {
        base.OnDeath();
        _playerController.PlayerState = PlayerState.Death;
    }

    public void ResetPlayer()
    {
        transform.SetPositionAndRotation(_startPosition, Quaternion.identity);
        _currentHp = _maxHp;
        _playerController.PlayerState = PlayerState.Idle;
        GameManager.Instance.GameState = GameState.Playing;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Enemy")
        {
            OnCollideEnemy(other.collider);
        }
    }

    private void OnCollideEnemy(Collider other)
    {
        bool isDashing = _playerController._playerMovement._isDashing;

        if(isDashing)
        {
            DamageUnit(other.GetComponent<EnemyBaseUnit>());
        }
    } 
}

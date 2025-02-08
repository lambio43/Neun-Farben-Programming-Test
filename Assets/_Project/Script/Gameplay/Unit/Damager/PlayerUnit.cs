using NF.Main.Core.PlayerStateMachine;
using NF.Main.Gameplay.PlayerInput;
using UnityEngine;

public class PlayerUnit : BaseUnit
{
    [SerializeField] private PlayerController _playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}

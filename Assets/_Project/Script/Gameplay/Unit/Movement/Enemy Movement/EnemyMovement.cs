using NF.Main.Core;
using Unity.Mathematics;
using UnityEngine;
using UniRx;

public class EnemyMovement : BaseMovement
{
    public Transform[] _enemyAIPath;
    private int _maxAIPathCount;
    [SerializeField]private int _currentAIPathIndex = 0;

    //use unirx for changes to AIPathIndex so as to change to idle state when it change
    public Subject<int> AIPathIndex;

    private void OnEnable()
    {
        AIPathIndex = new Subject<int>();
    }

    void Start()
    {
        _maxAIPathCount = _enemyAIPath.Length;
        Debug.Log(_maxAIPathCount);
    }

    public override void Move(Vector2 movementDireciton)
    {
        base.Move(movementDireciton);

        transform.position = Vector3.MoveTowards(transform.position, _enemyAIPath[_currentAIPathIndex].position, _moveSpeed * Time.deltaTime);
        CheckIfEnemyOnLocation();
    }

    public override void Turn(Vector2 lookDirection)
    {
        Vector3 direction = _enemyAIPath[_currentAIPathIndex].position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 5f * Time.deltaTime);
    }

    public bool IsFacingTarget()
    {
        Vector3 direction = _enemyAIPath[_currentAIPathIndex].position - transform.position;
        float dotprod = Vector3.Dot(direction.normalized, transform.forward);
        if(dotprod == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ChangePathIndex()
    {
        _currentAIPathIndex++;

        if(_currentAIPathIndex >= _maxAIPathCount)
        {
            _currentAIPathIndex = 0;
        }
        AIPathIndex.OnNext(_currentAIPathIndex);
    }

    private void CheckIfEnemyOnLocation()
    {
        if(transform.position == _enemyAIPath[_currentAIPathIndex].position)
        {
            ChangePathIndex();
        }
    }
}

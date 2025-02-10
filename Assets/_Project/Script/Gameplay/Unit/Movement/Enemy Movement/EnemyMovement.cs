using NF.Main.Core;
using UnityEngine;

public class EnemyMovement : BaseMovement
{
    public Transform[] _enemyAIPath;
    private int _maxAIPathCount;
    private int _currentAIPathIndex = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _maxAIPathCount = _enemyAIPath.Length;
        Debug.Log(_maxAIPathCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Move(Vector2 movementDireciton)
    {
        base.Move(movementDireciton);

        //Ai movement

        transform.position = Vector3.MoveTowards(transform.position, _enemyAIPath[_currentAIPathIndex].position, _moveSpeed * Time.deltaTime);
        CheckIfEnemyOnLocation();
    }

    private void ChangePathIndex()
    {
        _currentAIPathIndex++;

        if(_currentAIPathIndex >= _maxAIPathCount)
        {
            _currentAIPathIndex = 0;
        }
    }

    private void CheckIfEnemyOnLocation()
    {
        if(transform.position == _enemyAIPath[_currentAIPathIndex].position)
        {
            ChangePathIndex();
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SpeedPickup : BasePickup
{
    public UnityEvent OnPickUp;
    public float _buffDuration;
    public float _moveSpeedMultiplier;
    private float _playerOriginalMoveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ActivatePickupEffect(other.GetComponentInParent<BaseUnit>());
        }
    }

    public override void ActivatePickupEffect(BaseUnit baseUnit)
    {
        base.ActivatePickupEffect(baseUnit);
        BaseMovement baseMovement = baseUnit.GetComponent<BaseMovement>();
        _playerOriginalMoveSpeed = baseMovement._moveSpeed;
        baseMovement.ChangeMoveSpeed(_playerOriginalMoveSpeed * _moveSpeedMultiplier);
        Debug.Log(_playerOriginalMoveSpeed);
        OnPickUp.Invoke();
        StartCoroutine(CO_RevertMoveSpeed(baseMovement));
    }

    private IEnumerator CO_RevertMoveSpeed(BaseMovement baseMovement)
    {
        yield return new WaitForSeconds(_buffDuration);
        baseMovement.ChangeMoveSpeed(_playerOriginalMoveSpeed);
        Destroy(this.gameObject);
    }
}

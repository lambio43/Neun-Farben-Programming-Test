using NF.Main.Core;
using NF.Main.Gameplay;
using UnityEngine;
using UniRx;

public class BaseUnit : MonoExt, IDamager, IHealth
{
    public float _maxHp;
    protected float _currentHp;
    public float _dmg;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }   

    public virtual void DamageUnit(BaseUnit unitToDamage)
    {
        unitToDamage.ReduceHealth(_dmg);
    }

    public virtual void ReduceHealth(float healthToReduce)
    {
        _currentHp -= healthToReduce;

        if(_currentHp <= 0)
        {
            OnDeath();
        }
    }

    public virtual void OnDeath()
    {
        
    }
}

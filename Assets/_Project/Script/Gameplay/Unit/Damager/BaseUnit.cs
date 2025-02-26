using NF.Main.Core;
using NF.Main.Gameplay;
using UnityEngine;
using UniRx;

public class BaseUnit : MonoExt, IDamager, IHealth
{
    public float _maxHp;
    protected float _currentHp;
    public float _dmg;
    
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

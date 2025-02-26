using UnityEngine;

public class SpikeTrap : BaseTrap
{
    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Player")
        {
            EffectTrapActivate(other.collider.GetComponentInParent<BaseUnit>());
        }
    }

    public override void EffectTrapActivate(BaseUnit unitToAffect)
    {
        base.EffectTrapActivate(unitToAffect);
        DamageUnit(unitToAffect);
    }
}

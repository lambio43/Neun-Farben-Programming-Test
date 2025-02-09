using UnityEngine;

public class SpikeTrap : BaseTrap
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

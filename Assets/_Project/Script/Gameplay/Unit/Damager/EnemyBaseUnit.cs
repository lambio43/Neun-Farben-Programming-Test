using Unity.VisualScripting;
using UnityEngine;

public class EnemyBaseUnit : BaseUnit
{
    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Player")
        {
            OnCollidePlayer(other);
        }
    }

    private void OnCollidePlayer(Collision other)
    {
        PlayerMovement playerMovement = other.collider.GetComponentInParent<PlayerMovement>();

        if(playerMovement._isDashing == true)
        {
            return;
        }
        else
        {
            DamageUnit(other.collider.GetComponentInParent<BaseUnit>());
        }
    }

    public override void OnDeath()
    {
        base.OnDeath();
        Destroy(this.gameObject);
    }
}

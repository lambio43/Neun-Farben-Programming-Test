using Unity.VisualScripting;
using UnityEngine;

public class EnemyBaseUnit : BaseUnit
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

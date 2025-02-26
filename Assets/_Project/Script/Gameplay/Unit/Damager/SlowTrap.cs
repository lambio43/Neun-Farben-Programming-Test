using UnityEngine;

public class SlowTrap : BaseTrap
{
    public float _dragValue;
    public float _maxSpeedOnTrap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            EffectTrapActivate(collision.collider.GetComponentInParent<BaseUnit>());
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            collision.collider.GetComponentInParent<BaseMovement>().SpeedControl();
            collision.collider.GetComponentInParent<BaseMovement>().RevertDragValue();
        }
        
    }

    public override void EffectTrapActivate(BaseUnit unitToAffect)
    {
        base.EffectTrapActivate(unitToAffect);
        BaseMovement movement = unitToAffect.GetComponent<BaseMovement>();

        movement.ChangeDrag(_dragValue);
        movement.ChangeMaxSpeed(_maxSpeedOnTrap);
        Debug.Log("Works");
    }
}

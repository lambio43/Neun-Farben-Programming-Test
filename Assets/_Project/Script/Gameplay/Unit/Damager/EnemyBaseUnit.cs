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
            DamageUnit(other.collider.GetComponentInParent<BaseUnit>());
        }
    }
}

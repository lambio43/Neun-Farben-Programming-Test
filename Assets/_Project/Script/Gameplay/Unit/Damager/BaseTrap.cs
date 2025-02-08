using UnityEngine;

public class BaseTrap : BaseUnit
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
        if(other.collider.CompareTag("Player"))
        {
            DamageUnit(other.collider.GetComponentInParent<BaseUnit>());
        }
    }
}

using UnityEngine;
using NF.Main.Core;
using UnityEngine.Events;
using NF.Main.Gameplay;

public class FinishLine : MonoExt
{
    //public UnityEvent OnCrossFinishLine;
    private bool isCrossFinishLine = false;

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
        if(other.CompareTag("Player") && isCrossFinishLine == false)
        {
            //other.GetComponentInParent<BaseUnit>()
           GameManager.Instance.GameState = GameState.GameWin;
        }
    }
}

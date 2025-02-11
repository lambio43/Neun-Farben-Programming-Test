using UnityEngine;
using NF.Main.Core;
using UnityEngine.Events;
using NF.Main.Gameplay;

public class FinishLine : MonoExt
{
    private bool isCrossFinishLine = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && isCrossFinishLine == false)
        {
            //other.GetComponentInParent<BaseUnit>()
           GameManager.Instance.GameState = GameState.GameWin;
        }
    }
}

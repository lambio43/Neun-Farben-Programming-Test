using NF.Main.Core;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class UIManager : SingletonPersistent<UIManager>
{
    public GameObject _winMessage;
    public GameObject _loseMessage;
    
    public TMP_Text _dashCoolDownText;

    public void UpdateDashCoolDownText(float currentCooldown)
    {
        Debug.Log("Fired");
        if(currentCooldown <= 0)
        {
            _dashCoolDownText.text = "Ready";
        }
        else
        {
            _dashCoolDownText.text = currentCooldown.ToString("F");
        }
    }
}

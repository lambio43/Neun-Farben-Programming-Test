using Unity.VisualScripting;
using UnityEngine;

public interface IMove
{
    void Move(Vector2 moveDireciton);
    void Dash(Vector3 dashDireciton);
    void Turn(Vector2 lookDirection);
    void ResetDash();
    void DashCooldown();
}

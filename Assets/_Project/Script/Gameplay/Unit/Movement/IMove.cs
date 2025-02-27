using UnityEngine;

public interface IMove
{
    void Move(Vector2 moveDireciton);
    void Dash(Vector3 dashDireciton);
    void ResetDash();
    void DashCooldown();
}

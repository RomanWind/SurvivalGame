using UnityEngine;

public abstract class GameBaseState
{
    public virtual void EnterState(GameStateManager gameState) {}

    public virtual void UpdateState(GameStateManager gameState) {}
}

using UnityEngine;

public class GameWaitState : GameBaseState
{
    public override void EnterState(GameStateManager gameState) 
    {
        Debug.Log("Wait State");
    }

    public override void UpdateState(GameStateManager gameState) 
    {

    }
}

using UnityEngine;

public class GameFightState : GameBaseState
{
    public override void EnterState(GameStateManager gameState)
    {
        Debug.Log("Entering Fight State");
        GameManager.instance.FightStateInitiated();
    }

    public override void UpdateState(GameStateManager gameState)
    {

    }
}

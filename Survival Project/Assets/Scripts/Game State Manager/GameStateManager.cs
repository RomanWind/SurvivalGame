using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    private GameBaseState _currentState;
    public GameWaitState waitState = new GameWaitState();
    public GameTravelState travelState = new GameTravelState();
    public GameShopState shopState = new GameShopState();
    public GamePauseState pauseState = new GamePauseState();
    public GameFightState fightState = new GameFightState();

    void Start()
    {
        //_currentState = waitState;
        //_currentState.EnterState(this);
        SwitchState(fightState);
    }

    void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(GameBaseState state)
    {
        _currentState = state;
        state.EnterState(this);
    }

    public GameBaseState GetCurrentState()
    {
        return _currentState;
        //Debug.Log($"Current state is: {_currentState}");
    }

    public bool CurrentStateIsFight()
    {
        if (_currentState == fightState)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

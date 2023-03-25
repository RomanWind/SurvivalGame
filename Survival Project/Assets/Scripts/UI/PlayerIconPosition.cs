using UnityEngine;

public class PlayerIconPosition : MonoBehaviour
{
    [SerializeField] private int _currentRoomIndex;
    [SerializeField] private Player _player;
    [SerializeField] private DungeonMapWaypoints _mapWaypoints;
    [SerializeField] private GameStateManager _gameStateManager;
    private bool _isMoving = false;
    private bool _playerWasMoving = false;
    private Transform _target;
    private float _speed = 1.5f;

    private GameFightState fightState = new GameFightState();

    private void FixedUpdate()
    {
        if (_gameStateManager.CurrentStateIsWait())
        {
            _target = _mapWaypoints.GetWaypointPosition()[_mapWaypoints.GetTargetWaypoint()];
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed);

            if (transform.position != _target.position)
            {
                _isMoving = true;
                _player.MovePlayer();
                _playerWasMoving = true;
            }
            else
            {
                _isMoving = false;

                if (_mapWaypoints.GetWaypointName()[_mapWaypoints.GetTargetWaypoint()] == "Button")
                {
                    _player.StopPlayerRunAnimation();
                }
                else
                {
                    _mapWaypoints.IncrementTargetWaypoint();
                    return;
                }

                if (_playerWasMoving == true && _mapWaypoints.GetWaypointName()[_mapWaypoints.GetTargetWaypoint()] == "Button")
                {
                    _playerWasMoving = false;
                    _gameStateManager.SwitchState(fightState);
                }
            }
        }
    }

    public int GetCurrentRoomIndex()
    {
        return _currentRoomIndex;
    }

    public void SetCurrentRoomIndex(int indexToSet)
    {
        _currentRoomIndex = indexToSet;
    }

    public bool IsMoving()
    {
        return _isMoving;
    }
}

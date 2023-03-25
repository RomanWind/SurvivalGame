using UnityEngine;

public class MapButton : MonoBehaviour
{
    [SerializeField] private GameStateManager _gameStateManager;
    [SerializeField] private DungeonMapWaypoints _mapWaypoints;
    [SerializeField] private PlayerIconPosition _playerIcon;
    [SerializeField] private int _buttonIndex;

    public void OnDungeonRoomSwap()
    {
        int nextDungeonRoom = _playerIcon.GetCurrentRoomIndex() + 1;

        if (_gameStateManager.CurrentStateIsWait())
        {
            if (_playerIcon.IsMoving() == false && _buttonIndex == nextDungeonRoom)
            {
                _mapWaypoints.IncrementTargetWaypoint();
                _playerIcon.SetCurrentRoomIndex(_buttonIndex);
            }
        }
    }
}

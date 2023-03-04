using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Player _player;
    [SerializeField] private int _enemiesAmount = 2;
    [SerializeField] private GameStateManager _gameStateManager;
    [SerializeField] private Transform _enemySpawnLocation;
    [SerializeField] private List<GameObject> _enemyTypes = new List<GameObject>();
    private List<GameObject> _enemiesList = new List<GameObject>();
    private List<Enemy> _enemies = new List<Enemy>();
    private int _currentEnemyIndex;

    private GameWaitState waitState = new GameWaitState();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(_gameStateManager.CurrentStateIsFight())
        {
            //Player DealDamage
            if (_player.AttackEnemy())
            {
                _enemies[_currentEnemyIndex].RecieveDamage(_player.GetPlayerDamage());
            }
            if (_enemies[_currentEnemyIndex].GetEnemyHealth() <= 0 && _currentEnemyIndex < _enemiesAmount - 1)
            {
                _currentEnemyIndex += 1;
            }
            if (_enemies[_currentEnemyIndex].GetEnemyHealth() <= 0 && _currentEnemyIndex == _enemiesAmount - 1)
            {
                _gameStateManager.SwitchState(waitState);
            }

            //Enemy DealDamage
            for (int i = 0; i < _enemiesAmount; i++)
            {
                if (_enemies[i].DealDamage())
                {
                    _player.RecieveDamage(_enemies[i].GetEnemyAttack());
                }
            }
        }
    }

    public void FightStateInitiated()
    {
        for (int i = 0; i < _enemiesAmount; i++)
        {
            _enemiesList.Add(SpawnEnemy(_enemyTypes[0]));
            _enemySpawnLocation.position += Vector3.right * 1;
            _enemies.Add(_enemiesList[i].GetComponent<Enemy>());
        }
        _currentEnemyIndex = 0;
    }

    private GameObject SpawnEnemy(GameObject enemyToSpawn)
    {
        return Instantiate(enemyToSpawn, _enemySpawnLocation.position, _enemySpawnLocation.rotation);
    }
}

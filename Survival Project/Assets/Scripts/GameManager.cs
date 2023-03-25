using System.Collections.Generic;
using InventorySystem;
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

    //Inventory
    [SerializeField] private GameObject[] _items = new GameObject[2];
    [SerializeField] private Inventory _inventory;
    private GameItem[] _gameItems = new GameItem[2];
    private int _itemToSpawn;
    
    private void Awake()
    {
        instance = this;

        _inventory = _player.GetComponent<Inventory>();
        for (int i = 0; i < _items.Length; i++)
        {
            _gameItems[i] = _items[i].GetComponent<GameItem>();
        }
    }

    private void Update()
    {
        if(_gameStateManager.CurrentStateIsFight())
        {
            //Player DealDamage
            if (_player.AttackEnemy()) _enemies[_currentEnemyIndex].RecieveDamage(_player.GetPlayerDamage());
            
            if (_enemies[_currentEnemyIndex].GetEnemyHealth() <= 0 && _currentEnemyIndex < _enemiesAmount - 1)
            {
                //Give loot and randomize loot by _itemToSpawn variable 
                _itemToSpawn = 0;
                if (_inventory.CanAcceptItem(_gameItems[_itemToSpawn].Stack))
                {
                    _inventory.AddItem(SpawnLoot(_itemToSpawn).GetComponent<GameItem>().Pick());
                }
                _currentEnemyIndex++;
            }
            if (_enemies[_currentEnemyIndex].GetEnemyHealth() <= 0 && _currentEnemyIndex == _enemiesAmount - 1)
            {
                _itemToSpawn = 0;
                if (_inventory.CanAcceptItem(_gameItems[_itemToSpawn].Stack))
                {
                    _inventory.AddItem(SpawnLoot(_itemToSpawn).GetComponent<GameItem>().Pick());
                }

                _gameStateManager.SwitchState(waitState);
                _enemiesList.Clear();
                _enemies.Clear();

                return;
            }

            //Enemy DealDamage
            for (int i = 0; i < _enemiesAmount; i++)
            {
                if (_enemies[i].DealDamage()) _player.RecieveDamage(_enemies[i].GetEnemyAttack());
            }
        }
    }

    public void FightStateInitiated()
    {
        _enemySpawnLocation.position = _player.transform.position + new Vector3(3.46f,-0.574f, 0);

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

    private GameObject SpawnLoot(int indexOfSpawnedItem)
    {
        return Instantiate(_items[indexOfSpawnedItem]);
    }
}

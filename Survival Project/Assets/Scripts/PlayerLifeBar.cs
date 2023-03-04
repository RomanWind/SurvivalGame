using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeBar : MonoBehaviour
{
    [SerializeField] private Image _playerHealthBar;
    [SerializeField] private Player _player;
    private float _playerMaxHealth;
    private float _playerHealth;

    private void Update()
    {
        UpdatePlayerLife();
    }

    private void UpdatePlayerLife()
    {
        _playerHealth = _player.GetPlayerHealth();
        _playerMaxHealth = _player.GetPlayerMaxHealth();
        float completionRatio = _playerHealth / _playerMaxHealth;
        _playerHealthBar.fillAmount = completionRatio;
    }
}

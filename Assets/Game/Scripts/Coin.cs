using UnityEngine;

public abstract class Coin : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    protected GameLogic _gameLogic;
    protected CoinsSpawner _coinsSpawner;
    protected int _coinNumber;
    public void SetupCoin(int index, Sprite sprite, CoinsSpawner coinsSpawner, GameLogic gameLogic)
    {
        _coinsSpawner = coinsSpawner;
        _coinNumber = index;
        _gameLogic = gameLogic;
        _spriteRenderer.sprite = sprite;
        name = $"Coin {_coinNumber}";
    }
    public int GetCoinNumber()
    {
         return _coinNumber;
    }
}

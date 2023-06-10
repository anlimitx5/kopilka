using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    public event Action OnSpawnCoin;
    public event Action OnAddCoinToBox;
    public event Action OnMaximumCoinsInBox;
    public List<int> coinsNumberInBox { get; private set; } = new List<int>();
    public List<TestingCoin> testingCoinsList { get; private set; } = new List<TestingCoin>();
    [SerializeField] private float _spawnCoinEverySeconds = 3;
    [SerializeField] private float _firstSpawnCoinDelaySeconds = 4;
    [SerializeField] private FallingCoin _fallingCoinPrefab;
    [SerializeField] private TestingCoin _testingCoinPrefab;
    [SerializeField] private Animator _pigAnimator;
    [SerializeField] private Transform _fallingCoinsTranform;
    [SerializeField] private Transform _testingCoinsTranform;
    [SerializeField] private Sprite[] _coinSprite;
    [SerializeField] private GameLogic _gameLogic;
    private List<int> _availableCoinsIndex = new List<int>();

    private void OnEnable()
    {
        _gameLogic.OnGameStart += BeginCoinSpawning;
        OnMaximumCoinsInBox += SpawnTestingCoins;
    }
    private void OnDisable()
    {
        _gameLogic.OnGameStart -= BeginCoinSpawning;
        OnMaximumCoinsInBox -= SpawnTestingCoins;
    }
    public void BeginCoinSpawning()
    {
        for (int i = 0; i < _coinSprite.Length; i++)
        {
            _availableCoinsIndex.Add(i);
        }
        Invoke(nameof(SpawnFallingCoin), _firstSpawnCoinDelaySeconds);
    }
    private void SpawnFallingCoin()
    {
        OnSpawnCoin?.Invoke();
        int coinNumber = ChangeCoinNumber();
        FallingCoin coin = Instantiate(_fallingCoinPrefab, _fallingCoinsTranform.position, Quaternion.identity, _fallingCoinsTranform);
        coin.SetupCoin(coinNumber, _coinSprite[coinNumber], this, _gameLogic);
        _availableCoinsIndex.Remove(coinNumber);
        ContinueSpawnFallingCoins();
    }
    private void ContinueSpawnFallingCoins()
    {
        if (_gameLogic.currentCoinsValue >= _gameLogic.maximumCoinsValue) return;
        if (_gameLogic.currentCoinsValue != _gameLogic.maximumCoinsValue)
        {
            Invoke(nameof(SpawnFallingCoin), _spawnCoinEverySeconds);
        }
    }
    public void SpawnTestingCoins()
    {
        int spawnHeight = 0;
        for (int i = 0; i < _coinSprite.Length; i++)
        {
            Vector3 ChangedCoinPosition = new Vector3(_testingCoinsTranform.position.x, _testingCoinsTranform.position.y + spawnHeight, _testingCoinsTranform.position.z);
            TestingCoin coin = Instantiate(_testingCoinPrefab, ChangedCoinPosition, Quaternion.identity, _testingCoinsTranform);
            coin.SetupCoin(i, _coinSprite[i], this, _gameLogic);
            testingCoinsList.Add(coin);
            spawnHeight -= 2;
        }
    }
    public void AddCoinToBox(int coinIndex)
    {
        bool spawnedMaximumCoins = _gameLogic.currentCoinsValue == _gameLogic.maximumCoinsValue;
        string pigTrigger = spawnedMaximumCoins ? "Full" : "React";
        _pigAnimator.SetTrigger(pigTrigger);
        coinsNumberInBox.Add(coinIndex);
        if (spawnedMaximumCoins)
        {
            Invoke(nameof(MaximumCoinsInBox), 2);
        }
    }
    private void MaximumCoinsInBox()
    {
        OnMaximumCoinsInBox?.Invoke();
    }
    private int ChangeCoinNumber()
    {
        int indexInAvailableList = UnityEngine.Random.Range(0, _availableCoinsIndex.Count);
        int coinNumber = _availableCoinsIndex[indexInAvailableList];
        return coinNumber;
    }
}

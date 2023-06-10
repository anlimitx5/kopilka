using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public event Action OnGameStart;
    public event Action OnCorrectCoin;
    public event Action OnVictory;
    public event Action OnDefeat;
    public int currentCoinsValue { get; private set; } = 0;
    public bool canClickOnCoins { get; private set; } = false;
    public readonly int maximumCoinsValue = 4;
    [SerializeField] private  Sprite _correctSprite;
    [SerializeField] private  Sprite _wrongSprite;
    [SerializeField] private CoinsSpawner _coinsSpawner;
    [SerializeField] private ParticleSystem _confetti;
    private int _coinIndexForCompare = 0;

    public void StartGame()
    {
        OnGameStart?.Invoke();
    }
    private void Victory()
    {
        canClickOnCoins = false;
        _confetti.gameObject.SetActive(true);
        OnVictory?.Invoke();
    }
    private void Defeat()
    {
        canClickOnCoins = false;
        OnDefeat?.Invoke();
    }
    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void ExitGame()
    {
       Application.Quit();
    }
    public void CheckCoinEquality(int indexInList)
    {
        if (_coinIndexForCompare >= maximumCoinsValue) return;

        bool result = _coinsSpawner.testingCoinsList[indexInList].GetCoinNumber() == _coinsSpawner.coinsNumberInBox[_coinIndexForCompare];
        Sprite spriteResult = result ? _correctSprite : _wrongSprite;
        _coinsSpawner.testingCoinsList[indexInList].SetCorrectOrWrongSprite(spriteResult);

        if (!result)
        {
            Defeat();
        }
        else
        {
            _coinIndexForCompare = Mathf.Clamp(_coinIndexForCompare + 1, 0, maximumCoinsValue);

            if (_coinIndexForCompare == maximumCoinsValue)
            {
                Victory();
            }
            else
            {
                OnCorrectCoin?.Invoke();
            }
        }
    }
    private void IncrementCurrentCoinsValue()
    {
        currentCoinsValue = Mathf.Clamp(currentCoinsValue + 1, 0, maximumCoinsValue);
    }
    private void EnableClickOnCoins()
    {
        canClickOnCoins = true;
    }
    private void OnEnable()
    {
        _coinsSpawner.OnSpawnCoin += IncrementCurrentCoinsValue;
        _coinsSpawner.OnMaximumCoinsInBox += EnableClickOnCoins;
    }
    private void OnDisable()
    {
        _coinsSpawner.OnSpawnCoin -= IncrementCurrentCoinsValue;
        _coinsSpawner.OnMaximumCoinsInBox -= EnableClickOnCoins;
    }
}

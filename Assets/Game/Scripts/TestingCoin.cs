using UnityEngine;

public class TestingCoin : Coin
{
    private bool _wasClicked;
    private void OnMouseUpAsButton()
    {
        ChooseCoin();
    }
    private void ChooseCoin()
    {
        if (_wasClicked || !_gameLogic.canClickOnCoins) return;
        _wasClicked = true;
        _gameLogic.CheckCoinEquality(_coinNumber);
    }
    public void SetCorrectOrWrongSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}

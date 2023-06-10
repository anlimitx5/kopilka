using UnityEngine;

public class FallingCoin : Coin
{
    public void AddCoinToBox()
    {
        _coinsSpawner.AddCoinToBox(_coinNumber);
        Destroy(gameObject);
    }
}

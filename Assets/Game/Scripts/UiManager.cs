using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameLogic _gameLogic;
    [SerializeField] private GameObject _victoryUi;
    [SerializeField] private GameObject _defeatUi;
    [SerializeField] private Button[] _restartButton;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _exitGameButton;
    private float _delayGameOver = 2;

    private void Awake()
    {
        SetListeners();
        _startGameButton.gameObject.SetActive(true);
    }
    private void SetListeners()
    {
        _startGameButton.onClick.AddListener(_gameLogic.StartGame);
        _restartButton[0].onClick.AddListener(_gameLogic.Restart);
        _restartButton[1].onClick.AddListener(_gameLogic.Restart);
        _exitGameButton.onClick.AddListener(_gameLogic.ExitGame);
    }
    private void ShowVictory()
    {
        Invoke(nameof(DelayedShowVictory), _delayGameOver);
    }
    private void ShowDefeat()
    {
        Invoke(nameof(DelayedShowDefeat), _delayGameOver);
    }
    private void DelayedShowVictory()
    {
        _victoryUi.gameObject.SetActive(true);
    }
    private void DelayedShowDefeat()
    {
        _defeatUi.gameObject.SetActive(true);
    }
    private void OnEnable()
    {
        _gameLogic.OnVictory += ShowVictory;
        _gameLogic.OnDefeat += ShowDefeat;
    }
    private void OnDisable()
    {
        _gameLogic.OnVictory -= ShowVictory;
        _gameLogic.OnDefeat -= ShowDefeat;
    }
}

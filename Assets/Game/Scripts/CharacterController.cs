using Spine.Unity;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private GameLogic _gameLogic;

    [SerializeField] private SkeletonAnimation _skeletonAnimation;    

    [SerializeField] private AnimationReferenceAsset _stateCorrect;
    [SerializeField] private AnimationReferenceAsset _stateGreet;
    [SerializeField] private AnimationReferenceAsset _stateIdle;
    [SerializeField] private AnimationReferenceAsset _stateWin;
    [SerializeField] private AnimationReferenceAsset _stateWrong;

    private void Start()
    {
        SetState_Idle();
    }
    private void OnEnable()
    {
        _gameLogic.OnGameStart += SetState_Greet;
        _gameLogic.OnCorrectCoin += SetState_Correct;
        _gameLogic.OnVictory += SetState_Win;
        _gameLogic.OnDefeat += SetState_Wrong;
    }
    private void OnDisable()
    {
        _gameLogic.OnGameStart -= SetState_Greet;
        _gameLogic.OnCorrectCoin -= SetState_Correct;
        _gameLogic.OnVictory -= SetState_Win;
        _gameLogic.OnDefeat -= SetState_Wrong;
    }
    private void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        _skeletonAnimation.state.SetAnimation(0,animation,loop).TimeScale = timeScale;
        if(!loop)
        {
            _skeletonAnimation.state.AddAnimation(0, _stateIdle, true, animation.Animation.Duration).TimeScale = 2f;
        }
    }

    private void SetState_Correct()
    {
        AnimationReferenceAsset state = _stateCorrect;
        SetAnimation(state, false, 1f);
    }
    private void SetState_Greet()
    {
        AnimationReferenceAsset state = _stateGreet;
        SetAnimation(state, false, 1f);
    }
    private void SetState_Idle()
    {
        AnimationReferenceAsset state = _stateIdle;
        SetAnimation(state, true, 2f);
    }
    private void SetState_Win()
    {
        AnimationReferenceAsset state = _stateWin;
        SetAnimation(state, false, 1f);
    }
    private void SetState_Wrong()
    {
        AnimationReferenceAsset state = _stateWrong;
        SetAnimation(state, false, 1f);
    }
}

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInput), typeof(PlayerController), typeof(PlayerStates))]
public class PlayerSprintAbility : MonoBehaviour
{
    private enum SprintSetting { Hold, Toggle }

    #region Inspector

    [Header("Sprint Settings")]
    [SerializeField] private SprintSetting sprintSetting = SprintSetting.Hold;
    [SerializeField] private float sprintingSpeed = 9f;
    
    [Header("Headbob Settings")] 
    [SerializeField] private float sprintBobSpeed = 16f;
    [SerializeField] private float sprintBobAmount = 0.1f;
    
    [Header("Events")] 
    [SerializeField] private UnityEvent onSprintStarted;
    [SerializeField] private UnityEvent onSprintEnded;
    
    #endregion

    #region Private Variables

    private PlayerInput _playerControlInput;
    private PlayerController _playerController;
    private PlayerStates _playerState;

    #endregion

    #region Unity Events

    private void Awake()
    {
        _playerControlInput = GetComponent<PlayerInput>();
        _playerController = GetComponent<PlayerController>();
        _playerState = GetComponent<PlayerStates>();
    }

    private void OnEnable()
    {
        _playerControlInput.OnSprintChanged += Sprint;
        _playerState.OnStateChange += OnStateChanged;
    }

    private void OnDisable()
    {
        _playerControlInput.OnSprintChanged -= Sprint;
        _playerState.OnStateChange -= OnStateChanged;
    }

    #endregion

    #region Sprint Methods

    private void Sprint(bool isPressed)
    {
        switch (sprintSetting)
        {
            case SprintSetting.Hold:
                if (isPressed)
                {
                    SprintInitiate();
                }
                else
                {
                    SprintRelease();
                }
                break;

            case SprintSetting.Toggle:
                if (!isPressed) break;

                if (_playerState.CompareState(MovementState.Running))
                {
                    SprintRelease();
                }
                else
                {
                    SprintInitiate();
                }
                break;
        }
    }

    private void SprintInitiate()
    {
        if (_playerState.CompareState(MovementState.Crouching)) return;

        _playerState.SetState(MovementState.Running);
    }

    private void SprintRelease()
    {
        if (!_playerState.CompareState(MovementState.Running)) return;

        _playerState.SetState(MovementState.Walking);
    }

    private void OnStateChanged(MovementState oldState, MovementState newState)
    {
        if (newState == MovementState.Running)
        {
            _playerController.SetMoveSpeed(sprintingSpeed);
            _playerController.SetHeadbob(sprintBobSpeed, sprintBobAmount);
            
            onSprintStarted?.Invoke();
        }
        else if (oldState == MovementState.Running)
        {
            onSprintEnded?.Invoke();
        }
    }

    #endregion
}
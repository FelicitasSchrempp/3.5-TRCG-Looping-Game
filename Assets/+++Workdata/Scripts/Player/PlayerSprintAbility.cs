using Unity.U2D.Physics;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(PlayerController), typeof(PlayerStates))]
public class PlayerSprintAbility : MonoBehaviour
{
    private enum SprintSetting { Hold, Toggle }

    #region Inspector

    [Header("Sprint Settings")]
    [SerializeField] private SprintSetting sprintSetting = SprintSetting.Hold;
    [SerializeField] private float sprintingSpeed = 12f;
    
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
        if (oldState == MovementState.Running)
        {
            print("sprinting end");
        }
        
        if (newState != MovementState.Running) return;
        
        _playerController.SetMoveSpeed(sprintingSpeed);
    }

    #endregion
}
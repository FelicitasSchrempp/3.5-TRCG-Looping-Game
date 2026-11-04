using System;
using UnityEngine;

public enum MovementState{Default, Walking, Running, Crouching, Jumping}
public class PlayerStates : MonoBehaviour
{
    public event Action<MovementState, MovementState> OnStateChange;
   
    [SerializeField] private MovementState currentState = MovementState.Walking;
    public MovementState CurrentState => currentState;
   
    public bool CompareState(MovementState state)
    {
        return currentState == state;
    }

    public void SetState(MovementState newState)
    {
        if (newState == currentState) return;
      
        MovementState oldState = currentState;
        currentState = newState;
        OnStateChange?.Invoke(oldState, newState);
    }
}

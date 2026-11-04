using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(PlayerRaycast))]
public class PlayerInteraction : MonoBehaviour
{
    #region Private Variable
   
    private PlayerInput _playerControlInput;
    private PlayerRaycast _playerRaycast;
   
    #endregion
   
    #region Unity Events

    private void Awake()
    {
        _playerControlInput = GetComponent<PlayerInput>();
        _playerRaycast = GetComponent<PlayerRaycast>();
    }

    private void OnEnable()
    {
        _playerControlInput.OnInteractPressed  += Interact;
      
    }

    private void OnDisable()
    {
        _playerControlInput.OnInteractPressed  -= Interact;
    }
   
    #endregion
   
    #region Interaction Methods

    private void Interact()
    {
        BaseInteractable target = _playerRaycast.CurrentInteractable;
        if (target == null) return;
      
        target.Interact();
    }
   
    #endregion
}

using UnityEngine;
using UnityEngine.Events;

public class DialogueInteractable : BaseInteractable
{
    [SerializeField] bool singleInteraction;
    
    private bool _isStillSelected;
    private bool _DialogueActive;
    
    public override void Interact()
    {
        if (!canInteract) return;
        if (singleInteraction) canInteract = false;
        
        DialogueController.OnDialogueStarted?.Invoke(this);
        _DialogueActive = true;
        base.Interact();
        base.Unhover();
    }
    
    public override void Hover()
    {
        _isStillSelected = true;
        
        if (_DialogueActive) return;
        if (!canInteract) return;
        
        base.Hover();
    }

    public override void Unhover()
    {
        _isStillSelected = false;
        
        if (_DialogueActive) return;
        if (!canInteract) return;
        
        base.Unhover();
    }

    public void TrySelected()
    {
        if (!_isStillSelected) return;
        _DialogueActive = false;
        base.Hover();
    }
}

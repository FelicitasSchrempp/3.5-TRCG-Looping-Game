using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    
    private DialogueController _dialogueController;
    private Button _lastSelectable;
    private bool _isInCutScene;
    
    #region Unity Event Functions

    private void Awake()
    {
        Instance = this;
        _dialogueController = FindFirstObjectByType<DialogueController>();
    }

    private void OnEnable()
    {
        DialogueController.DialogueClosed += EndDialogue;
    }

    private void OnDisable()
    {
        DialogueController.DialogueClosed -= EndDialogue;
    }

    #endregion

    #region Modes

    private void EnterPlayMode()
    {
        if (_isInCutScene) return;
        
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;

        ShowGameHud();
    }

    private void EnterDialogueMode()
    {
        HideHud();
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void EnterAutoMovementMode()
    {
        HideHud();
    }

    private void EnterCutsceneMode()
    {
        HideHud();
        _isInCutScene = true;
    }

    #endregion

    public void StartDialogue(string dialoguePath)
    {
        EnterDialogueMode();
        _dialogueController.StartDialogue(dialoguePath);
    }

    public void StartCutsceneMode()
    {
        EnterCutsceneMode();
    }
    
    public void EndCutsceneMode()
    {
        _isInCutScene = false;
        EnterPlayMode();
    }

    private void EndDialogue()
    {
        EnterPlayMode();
    }

    public void StartAutoMovement()
    {
        EnterAutoMovementMode();
    }
    
    public void EndAutoMovement()
    {
        EnterPlayMode();
    }
    
    public void SetLastSelectable()
    {
        SetSelectable(_lastSelectable);
    }
    public void SetSelectable(Button newSelactable)
    {
        Selectable newSelectable;
        _lastSelectable = newSelactable;
        newSelectable = newSelactable;

        //newSelactable.Select();
        StartCoroutine(DelayNewSelectable(newSelectable));
    }

    public void ExitMenu()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowGameHud()
    {
        //gameHudAnimator.Play("ShowHud");
    }

    public void HideHud()
    {
        //gameHudAnimator.Play("HideHud");
    }
    
    IEnumerator DelayNewSelectable(Selectable newSelectable)
    {
        yield return null;
        newSelectable.Select();
    }
}


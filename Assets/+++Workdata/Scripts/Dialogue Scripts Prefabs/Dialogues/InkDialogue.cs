using System;
using UnityEngine;
using UnityEngine.Events;

public class InkDialogue : MonoBehaviour
{
    #region Inspector

    [Tooltip("Path to a specified knot.stitch in the ink file.")]
    [SerializeField] private string dialoguePath;
    
    #endregion
    
    private DialogueController _dialogueController;

    private void Awake()
    {
        _dialogueController = FindFirstObjectByType<DialogueController>();
    }

    public void StartDialogue(string dialoguePath)
    {
        if (string.IsNullOrWhiteSpace(dialoguePath))
        {
            Debug.LogWarning("No dialogue path defined", this);
            return;
        }
        
        _dialogueController.StartDialogue(dialoguePath);
    }
    
    public void StartDialogue()
    {
        if (string.IsNullOrWhiteSpace(dialoguePath))
        {
            Debug.LogWarning("No dialogue path defined", this);
            return;
        }
        
        FindObjectOfType<GameController>().StartDialogue(dialoguePath);
    }
}
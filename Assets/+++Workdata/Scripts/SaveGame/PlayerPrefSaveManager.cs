using UnityEngine;

public class PlayerPrefSaveManager : MonoBehaviour
{
    public string playerPrefKey = "default";
    
    public SaveData saveData;

    public int saveSlotIndex;

    private void Start()
    {
        if (PlayerPrefs.HasKey(playerPrefKey))
        {
            saveData = JsonManager.LoadFromPlayerPref<SaveData>(playerPrefKey);
            LoadGame();
            return;
        }
        JsonManager.SaveToPlayerPref(playerPrefKey, saveData);
    }

    public void SaveGame()
    {
        Vector3 playerPosition = GameObject.FindAnyObjectByType<PlayerController>().transform.position;
        saveData.saveSlots[saveSlotIndex].playerPosition = playerPosition;
        
        JsonManager.SaveToPlayerPref(playerPrefKey, saveData);
    }

    public void LoadGame()
    { 
        GameObject.FindAnyObjectByType<PlayerController>().transform.position = saveData.saveSlots[saveSlotIndex].playerPosition;
    }

    
    [ContextMenu("Delete All PlayerPrefs")]
    public void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}

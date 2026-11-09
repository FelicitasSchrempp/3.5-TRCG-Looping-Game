using UnityEngine;
using File = System.IO.File;

public class JsonManager
{
    #region PlayerPrefs

    public static void SaveToPlayerPref<T>(string playerPrefKey, T data)
    {
        string json = JsonUtility.ToJson(data);
        Debug.Log(json);
        PlayerPrefs.SetString(playerPrefKey, json);
        PlayerPrefs.Save();
    }

    public static T LoadFromPlayerPref<T>(string playerPrefKey)
    {
        return JsonUtility.FromJson<T>(PlayerPrefs.GetString(playerPrefKey));
    }

    #endregion

}

using UnityEngine;
using System.Collections;

public class CloudSaveGUI : MonoBehaviour {

    public void SetGameSave(int i)
    {
        GooglePlayManager.instance.SetGameSave(i);
        GooglePlayManager.instance.SaveToCloud();
        
    }

    public void ShowSelectUI()
    {
        GooglePlayManager.instance.ShowSelectUI();
    }
}

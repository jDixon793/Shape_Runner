using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShapeSelector : MonoBehaviour {


    public int sceneNum;


    void Awake()
    {
        sceneNum = 1;

    }
    public void ShapeSelect(int shape)
    {
        PlayerPrefs.SetInt("shapeNumber", shape);
        PlayerPrefs.Save();
        Application.LoadLevel(sceneNum);
    }

    public void ResetUserData()
    {
        PlayerPrefs.DeleteAll();
    }
}

using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour
{

    public void LoadScene(int level)
    {
        Application.LoadLevel(level);
    }
}

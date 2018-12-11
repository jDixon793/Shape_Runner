using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GrabScore : MonoBehaviour {

    public string scoreType;
    //totalScore,lastScore,topScore
	// Use this for initialization
	void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {
        if (PlayerPrefs.HasKey(scoreType))
        {
            int score = PlayerPrefs.GetInt(scoreType);
            if (score > 0)
            {
                GetComponent<Text>().text = score.ToString();
                GetComponent<Text>().enabled = true;
            }
        }
        else
        {
            GetComponent<Text>().text = "";
            GetComponent<Text>().enabled = false;
        }
    }
}

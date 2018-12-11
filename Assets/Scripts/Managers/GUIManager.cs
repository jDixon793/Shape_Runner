using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour {

    public Text score;
    public GameObject ground;
    bool isPaused;
    public Sprite pause;
    public Sprite play;
    public GameObject pauseToggle;
	void Start ()
    {
        isPaused = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        score.text =  (int)(ground.transform.position.x * -1)+"";
	}

    public void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            pauseToggle.GetComponent<Image>().sprite = pause;
            isPaused = !isPaused;
        }
        else
        {
            Time.timeScale = 0;
            pauseToggle.GetComponent<Image>().sprite = play;
            isPaused = !isPaused;
        }
    }
}

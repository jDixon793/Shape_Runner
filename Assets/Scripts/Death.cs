using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Death : MonoBehaviour
{
    
    public GameObject score;
    public GameObject gameOverGUI;
    public float waitTime = 1.7f;
    public void OnCollisionEnter2D(Collision2D collision)
    {
       
        StartCoroutine(EndGame());
        
    }

    public IEnumerator EndGame()
    {
        int finalScore = int.Parse(score.gameObject.GetComponent<Text>().text);
        int totalScore = PlayerPrefs.GetInt("totalScore");
        int highScore = PlayerPrefs.GetInt("highScore");

        if (finalScore>highScore)
        {
            PlayerPrefs.SetInt("highScore", finalScore);
        }
        PlayerPrefs.SetInt("lastScore", finalScore);
        PlayerPrefs.SetInt("totalScore",finalScore+totalScore);
        PlayerPrefs.Save();

        AdManager.instance.HideBanner();
        AdManager.instance.IncrementDeaths();
        GooglePlayManager.instance.SaveToCloud();
        gameOverGUI.SetActive(true);
        score.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        Application.LoadLevel(0);
    }
}

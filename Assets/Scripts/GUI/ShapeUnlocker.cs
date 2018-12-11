using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShapeUnlocker : MonoBehaviour {

    public GameObject shapeParent;
    public int unlockAmount;
    // Use this for initialization
    void Start ()
    {

        if (PlayerPrefs.HasKey("totalScore"))
        {
            int score = PlayerPrefs.GetInt("totalScore");
            if (score > unlockAmount)
            {
                GetComponent<Text>().text = "";
                shapeParent.GetComponent<Button>().interactable = true;
            }
            else
            {
                GetComponent<Text>().text = unlockAmount.ToString();
                shapeParent.GetComponent<Button>().interactable = false;
            }
        }
        else if(unlockAmount==0)
        {
            GetComponent<Text>().text = "";
            shapeParent.GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Text>().text = unlockAmount.ToString();
            shapeParent.GetComponent<Button>().interactable = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

}

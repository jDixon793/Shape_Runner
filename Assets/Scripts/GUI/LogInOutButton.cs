using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LogInOutButton : MonoBehaviour {

	void Update () {
	
        if(this.gameObject.activeSelf)
        {
            if(GooglePlayManager.instance.loggedIn)
            {
                GetComponentInChildren<Text>().text = "Sign Out of google play";
            }
            else
            {
                GetComponentInChildren<Text>().text = "Sign In to google play";
            }
        }
	}
}

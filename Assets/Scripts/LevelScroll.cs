using UnityEngine;
using System.Collections;

public class LevelScroll : MonoBehaviour {

    public float scrollSpeed;
    bool started = false;
    public bool startScrollOnLoad = false;


	
	// Update is called once per frame
	void Update () {
        if (startScrollOnLoad)
        {
            started = true;
        }
        if (Input.GetButtonDown("Fire1")&&!started)
        {
            started = true;
        }
        if (started)
        {
            transform.Translate(Vector3.left * Time.deltaTime * scrollSpeed);
        }
    }
}

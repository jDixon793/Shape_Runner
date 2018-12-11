using UnityEngine;
using System.Collections;

public class FloorDestroy : MonoBehaviour {

    float curX;
    public float startX;
    public float destroyDistance;
    public GameObject ground;

    // Use this for initialization
	void Start ()
    {
        startX = gameObject.transform.localPosition.x;
        ground = GameObject.FindGameObjectWithTag("Ground");
	}
	
	// Update is called once per frame
	void Update ()
    {
        curX= ground.transform.position.x*-1;
        if (curX>destroyDistance+startX)
        { 
            GameObject.Destroy(gameObject);
        }
	}
}

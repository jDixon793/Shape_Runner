using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float thrust;
    public float buffer = .2f;
    public float rotBuffer = .1f;
    public float settleTime = 15f;
    public float spinSpeed;
    public int numSides;
    float startPos;
    GameObject groundCheck;
    RaycastHit2D hit;
    float rotationEnd;
    public bool grounded;
    public bool jump;
    public bool started;
    public bool settled; //shape has settled on the floor
    public LayerMask mask;

    // Use this for initialization
    void Start () {
        //turn on ads when the player gets dropped in
        AdManager.instance.ShowBanner();

        startPos = transform.position.x;
        started = false;
        jump = false;
        grounded = false;
	}
	
	// use update for nonphysics
	void Update ()
    {
        if(transform.position.x != startPos)
        {
            float pos = Mathf.Lerp(transform.position.x, startPos, Time.deltaTime);
            transform.position = new Vector3(pos, transform.position.y, transform.position.z);
        }
        /*hit = Physics2D.Raycast(transform.position, Vector2.down, buffer,mask);
        if (hit.collider != null)
        { 
            //Debug.Log(hit.collider.gameObject.name);
            grounded = hit.collider.tag == "Ground";
        }

        else
        {
            grounded = false;
        }*/
       // grounded = true;
        if (Input.GetButtonDown("Fire1") && grounded && started)
        {
            
            jump = true;
            grounded = false;
            StopCoroutine(RotateToEnd(rotationEnd));
            
        }
        else if (!started&&Input.GetButtonDown("Fire1") && grounded)
        {
            started = true;
        }
        if(!grounded)//if we are not touching the ground we want to spin
        {
            transform.Rotate(Vector3.back, spinSpeed * Time.deltaTime);
            settled = false;
        }
        if (grounded&& !settled)
        {
            rotationEnd = (float)((int)transform.localEulerAngles.z/(int)(360 / numSides));
            rotationEnd *= (360f / numSides);

            StartCoroutine(RotateToEnd(rotationEnd));
            settled = true;
        }
    }
    public IEnumerator RotateToEnd(float rotEndZ)
    {
        while (Mathf.Abs(rotEndZ - transform.localEulerAngles.z) <= rotBuffer)
        {
            Debug.Log((rotEndZ - transform.localEulerAngles.z));
            if((transform.localEulerAngles.z)<=2|| (transform.localEulerAngles.z) >= 358)//hardcode solution to wierd 0-360 problem
            {
                Vector3 a = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
                transform.rotation = Quaternion.Euler(a);
                break;
            }
            Vector3 angle = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y,rotEndZ);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(angle),Time.deltaTime * settleTime );
            yield return new WaitForEndOfFrame();
        }
    }
    //use fixed update for physics
    void FixedUpdate()
    {
        if(jump)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, thrust, 0);
            jump = false;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;
    }



}

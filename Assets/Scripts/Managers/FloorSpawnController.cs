using UnityEngine;
using System.Collections;

public class FloorSpawnController : MonoBehaviour {

    public GameObject floor;
    public GameObject currentFloor;
    
    public GameObject ground;
    public GameObject[] playerShapes;
    public int numPlatforms;
    public float spawnMinY;
    public float spawnOffset;
    public float floorLength;
    public float spawnPosX;
    public float floorOffset;
    public bool stopPlayerSpawn;
    float lastSpawnPosX;
    float lastSpawnPosY;
    int lastPlatformNum;
    public int floorCount;

    public GameObject playerSpawn;


    void Start () {
        currentFloor = (GameObject)Instantiate(floor, StartSpawnPos(), Quaternion.identity);
        currentFloor.transform.localScale = new Vector3(floorLength, currentFloor.transform.localScale.y, 1);
        currentFloor.transform.parent = ground.transform;
        currentFloor.GetComponent<BoxCollider2D>().size= new Vector3(1, 1, 1);
        floorCount++;

        if (!stopPlayerSpawn)
        SpawnPlayer();
    }
	
	// Update is called once per frame
	void Update () {

        if(ground.transform.position.x*-1>lastSpawnPosX-(floorLength))
        {
            SpawnNumFloors(1);
        }
       // Instantiate(floor, SpawnPos(), Quaternion.identity);
    }

    Vector3 StartSpawnPos()
    {
        Vector3 spawn = new Vector3(0, spawnMinY + (2 * spawnOffset), 1);
        lastSpawnPosX = 0;
        lastSpawnPosY = spawnMinY + (2 * spawnOffset);
        return spawn;
    }
    Vector3 SpawnPos()
    {
        //Pick to either make a platform up, down or same

        float lastY = lastSpawnPosY;
       // Debug.Log(lastY);
        float spawnY =lastY;
        int rand = Random.Range(0, 2); //
        switch(rand)
        {
            //go up
            case 0:
                if (lastY + spawnOffset < spawnMinY + (numPlatforms * spawnOffset))
                {
                    //Debug.Log(lastY + " + " + spawnOffset + " = " + (lastY + spawnOffset));
                    spawnY = lastY + spawnOffset;
                    
                }
                else
                {
                    //Debug.Log(lastY + " - " + spawnOffset + " = " + (lastY - spawnOffset));
                    spawnY = lastY - spawnOffset;
                }
                break;
             //godown;
            case 1:
                if (lastY - spawnOffset>= spawnMinY)
                {
                    //Debug.Log(lastY + " - " + spawnOffset + " = " + (lastY - spawnOffset));
                    spawnY = lastY - spawnOffset;
                    
                }
                else
                {
                    //Debug.Log(lastY + " + " + spawnOffset + " = " + (lastY + spawnOffset));
                    spawnY = lastY + spawnOffset;
                }
                break;
            //stay the same
            case 2:
                spawnY = lastY;
               // Debug.Log(lastY);
                break;
            default:
               // Debug.Log("default");
                spawnY = lastY;
                break;
        }
        //Debug.Log("Ground: "+ (ground.transform.position.x)+"  "+(lastSpawnPosX + floorLength + floorOffset + (ground.transform.position.x)));
        Vector3 spawn = new Vector3(lastSpawnPosX+floorLength+floorOffset+(ground.transform.position.x), spawnY, 1);
        lastSpawnPosX += floorLength + floorOffset;
        lastSpawnPosY = spawnY;
        return spawn;
    }
    void SpawnNumFloors(int numFloors)
    {
        for (int i = 0; i < numFloors; i++)
        {
            currentFloor = (GameObject)Instantiate(floor, SpawnPos(), Quaternion.identity);
            currentFloor.transform.localScale = new Vector3(floorLength, currentFloor.transform.localScale.y, 1);
            currentFloor.transform.parent = ground.transform;
            currentFloor.GetComponent<BoxCollider2D>().size = new Vector3(1, 1, 1);
            floorCount++;
        }
    }

    void SpawnPlayer()
    {
        if (PlayerPrefs.HasKey("shapeNumber"))
        {
            int player = PlayerPrefs.GetInt("shapeNumber");
            Instantiate(playerShapes[player],playerSpawn.transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(playerShapes[0], playerSpawn.transform.position, Quaternion.identity);
            PlayerPrefs.SetInt("shapeNumber",0);
            Debug.Log("Player prefab doesn't exist");
        }
        
    }
}

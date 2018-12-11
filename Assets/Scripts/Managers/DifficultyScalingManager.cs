using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DifficultyScalingManager : MonoBehaviour {

    public FloorSpawnController floorSpawner;
    public LevelScroll levelScroll;
    public float numFloors;
    public float zoomSpeed;
    public float zoomAmount;
    public float zoomMax;
    public int numFloorsBeforeDifficultyChange;
    //how far apart horizontally
    // public float floorOffsetInc;
    //how far apart vertically
    public float floorOffsetProportion;
    public float floorLengthInc;
    public float scrollSpeedInc;
    bool difficultyIncreased;
	// Use this for initialization
	void Start ()
    {
        difficultyIncreased = false;
        zoomAmount = floorSpawner.spawnOffset;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (numFloors != floorSpawner.floorCount)
        { numFloors = floorSpawner.floorCount; }

        if (numFloors % (float)numFloorsBeforeDifficultyChange == 0 && !difficultyIncreased)
        {
           // Debug.Log("Difficulty Increased");
            IncreaseDifficulty();
            difficultyIncreased = true;

        }
        else if(numFloors % (float)numFloorsBeforeDifficultyChange != 0)
        {
            difficultyIncreased = false;
        }
    }

    void IncreaseDifficulty()
    {
        //how far apart horizontally
        // floorSpawner.floorOffset += floorOffsetInc;

        //how far apart vertically

        if (Camera.main.orthographicSize <= zoomMax)
        {
            StartCoroutine(ZoomCamera());
            floorSpawner.numPlatforms++;
        }
        floorSpawner.floorLength += floorLengthInc;
        levelScroll.scrollSpeed += scrollSpeedInc;
        floorSpawner.floorOffset = levelScroll.scrollSpeed / floorOffsetProportion;
       // Debug.Log(floorSpawner.floorOffset);

    }

   IEnumerator ZoomCamera()
    {
        float endCameraSize = Camera.main.orthographicSize + zoomAmount;
        Vector3 endCameraPos = new Vector3(Camera.main.transform.position.x + (zoomAmount/2), Camera.main.transform.position.y + zoomAmount, Camera.main.transform.position.z);
        
            while (Camera.main.orthographicSize < endCameraSize)
            {

                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, endCameraSize, Time.deltaTime * zoomSpeed);
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, endCameraPos, Time.deltaTime * zoomSpeed);
                yield return new WaitForEndOfFrame();

            }
  
    }
      
}

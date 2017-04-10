using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightningControlScript : MonoBehaviour {

    //Prefabs to be assigned in Editor
    public GameObject BoltPrefab;
    public GameObject BranchPrefab;

    //For pooling
    List<GameObject> activeBoltsObj;
    List<GameObject> inactiveBoltsObj;
    int maxBolts = 50;    

    float scaleText;
    Vector2 positionText;

    //Different modes for the demo
    public enum Mode : byte
    {
        bolt,
        branch,
        moving,
        text,
        nodes,
        burst
    }

    //The current mode the demo is in
    Mode currentMode = Mode.bolt;

    //For handling mouse clicks
    int clicks = 0;
    Vector2 pos1, pos2;

	// Use this for initialization
	void Start () 
    {
        //Initialize lists
        activeBoltsObj = new List<GameObject>();
        inactiveBoltsObj = new List<GameObject>();

        //Grab the parent we'll be assigning to our bolt pool
        GameObject p = GameObject.Find("LightningPoolHolder");

        //For however many bolts we've specified
        for (int i = 0; i < maxBolts; i++)
        {            
            //create from our prefab
            GameObject bolt = (GameObject)Instantiate(BoltPrefab);

            //Assign parent
            bolt.transform.parent = p.transform;

            //Initialize our lightning with a preset number of max sexments
            bolt.GetComponent<LightningBolt>().Initialize(50);

            //Set inactive to start
            bolt.SetActive(false);

            //Store in our inactive list
            inactiveBoltsObj.Add(bolt);
        }

	}
	
	// Update is called once per frame
	void Update () 
    {

        //Declare variables for use later
        GameObject boltObj;
        LightningBolt boltComponent;

        //store off the count for effeciency
        int activeLineCount = activeBoltsObj.Count;

        //loop through active lines (backwards because we'll be removing from the list)
        for (int i = activeLineCount - 1; i >= 0; i--)
        {
            //pull GameObject
            boltObj = activeBoltsObj[i];

            //get the LightningBolt component
            boltComponent = boltObj.GetComponent<LightningBolt>();

            //if the bolt has faded out
            if (boltComponent.IsComplete)
            {
                //deactive the segments it contains
                boltComponent.DeactivateSegments();

                //set it inactive
                boltObj.SetActive(false);

                //move it to the inactive list
                activeBoltsObj.RemoveAt(i);
                inactiveBoltsObj.Add(boltObj);
            }
        }         

        //update and draw active bolts
        for (int i = 0; i < activeBoltsObj.Count; i++)
        {
            activeBoltsObj[i].GetComponent<LightningBolt>().UpdateBolt();
            activeBoltsObj[i].GetComponent<LightningBolt>().Draw();
        }
      
	}

    public void RideTheLightning(Vector3 pos1, Vector3 pos2)
    {
        Vector2 pos1_2D = new Vector2(pos1.x, pos1.z);
        Vector2 pos2_2D = new Vector2(pos2.x, pos2.z);
        CreatePooledBolt(pos1_2D, pos2_2D, Color.white, 1f);        
    }

    public void BurstTheLightning(Vector3 pos1_3D, Vector3 pos2_3D)
    {
        Vector2 pos2 = new Vector2(pos1_3D.x, pos1_3D.y);
        Vector2 pos1 = new Vector2(pos2_3D.x, pos2_3D.y);
        //get the difference between our two positions (destination - source = vector from source to destination)
        Vector2 diff = pos2 - pos1;

        //define how many bolts we want in our circle
        int boltsInBurst = 8;

        for (int i = 0; i < boltsInBurst; i++)
        {
            //rotate around the z axis to the appropriate angle
            Quaternion rot = Quaternion.AngleAxis((360f / boltsInBurst) * i, new Vector3(0, 0, 1));

            //Calculate the end position for the bolt
            Vector2 boltEnd = (Vector2)(rot * diff) + pos1;

            //create a (pooled) bolt from pos1 to boltEnd
            CreatePooledBolt(pos1, boltEnd, Color.white, 1f);
        }
    }

    //calculate distance squared (no square root = performance boost)
    public float DistanceSquared(Vector2 a, Vector2 b)
    {
        return ((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
    }

    public void SetMode(Mode mode)
    {
        currentMode = mode;
    }

    void CreatePooledBolt(Vector2 source, Vector2 dest, Color color, float thickness)
    {
        //if there is an inactive bolt to pull from the pool
        if (inactiveBoltsObj.Count > 0)
        {
            //pull the GameObject
            GameObject boltObj = inactiveBoltsObj[inactiveBoltsObj.Count - 1];

            //set it active
            boltObj.SetActive(true);

            //move it to the active list
            activeBoltsObj.Add(boltObj);
            inactiveBoltsObj.RemoveAt(inactiveBoltsObj.Count - 1);

            //get the bolt component
            LightningBolt boltComponent = boltObj.GetComponent<LightningBolt>();

            //activate the bolt using the given position data
            boltComponent.ActivateBolt(source, dest, color, thickness);
        }
    }

 
}

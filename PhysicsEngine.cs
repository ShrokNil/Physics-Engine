using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PhysicsEngine : MonoBehaviour {
    // Types All start uppercase while instances start with lower case letters and have uppercase second words.
    public Vector3 velocityVector;  // m s ^-1
    public Vector3 netForceVector;  // N [kg = m s^-2]
    public float mass; // kg

   
    private List<Vector3> forceVectorList = new List<Vector3>();
    private PhysicsEngine[] physicsEngineArray;

    // e = 10^n used in floats
    private const float bigG = 6.674e-11f; //[m^3 s^−2 kg^−1]           //Old bigG = 6.673e-11f;
    // Use this for initialization

    void Start ()
    {
        SetupThrustTrails();
        physicsEngineArray = GameObject.FindObjectsOfType<PhysicsEngine>(); //Uses find ObjectsOftype not ObjectOfType because it's searching through an array and those contain more than one object.

    }

    void FixedUpdate() {
        RenderTrails();
        CalculateGravity();
        UpadatePosition();

    }

    public void AddForce(Vector3 forceVector)
    {
        forceVectorList.Add(forceVector);
    }

    void CalculateGravity()
    {
        foreach (PhysicsEngine physicsEngineA in physicsEngineArray) {
            foreach (PhysicsEngine physicsEngineB in physicsEngineArray) { 
                if (physicsEngineA != physicsEngineB) {
                    
                        Debug.Log("Calculating force exerted on " + physicsEngineA.name
                            + " due to the gravity of " + physicsEngineB.name);
                    float rSquared = Mathf.Pow

                }
            }
        }
    }
  
    

            
   
       
            
   
   
    void UpadatePosition()
    {
        //Sum the forces and clear the list
        netForceVector = Vector3.zero;

        foreach (Vector3 forceVector in forceVectorList)
        {
            netForceVector += netForceVector = forceVector; //f = m*a
        }

        forceVectorList = new List<Vector3>(); //Clear the list

        {
            //Calculate position change due to net force
            Vector3 accelerationVector = netForceVector / mass;  //a = f/m
            velocityVector += accelerationVector * Time.deltaTime;
            transform.position += velocityVector * Time.deltaTime;
        }
    }

    public bool showTrails = true; //Show Trails Checkbox

    /// <summary>
    /// Code for drawing thrust trails
    /// </summary>
    private LineRenderer lineRenderer;
    private int numberOfForces;

  
    void SetupThrustTrails()
    {
        forceVectorList = GetComponent<PhysicsEngine>().forceVectorList;

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
        lineRenderer.startWidth = 0.2F;
        lineRenderer.endWidth = 0.2F;
        lineRenderer.useWorldSpace = false;
    }

    
    void RenderTrails()
    {
        if (showTrails)
        {
            lineRenderer.enabled = true;
            numberOfForces = forceVectorList.Count;
            lineRenderer.positionCount = (numberOfForces * 2);
            int i = 0;
            foreach (Vector3 forceVector in forceVectorList)
            {
                lineRenderer.SetPosition(i, Vector3.zero);
                lineRenderer.SetPosition(i + 1, -forceVector);
                i = i + 2;
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
}



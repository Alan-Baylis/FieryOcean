using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Forge3D;
using System;

//Requires the LineRenderer component
[RequireComponent(typeof(LineRenderer))]

public class TrajectoryPredictor3D : MonoBehaviour
{
    //Velocity that the object will be shot at
    public float velocity;
    //Amount of time between each point
    public float timeBetweenPoints = 0.01f;
    //Max number of points allowed in the trajectory line/Max amount of texture objects allowed
    public int maxNumberOfPoints = 100;
    //Object that will be instantiated
    public GameObject textureObject;
    public GameObject crosshair;
    //If the the point of arc number modulo of this variable equals 0 a texture will be placed on that point
    public int textureObjectDivisor = 1;
    //The acceleration of gravity. It is automatically set to the acceleration of the earth's gravity.
    public Vector3 gravity = new Vector3(0, /*-Physics.gravity.y*/-9f, 0);
    //Layers that will stop the trajectory
    public LayerMask[] layersToHit;
    //Is the projectile being fired out of the xAxis?
    public bool xAxisForward = false;
    // From this obj we get velocity and angel for firing
    public F3DTurret f3dturret;
    //
    public Transform anchorFireTransform;
    //An internal variable that controlles whether or not the
    internal bool hasFired = false;

    //The vertical velocity of the object
    float verticalVelocity;
    //The initial horizontal velocity of the object
    float horizontalVelocity;
    //Velocity in the z direction
    float depthVelocity;

    //The y displacement of the object
    float yDisplacement;
    //The x displacement of the object
    float xDisplacement;
    //Object displacement in the z direction
    float zDisplacement;

    //The angle of launch
    float angle;
    //Another angle of launcher. Two angles are needed for 3D prediction
    float phi;

    //The line renderer component
    LineRenderer lineRenderer;
    //Vector point of the next point in the trajectory
    Vector3 vector;
    Vector3 lastVector;
    //Array of objectTextures
    GameObject[] objectPoints;
    //Simple integer used for for-loops
    int x;
    //Used for time variable in Kinematic equation
    float i;
    //Holds all of the "Texture Objects"
    GameObject textureObjectsHolder;
    Transform crosshairTransform;
    public Transform barrel;

    private const int SIZE = 2;
    void OnValidate()
    {
        if (layersToHit.Length != SIZE)
        {
            Array.Resize(ref layersToHit, SIZE);
        }
    }


    // Use this for initialization
    void Start()
    {
        //velocity = 25f;
        if (f3dturret == null)
            throw new NullReferenceException("F3DTurrent not set for TrajectoryPredictor3D");

        //Gets the line renderer component
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;

        if (crosshair != null)
            crosshairTransform = crosshair.GetComponent<Transform>();

        //Generates an empty game Object to hold the "Texture Objects" if need be
        if (textureObject != null)
        {

            if (GameObject.Find("TextureObjectsHolder(Trajectory)") == null)
            {
                textureObjectsHolder = new GameObject("TextureObjectsHolder(Trajectory)");
            }
            else
            {
                textureObjectsHolder = GameObject.Find("TextureObjectsHolder(Trajectory)");
            }

            //Checks if there is a texture object and if so instantiates them into the objectPoints array
            if (maxNumberOfPoints >= 0)
            {
                objectPoints = new GameObject[maxNumberOfPoints];

                for (x = 0; x < maxNumberOfPoints; x++)
                {

                    objectPoints[x] = (GameObject)Instantiate(textureObject);

                    objectPoints[x].transform.parent = textureObjectsHolder.transform;

                    objectPoints[x].active = false;
                }
            }
            else
            {
                throw new System.OverflowException("Cannot use a negative number in the 'Max Number Of Points' parameter of the Trajectory Predictor script!");
            }
        }
    }

    Func<LayerMask[],Vector3, bool> ChkLayers = (LayerMask[] ls, Vector3 v) => { foreach (LayerMask l in ls) { if(Physics.CheckSphere(v, 0, l)) return true; } return false; };

    // Update is called once per frame
    void Update()
    {
        //Sets "angle" and "phi" to the Euler equivalent of the object's rotation
        if (hasFired == false)
        {
            angle = -barrel.rotation.eulerAngles.x; //f3dturret.angel;
            //barrel.rotation.eulerAngles.x;
            phi = transform.rotation.eulerAngles.y;
        }

        //If there is a texture object set all objects in the objectPoints array to false
        if (textureObject != null)
        {
            for (x = 0; x < maxNumberOfPoints; x++)
            {
                objectPoints[x].active = false;
            }
        }

        velocity = f3dturret.bullet_game_speed;
        verticalVelocity = velocity * Mathf.Sin(angle * Mathf.Deg2Rad);

        //Gets the horizontal velocity of the object
        horizontalVelocity = velocity * Mathf.Cos(angle * Mathf.Deg2Rad) * Mathf.Sin((phi+90f) * Mathf.Deg2Rad);
        depthVelocity = velocity * Mathf.Sin(phi * Mathf.Deg2Rad);

        if (timeBetweenPoints != 0)
        {
            //An integer that records what line number is currently being operated on
            int lineIndex = 0;

            //Sets the line renderer line count to the maxNumberOfPoints
            if (textureObject == null)
            {
                lineRenderer.SetVertexCount(maxNumberOfPoints);
            }

            i = 0;

            vector = anchorFireTransform.position;

            //Makes sure the line Index does not exceed the maxNumberOfPoints
            while (lineIndex < maxNumberOfPoints)
            {
                //Makes sure the current vector point is not intersecting an object with one of the layersToHit layer
                if (ChkLayers(layersToHit,vector)==false)
                {
                    if (crosshair != null)
                    {
                        //crosshair.GetComponent<Renderer>().enabled = false;
                    }

                    lastVector = vector;

                    //Iterates i if lineIndex is more than 0 so the
                    if (lineIndex > 0)
                    {
                        i += timeBetweenPoints;

                        //Sets the y displacement to the kinematic equation including the vertical velocity component								
                        yDisplacement = (float)(verticalVelocity * i + 0.5f * gravity.y * (i * i)) + anchorFireTransform.position.y;
                        //Sets the x displacement to the kinematic equation including the horizontal velocity component								
                        zDisplacement = horizontalVelocity * i + anchorFireTransform.position.z;
                        xDisplacement = depthVelocity * i + anchorFireTransform.position.x;
                        
                        //Creats a point using the x and y displacement and the zDepth
                        vector = new Vector3(xDisplacement, yDisplacement, zDisplacement);
                    }

                    //Makes sure the texture object isn't null
                    if (textureObject != null)
                    {
                        //Checks if lineIndex divided by textureObjectDivisor has a remainder
                        if (textureObjectDivisor != 0)
                        {
                            if (lineIndex % textureObjectDivisor == 0)
                            {
                                //Turns one of the objectPoints on
                                objectPoints[lineIndex].active = true;
                                //Sets the position of the activated to the vector point
                                objectPoints[lineIndex].transform.position = vector;
                            }
                        }
                        else
                        {
                            throw new System.DivideByZeroException("The 'Texture Object Divisor' parameter cannot be 0 in the 'Trajectory Predictor' script");
                        }
                    }
                    else
                    {
                        //Sets a line renderer line to the position of the vector point
                        lineRenderer.SetPosition(lineIndex, vector);
                    }

                    //Increments the lineIndex variable by 1
                    lineIndex++;
                }
                else
                {
                    if (crosshair != null)
                    {
                        //crosshair.GetComponent<Renderer>().enabled = true;

                        RaycastHit hit;
                        Physics.Linecast(lastVector, vector, out hit);
                        crosshairTransform.position = new Vector3(hit.point.x, crosshairTransform.position.y, hit.point.z);
                        Debug.Log("Projector: " + crosshairTransform.position.ToString());
                        //crosshairTransform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal); ;
                    }

                    break;
                }
            }
           
            //string layerName = LayerMask.LayerToName(layersToHit);
            /*if (chk)
                Debug.Log(" Trajectory prediction 0: " + layersToHit.value.ToString());
            if(chk1)
                Debug.Log(" Trajectory prediction 1: " + layersToHit1.value.ToString());*/
        

            if (textureObject == null)
            {
                //Sets the number of lines in the line renderer to lineIndex
                lineRenderer.SetVertexCount(lineIndex);
            }
        }
    }
}
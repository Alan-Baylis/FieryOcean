using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Forge3D;
using System;


//Requires the LineRenderer component
[RequireComponent(typeof(LineRenderer))]

public class TrajectoryPredictor3D : MonoBehaviour
{
    public struct ProjectorColors { public Color NotSelect; public Color Aiming; public Color Aimed; public Color AimImpossible; }

    //Amount of time between each point
    public float timeBetweenPoints = 0.1f;
    //Max number of points allowed in the trajectory line/Max amount of texture objects allowed
    public int maxNumberOfPoints = 80;
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
    public F3DTurret turret;
    //
    public Transform anchorFireTransform;

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
    Vector3 curVector;
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
    Projector _projector;
    Func<LayerMask[], Vector3, bool> ChkLayers = (LayerMask[] ls, Vector3 v) => { foreach (LayerMask l in ls) { if (Physics.CheckSphere(v, 0, l)) return false; } return true; };

    public ProjectorColors projectorColors { set; get; }

    private const int SIZE = 2;
    void OnValidate()
    {
        if (layersToHit.Length != SIZE)
        {
            Array.Resize(ref layersToHit, SIZE);
        }
    }

    // Use this for initialization
    public void CustomStart()
    {
        //velocity = 25f;
        if (turret == null)
            throw new NullReferenceException("F3DTurrent not set for TrajectoryPredictor3D");

        //Gets the line renderer component
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;

        if (crosshair != null)
        {
            crosshairTransform = crosshair.GetComponent<Transform>();
            _projector = crosshair.GetComponent<Projector>();
            //Material mat = Resources.Load("Projector/Material/MatAimCrossNotSelect", typeof(Material)) as Material;
            //pr.material = mat;
            _projector.material.SetColor("_Color", projectorColors.NotSelect);
            _curAimColor = projectorColors.NotSelect;
        }
        else
            throw new NullReferenceException("Projector in trajectory prediction is not set");

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


    /// <summary>
    /// Creates a trajectory with the help of objects lineRenders based on the specified angle and velocity of the projectile 
    /// </summary>
    public void TrajectoryPredict()
    {
        //Sets "angle" and "phi" to the Euler equivalent of the object's rotation
        angle = -barrel.rotation.eulerAngles.x; //f3dturret.angel;
        //barrel.rotation.eulerAngles.x;
        phi = transform.rotation.eulerAngles.y;

        //verticalVelocity = -turret.cannonParams.vY;
        verticalVelocity = turret.speed* Mathf.Sin(angle * Mathf.Deg2Rad);
        //Gets the horizontal velocity of the object
        //horizontalVelocity = turret.cannonParams.vX * Mathf.Sin((phi + 90f) * Mathf.Deg2Rad);
        horizontalVelocity = turret.speed * Mathf.Cos(angle * Mathf.Deg2Rad) * Mathf.Sin((phi + 90f) * Mathf.Deg2Rad);

        phi += 90;
        //Uses Pythagorean's theorem to get the velocity in the z direction
        if (((phi - 90 > 180 && xAxisForward == true) || ((phi < 90 || phi > 270) && xAxisForward == false)) && turret.speed != verticalVelocity)
        {
            depthVelocity = Mathf.Sqrt((turret.speed * turret.speed) - (horizontalVelocity * horizontalVelocity) - (verticalVelocity * verticalVelocity));
        }
        else if (turret.speed != verticalVelocity)
        {
            depthVelocity = -Mathf.Sqrt((turret.speed * turret.speed) - (horizontalVelocity * horizontalVelocity) - (verticalVelocity * verticalVelocity));
        }
        else {
            depthVelocity = 0;
        }

        depthVelocity = -depthVelocity;

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

            curVector = anchorFireTransform.position;

            //Makes sure the line Index does not exceed the maxNumberOfPoints
            while (lineIndex < maxNumberOfPoints)
            {
                //Makes sure the current vector point is not intersecting an object with one of the layersToHit layer
                if (ChkLayers(layersToHit, curVector))
                {
                    lastVector = curVector;

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
                        curVector = new Vector3(xDisplacement, yDisplacement, zDisplacement);
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
                                objectPoints[lineIndex].transform.position = curVector;
                            }
                        }
                        else
                            throw new System.DivideByZeroException("The 'Texture Object Divisor' parameter cannot be 0 in the 'Trajectory Predictor' script");
                    }
                    else
                    {
                        //Sets a line renderer line to the position of the vector point
                        lineRenderer.SetPosition(lineIndex, curVector);
                    }

                    //Increments the lineIndex variable by 1
                    lineIndex++;
                }
                else
                {
                    RaycastHit hit;
                    zDisplacement = horizontalVelocity * treajectoryPredictorCorrection;
                    xDisplacement = depthVelocity * treajectoryPredictorCorrection;

                    Physics.Linecast(lastVector, new Vector3(curVector.x + xDisplacement, curVector.y, curVector.z + zDisplacement), out hit);

                    lastPosition = new Vector3(hit.point.x, crosshairTransform.position.y, hit.point.z);

                    crosshairTransform.position = Vector3.SmoothDamp(crosshairTransform.position, lastPosition, ref _velocity, smoothTime);

                    if (!AlmostEqual(crosshairTransform.position,lastPosition, 0.2f))
                    {
                        SetProjectorColor(_curAimColor, projectorColors.Aiming, _projector.material);
                    }
                    else
                    {
                        if (AlmostEqual(turret.GetTarget(), hit.point, aimingError))
                            SetProjectorColor(_curAimColor, projectorColors.Aimed, _projector.material);
                        else
                            SetProjectorColor(_curAimColor, projectorColors.AimImpossible, _projector.material);
                    }

                    break;
                }
            }
           
            if (textureObject == null)
            {
                //Sets the number of lines in the line renderer to lineIndex
                lineRenderer.SetVertexCount(lineIndex);
            }
        }
    }

    /// <summary>
    /// Change the color of the projector if it does not match the required
    /// </summary>
    Action<Color , Color, Material> SetProjectorColor = (Color cur, Color c, Material mat) => { if (cur != c) mat.SetColor("_Color", c); };

    public float smoothTime = 0.3f;
    public float treajectoryPredictorCorrection = 12f;
    public float aimingError = 3;

    Color _curAimColor;
    Vector3 lastPosition;
    Vector3 _velocity;

    public bool AlmostEqual(Vector3 v1, Vector3 v2, float precision)
    {
        bool equal = true;

        if (Mathf.Abs(v1.x - v2.x) > precision) equal = false;
        if (Mathf.Abs(v1.y - v2.y) > precision) equal = false;
        if (Mathf.Abs(v1.z - v2.z) > precision) equal = false;

        return equal;
    }
}
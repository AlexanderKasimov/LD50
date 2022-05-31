using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RopesGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public Transform RopeStart;
    [SerializeField] public Transform RopeEnd;

    [SerializeField] public Transform Handle1;
    [SerializeField] public Transform Handle2;

    [SerializeField] public int RopePointsCount;
    [SerializeField] public bool looseEnd = false;
    //Test bezier
    //[SerializeField] [Range(0,1)] public float testTime;

    //lists of components that will change on Update
    private List<HingeJoint2D> jointsList = new List<HingeJoint2D>();
    private List<ConstantForce2D> constantForceList = new List<ConstantForce2D>();

    private LineRenderer lr;
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();

        //Create LineRender points
        lr.positionCount = RopePointsCount;

        //Set start point
        lr.SetPosition(0, RopeStart.position);


        GameObject gameObjectOld = new GameObject();

        //Initialize start rb2 (hook1)
        gameObjectOld.transform.position = GetBezierPoint(0);
        gameObjectOld.transform.SetParent(transform);
        gameObjectOld.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;


        //Create middle points
        for (int i = 0; i < RopePointsCount; i++)
        {
            //Initialize linerender points for static lines
            //lr.SetPosition(i, GetBezierPoint((float)i / RopePointsCount));

            //Initialize hinges
            GameObject gameObject = new GameObject();
            gameObject.transform.position = GetBezierPoint((float)i / (RopePointsCount-1));
            gameObject.transform.SetParent(transform);

            HingeJoint2D hingeJoint2D = gameObject.AddComponent<HingeJoint2D>();
            jointsList.Add(hingeJoint2D);
            hingeJoint2D.connectedBody = gameObjectOld?.GetComponent<Rigidbody2D>();

            //Set limits (first hinge and last one are limitless)
            if (i != 0 && i != RopePointsCount -1)
            {
                JointAngleLimits2D limits = hingeJoint2D.limits;
                limits.min = -30;
                limits.max = 30;
                hingeJoint2D.limits = limits;
                hingeJoint2D.useLimits = true;
            }

            //set previous body
            gameObjectOld = gameObject;

            //Add constant force component to hinge object and list
            constantForceList.Add(gameObjectOld.AddComponent<ConstantForce2D>());
        }

        //freeze end rb (hook2)
        if (!looseEnd)
        {
            gameObjectOld.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }

        //Set end point
        lr.SetPosition(RopePointsCount - 1, RopeEnd.position);

    }

    void Start()
    {

    }

    private void OnDrawGizmos()
    {
        //Debug bezier
        if (lr)
        {
            for (int i = 0; i < lr.positionCount; i++)
            {
                Gizmos.DrawSphere(lr.GetPosition(i), 0.1f);
            }
        }

        //Gizmos.DrawSphere(RopeStart.position, 0.1f);
        //Gizmos.DrawSphere(RopeEnd.position, 0.1f);
        //Gizmos.color = Color.blue;
        //Gizmos.DrawSphere(Handle1.position, 0.1f);
        //Gizmos.DrawSphere(Handle2.position, 0.1f);

        /*Test bezier
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(GetBezierPoint(testTime), 0.1f);
        */

        //Gizmos.color = Color.white;

        Gizmos.DrawLine(RopeStart.position, Handle1.position);
        Gizmos.DrawLine(RopeEnd.position, Handle2.position);

        Handles.DrawBezier(RopeStart.position, RopeEnd.position, Handle1.position, Handle2.position, Color.white, EditorGUIUtility.whiteTexture, 1f);
    }

    private Vector3 GetBezierPoint(float t)
    {
        Vector3 startPoint = RopeStart.position;
        Vector3 endPoint = RopeEnd.position;
        Vector3 startTangent = Handle1.position;
        Vector3 endTangent = Handle2.position;

        Vector3 a = Vector3.Lerp(startPoint, startTangent, t);
        Vector3 b = Vector3.Lerp(startTangent, endTangent, t);
        Vector3 c = Vector3.Lerp(endTangent, endPoint, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        //forward vector (not needed rn)
        //Quaternion forwardVector = Quaternion.LookRotation(d - e).normalized;
        return Vector3.Lerp(d, e, t);
    }

    [SerializeField] public Vector3 windDirection;
    [SerializeField] public float windPower;
    [SerializeField] public float noiseFrequency = 1;
    void FixedUpdate()
    {
        int i = 0;
        foreach (var item in jointsList)
        {
            lr.SetPosition(i, item.gameObject.transform.position);
            //constantForceList[i].force = windDirection.normalized * windPower * (Mathf.PerlinNoise(0, Time.time * noiseFrequency) * 2 - 1);
            constantForceList[i].force = windDirection.normalized * windPower * Mathf.Abs((Mathf.PerlinNoise(0, Time.time * noiseFrequency) * 2 - 1));
            i++;
        }
    }
}

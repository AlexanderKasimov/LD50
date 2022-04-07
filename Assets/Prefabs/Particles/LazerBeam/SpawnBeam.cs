using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBeam : MonoBehaviour
{
    public Vector3 Point1;
    public Vector3 Point2;
    TrailRenderer tr;

    [SerializeField]
    public float time;

    // Start is called before the first frame update
    void Awake(){
        transform.position = Point1;
        tr = GetComponent<TrailRenderer>();
        time = 0f;
    }

    void Start()
    {
        transform.position = Point2;
    }

    // Update is called once per frame
    void Update()
    {   
        if (time < tr.time/2)
        {
            time += Time.deltaTime;
            tr.material.SetFloat("_BeamWidth", Mathf.Lerp(0,10, time/tr.time));
        }
        if (time > tr.time/2 && time < tr.time)
        {
            time += Time.deltaTime;
            tr.material.SetFloat("_BeamWidth", Mathf.Lerp(10,0, time/tr.time));
             tr.material.SetFloat("_BeamDissapear", Mathf.Lerp(0,1, time/tr.time));
        }
    }
}

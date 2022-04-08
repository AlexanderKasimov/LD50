using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnNumber : MonoBehaviour
{
    public GameObject textWidget;

    public Vector2 startVelociy;
    public Vector2 Acceleration;
    public Vector2 velocity;
    public float lifetime=1;
    public string text;
    // Start is called before the first frame update
    void Awake(){
        
    }
    public void InitializeParticle(Vector2 startLocationIn, Vector2 startVelocityIn, float lifetimeIn, string textIn)
    {
        textWidget.transform.position = startLocationIn;
        startVelociy = startVelocityIn;
        velocity = startVelocityIn;
        lifetime = lifetimeIn;
        Destroy(gameObject, lifetime);
        textWidget.GetComponentInChildren<Text>().text = textIn;
    }

    // Update is called once per frame
     void Update()
     {
        velocity += Acceleration;
        textWidget.transform.position = textWidget.transform.position + (Vector3)velocity*Time.deltaTime;
     }
}

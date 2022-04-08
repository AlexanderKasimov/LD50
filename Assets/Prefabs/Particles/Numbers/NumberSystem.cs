using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberSystem : MonoBehaviour
{
    public GameObject number;

    void Start(){
        StartCoroutine("Spawn");
    }
    
    // Start is called before the first frame update

    private SpawnNumber tmpspawnNumber;
    IEnumerator Spawn(){
        GameObject tmp = Instantiate(number, transform.position, Quaternion.identity);
        
        tmpspawnNumber = tmp.GetComponent<SpawnNumber>();
        tmpspawnNumber.InitializeParticle(transform.position, new Vector2(Random.Range(-2f,2f), Random.Range(2f,4f)), 1f, Random.Range(100, 999).ToString());

        yield return new WaitForSeconds(0.1f);
        StartCoroutine("Spawn");
    }
}

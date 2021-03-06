using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField]
    private ScriptableObjectAnimCurve curve;

    private SpriteRenderer[] spriteRenderers;

    [SerializeField]
    private float duration = 1f;

    private bool isPlaying = false;

    private void Awake()
    {
              
    }

    public void PlayEffect()
    {
        if (isPlaying)
        {
            return;
        }
        isPlaying = true;
        StartCoroutine("PlayEffectCoroutine");
    }


    private IEnumerator PlayEffectCoroutine()
    {
        //Have to set every time cause characters spawn weapons -> to not add dependency character -> hitEffect
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        // Debug.Log("Start playing");
        float time = 0f;
        while (time < duration)
        {
            float parameterValue = curve.animationCurve.Evaluate(time / duration);
            foreach (var item in spriteRenderers)
            {   
                //When weapon changes while being hit -> can't finish coroutine -> isPlaying doesn't reset
                if (!item)
                {
                    Debug.Log("No item!");
                    continue;
                }
                item.material.SetFloat("_Control", parameterValue);
                // Debug.Log("Setted:" +parameterValue);
            }
            time += Time.deltaTime;
            yield return null;
        }
        //For safety - reset all SRs parameters to default - 0f
        foreach (var item in spriteRenderers)
        {
            if (!item)
            {
                continue;
            }
            item.material.SetFloat("Control", 0f);
        }
    
        isPlaying = false;

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

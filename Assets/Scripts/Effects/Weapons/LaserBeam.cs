using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{

    private LineRenderer lineRenderer;

    [SerializeField]
    private float duration = 1f;

    private float time = 0f;

    [SerializeField]
    private ScriptableObjectAnimCurve widthMutliplierCurve;

    [SerializeField]
    private float width = 0.4f;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Init(Vector2 startPos, Vector2 endPos)
    {
        Vector3[] positions = { startPos, endPos };
        lineRenderer.SetPositions(positions);
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= duration)
        {
            Destroy(gameObject);
            return;
        }
        float widthMultiplier = widthMutliplierCurve.animationCurve.Evaluate(time / duration);
        lineRenderer.widthMultiplier = width * widthMultiplier;
        time += Time.deltaTime;
    }
}

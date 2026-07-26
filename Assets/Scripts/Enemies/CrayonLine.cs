using System.Collections.Generic;
using UnityEngine;

public class CrayonLine : MonoBehaviour
{
    public GameObject crayon;
    private float lastLineUpdate;
    private List<Vector2> linePoints;
    public float lineUpdateInterval;

    void Start()
    {
        linePoints = new List<Vector2>();

        for (int i = 0; i < GetComponent<LineRenderer>().positionCount; i++)
        {
            GetComponent<LineRenderer>().SetPosition(i, new(crayon.transform.position.x, crayon.transform.position.y - 0.95f));
            linePoints.Add(GetComponent<LineRenderer>().GetPosition(i));
        }
        GetComponent<EdgeCollider2D>().SetPoints(linePoints);
    }

    void Update()
    {
        if (Time.time - lastLineUpdate > lineUpdateInterval)
        {
            lastLineUpdate = Time.time;
            if (crayon)
            {
                linePoints.Add(new(crayon.transform.position.x, crayon.transform.position.y - 0.95f));
                linePoints.RemoveAt(0);
                for (int i = 0; i < linePoints.Count; i++)
                {
                    GetComponent<LineRenderer>().SetPosition(i, linePoints[i]);
                }
                GetComponent<EdgeCollider2D>().SetPoints(linePoints);
            }
            else
            {
                if (linePoints.Count > 1)
                {
                    linePoints.RemoveAt(0);
                    GetComponent<LineRenderer>().positionCount--;
                    GetComponent<EdgeCollider2D>().SetPoints(linePoints);
                    for (int i = 0; i < linePoints.Count; i++)
                    {
                        GetComponent<LineRenderer>().SetPosition(i, linePoints[i]);
                    }
                    GetComponent<EdgeCollider2D>().SetPoints(linePoints);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}

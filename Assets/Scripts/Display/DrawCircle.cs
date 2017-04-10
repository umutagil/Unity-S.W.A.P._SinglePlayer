using UnityEngine;
using System.Collections;

// Draws a colored circle under the shooter
public class DrawCircle : MonoBehaviour {

    public Color circleColor;
    public float radCircle;
    LineRenderer lr;
    private float theta_scale = 0.05f;
    int size;

	void Start () {
        float sizeValue = (2.0f * Mathf.PI) / theta_scale;
        size = (int)sizeValue;
        size++;        

        lr = gameObject.AddComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Additive"));
        lr.startColor = circleColor;
        lr.endColor = circleColor;
        lr.startWidth = 0.2f;
        lr.endWidth = 0.2f;
        lr.numPositions = size;
	}
		
	void Update () {        

        float theta = 0f;        
        for (int i = 0; i < size; i++)
        {
            theta += (2.0f * Mathf.PI * theta_scale);
            // Calculate position of point
            float x = radCircle * Mathf.Cos(theta);
            float z = radCircle * Mathf.Sin(theta);
            x += gameObject.transform.position.x;
            z += gameObject.transform.position.z;

            // Set the position of this point
            Vector3 pos = new Vector3(x, 0.1f, z);
            lr.SetPosition(i, pos);            
        }

	}
}

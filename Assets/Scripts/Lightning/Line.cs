using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour
{
	public Vector2 A; //Start
	public Vector2 B; //End
	public float Thickness; //Thickness of line

	//Children that contain the pieces that make up the line
	public GameObject StartCapChild, LineChild, EndCapChild;

	//Create a new line
	public Line(Vector2 a, Vector2 b, float thickness)
	{
		A = a;
		B = b;
		Thickness = thickness;
	}

	//Used to set the color of the line
	public void SetColor(Color color)
	{
		StartCapChild.GetComponent<SpriteRenderer>().color = color;
		LineChild.GetComponent<SpriteRenderer>().color = color;
		EndCapChild.GetComponent<SpriteRenderer>().color = color;
	}

	//Will actually draw the line
	public void Draw()
	{          
		Vector2 difference = B - A;       

		float rotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

		//Set the scale of the line to reflect length and thickness
		LineChild.transform.localScale = new Vector3(100 * (difference.magnitude / LineChild.GetComponent<SpriteRenderer>().sprite.rect.width),
                                                      LineChild.transform.localScale.y, Thickness);

        StartCapChild.transform.localScale = new Vector3(StartCapChild.transform.localScale.x, StartCapChild.transform.localScale.y, Thickness);

        EndCapChild.transform.localScale = new Vector3(EndCapChild.transform.localScale.x, EndCapChild.transform.localScale.y, Thickness);

		//Rotate the line so that it is facing the right direction        
        float extraRotate = (rotation < 0f ? -90f : 90f);
        extraRotate = 180f - 2 * rotation;   

        LineChild.transform.rotation = Quaternion.Euler(new Vector3(-90f, rotation + extraRotate, 0f));
        StartCapChild.transform.rotation = Quaternion.Euler(new Vector3(-90f, rotation + extraRotate, 0f));
        EndCapChild.transform.rotation = Quaternion.Euler(new Vector3(-90f, rotation + 180 + extraRotate, 0f));

		//Move the line to be centered on the starting point
        LineChild.transform.position = new Vector3(A.x, 1f, A.y);
        StartCapChild.transform.position = new Vector3(A.x, 1f, A.y);
        EndCapChild.transform.position = new Vector3(A.x, 1f, A.y);
        

		//Need to convert rotation to radians at this point for Cos/Sin
		rotation *= Mathf.Deg2Rad;

		//Store these so we only have to access once
		float lineChildWorldAdjust = LineChild.transform.localScale.x * LineChild.GetComponent<SpriteRenderer>().sprite.rect.width / 2f;
		float startCapChildWorldAdjust = StartCapChild.transform.localScale.x * StartCapChild.GetComponent<SpriteRenderer>().sprite.rect.width / 2f;
		float endCapChildWorldAdjust = EndCapChild.transform.localScale.x * EndCapChild.GetComponent<SpriteRenderer>().sprite.rect.width / 2f;

		//Adjust the middle segment to the appropriate position
        LineChild.transform.position += new Vector3(.01f * Mathf.Cos(rotation) * lineChildWorldAdjust,
                                                     0f, .01f * Mathf.Sin(rotation) * lineChildWorldAdjust);

        //Adjust the start cap to the appropriate position
        StartCapChild.transform.position -= new Vector3(.01f * Mathf.Cos(rotation) * startCapChildWorldAdjust, 0f,
                                                         .01f * Mathf.Sin(rotation) * startCapChildWorldAdjust);

        //Adjust the end cap to the appropriate position
        EndCapChild.transform.position += new Vector3(.01f * Mathf.Cos(rotation) * lineChildWorldAdjust * 2, 0f,
                                                       .01f * Mathf.Sin(rotation) * lineChildWorldAdjust * 2);

        EndCapChild.transform.position += new Vector3(.01f * Mathf.Cos(rotation) * endCapChildWorldAdjust, 0f,
                                                       .01f * Mathf.Sin(rotation) * endCapChildWorldAdjust);
        

	}
}
using UnityEngine;
using System.Collections;

public class GameDisplayController : MonoBehaviour {

    public Texture2D yourCursor;  // Your cursor texture
    public int cursorSizeX = 16;  // Your cursor size x
    public int cursorSizeY = 16;  // Your cursor size y

	// Use this for initialization
	void Awake () {        
        Cursor.visible = false;
	}

    void OnGUI()
    {       
        GUI.DrawTexture(new Rect(Event.current.mousePosition.x - cursorSizeX / 2, Event.current.mousePosition.y - cursorSizeY / 2, cursorSizeX, cursorSizeY), yourCursor);
    }
}

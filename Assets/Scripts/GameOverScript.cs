using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

    private GUISkin skin;

    void Start()
    {
        //skin = Resources.Load("GUISkin-Death") as GUISkin;
    }

    void OnGUI()
    {
        const int buttonWidth = 200;
        const int buttonHeight = 100;

        //GUI.skin = skin;

        if (
          GUI.Button(
            // Center in X, 1/3 of the height in Y
            new Rect(
              Screen.width / 2 - (buttonWidth / 2),
              (1 * Screen.height / 3) - (buttonHeight / 2),
              buttonWidth,
              buttonHeight
            ),
            "Retry!"
          )
        )
        {
            // Reload the level
            Application.LoadLevel(Application.loadedLevel);
        }

        else if (
          GUI.Button(
            // Center in X, 2/3 of the height in Y
            new Rect(
              Screen.width / 2 - (buttonWidth / 2),
              (2 * Screen.height / 3) - (buttonHeight / 2),
              buttonWidth,
              buttonHeight
            ),
            "Quit"
          )
        )
        {
            // Reload the level
            Application.Quit();
        }
    }
}

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TestGuiScaling : MonoBehaviour 
{
    public GUISkin Skin;

	// Use this for initialization
	void Start () 
    {
        DebugConsole.IsOpen = true;
        DebugConsole.Log("this is a log message");
        DebugConsole.LogError("this is a error log message");
        DebugConsole.LogWarning("this is a warning log message");

        GuiScalerByDpi.Initialize();
	}

    void OnGUI()
    {
        GUI.skin = Skin;

        // by dpi
        GuiScalerByDpi.Begin();
        GUI.RepeatButton(new Rect(50, 10, 200, 35), "scaled by dip 1");
        GUI.RepeatButton(new Rect(50, 60, 200, 35), "scaled by dpi 2");
        GuiScalerByDpi.End();


        // by target, ratio sbagliato
        GuiScalerByTarget.AutoResize(800, 480);
        GUI.RepeatButton(new Rect(50, 100, 200, 35), "scaled by target 1");
        GUI.RepeatButton(new Rect(50, 150, 200, 35), "scaled by target 2");

        GuiScalerByTarget2.BeginGUI();
        GUI.RepeatButton(new Rect(50, 200, 200, 35), "scales by ratio 1");
        GUI.RepeatButton(new Rect(50, 250, 200, 35), "scaled by ratio 2");
        GuiScalerByTarget2.BeginGUI();

        GuiScalerByTarget3();
    }

	// Update is called once per frame
	void Update ()
	{
	}

    void GuiScalerByTarget3()
    {
        float originalWidth = 800;  // define here the original resolution
        float originalHeight = 480; // you used to create the GUI contents 
        Vector3 scale;

        /*
        scale.x = Screen.width/originalWidth; // calculate hor scale
        scale.y = Screen.height/originalHeight; // calculate vert scale
        scale.z = 1;
        var svMat = GUI.matrix; // save current matrix
        // substitute matrix - only scale is altered from standard
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
        // draw your GUI controls here:
        GUI.RepeatButton(new Rect(50, 300, 200, 35), "scales by matrix 1");
        GUI.RepeatButton(new Rect(50, 350, 200, 35), "scaled by matrix 2");
        // restore matrix before returning
        GUI.matrix = svMat; // restore matrix
        */

        scale.y = Screen.height / originalHeight; // calculate vert scale
        scale.x = scale.y; // this will keep your ratio base on Vertical scale
        scale.z = 1;
        float scaleX = Screen.width / originalWidth; // store this for translate
        var svMat = GUI.matrix; // save current matrix // substitute matrix - only scale is altered from standard
        GUI.matrix = Matrix4x4.TRS(new Vector3((scaleX - scale.y) / 2 * originalWidth, 0, 0), Quaternion.identity, scale);
        // draw your GUI controls here:
        GUI.RepeatButton(new Rect(50, 300, 200, 35), "scales by matrix 1");
        GUI.RepeatButton(new Rect(50, 350, 200, 35), "scaled by matrix 2");
        // restore matrix before returning
        GUI.matrix = svMat; // restore matrix
    }


 
}

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TestCoroutinesAndPhysics : MonoBehaviour 
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
        GuiScalerByDpi.Begin();

        GUI.skin = Skin;
       
        var state = (GUI.RepeatButton(new Rect(10, 10, 200, 35), "[f] apply instant"));
        //TestForcesScript.Button1.NewState = state;
        state = (GUI.RepeatButton(new Rect(10, 60, 200, 35), "[g] apply continuous"));
        //TestForcesScript.Button2.NewState = state;
    }

	// Update is called once per frame
	void Update ()
	{
	}
}

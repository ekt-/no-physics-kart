﻿using UnityEngine;


[ExecuteInEditMode]
public class GuiManager : MonoBehaviour 
{
    public GUISkin Skin;
    public TestAddForceMode TestForcesScript;
	
    void Start()
    {
        
    }

   
    /*
    void DoMyWindow(int windowID)
    {
        GUILayout.BeginVertical();
        GUILayout.Label("Im a Label");
        GUILayout.Space(8);
        GUILayout.Button("Im a Button");
        GUILayout.TextField("Im a textfield");
        GUILayout.TextArea("Im a textfield\nIm the second line\nIm the third line\nIm the fourth line");
        blnToggleState = GUILayout.Toggle(blnToggleState, "Im a Toggle button");
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        //Sliders
        GUILayout.BeginHorizontal();
        fltSliderValue = GUILayout.HorizontalSlider(fltSliderValue, 0.0f, 1.1f, GUILayout.Width(128));
        fltSliderValue = GUILayout.VerticalSlider(fltSliderValue, 0.0f, 1.1f, GUILayout.Height(50));
        GUILayout.EndHorizontal();
        //Scrollbars
        GUILayout.BeginHorizontal();
        fltScrollerValue = GUILayout.HorizontalScrollbar(fltScrollerValue, 0.1f, 0.0f, 1.1f, GUILayout.Width(128));
        fltScrollerValue = GUILayout.VerticalScrollbar(fltScrollerValue, 0.1f, 0.0f, 1.1f, GUILayout.Height(90));
        GUILayout.Box("Im\na\ntest\nBox");
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUI.DragWindow();
    }
     */
}

using UnityEngine;
using System.Collections;
 
[RequireComponent(typeof(GUIText))]
public class FpsCounter : MonoBehaviour 
{
	public float Frequency = 0.5f;
 
	public int FramesPerSec { get; protected set; }
 
	private void Start() 
    {
		StartCoroutine(FPS());
	}
 
	private IEnumerator FPS() 
    {
		for(;;)
        {
			// Capture frame-per-second
			int lastFrameCount = Time.frameCount;
			float lastTime = Time.realtimeSinceStartup;
			yield return new WaitForSeconds(Frequency);
			float timeSpan = Time.realtimeSinceStartup - lastTime;
			int frameCount = Time.frameCount - lastFrameCount;
 
			// Display it
			FramesPerSec = Mathf.RoundToInt(frameCount / timeSpan);
			gameObject.GetComponent<GUIText>().text = FramesPerSec.ToString() + " fps";
		}
	}
}
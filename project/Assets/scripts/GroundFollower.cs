using UnityEngine;

using System.Collections;
//http://answers.unity3d.com/questions/168097/orient-vehicle-to-ground-normal.html
//http://answers.unity3d.com/questions/196381/how-do-i-check-if-my-rigidbody-player-is-grounded.html
//http://forum.unity3d.com/threads/27824-Follow-ball-through-tube
//http://forum.unity3d.com/threads/33413-Maintain-constant-altitude-(make-object-follow-ground)
public class GroundFollower : MonoBehaviour
{
    public float forwardSpeed = 1;
    public float steerAngle = 1;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void FixedUpdate()
    {
        var x = Input.GetAxis("Horizontal");
	    var y = Input.GetAxis("Vertical");

        this.rigidbody.velocity += y * transform.forward * forwardSpeed;
        this.rigidbody.AddTorque(transform.up * x * steerAngle, ForceMode.Acceleration);
    }
}

using UnityEngine;
using System.Collections;

public class KartPrefabController : MonoBehaviour
{
    private bool m_inputUp;
    private bool m_inputDown;
    private bool m_inputLeft;
    private bool m_inputRight;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_inputUp = Input.GetKey(KeyCode.UpArrow);
        m_inputDown = Input.GetKey(KeyCode.DownArrow);
        m_inputLeft = Input.GetKey(KeyCode.LeftArrow);
        m_inputRight = Input.GetKey(KeyCode.RightArrow);
    }

    void FixedUpdate()
    {
        // TransformDirection to transform a direction from local to world space
        // InverseTransformDirection to transform a direction from world to local space
        // TransformPoint to transform a position from local to world space
        // InverseTransformPoint to transform a position from world to local space.

        // float xVel = transform.InverseTransformDirection(rigidbody.velocity).x;

        var isAccelerating = m_inputUp;
        var isBraking = !isAccelerating;

        var force = new Vector3(0, 0, 10);
        if (isAccelerating)
        {
            rigidbody.AddRelativeForce(force, ForceMode.Acceleration);    
        }
        else
        {
            var brakeForce = rigidbody.velocity * -0.1f;
            rigidbody.AddForce(brakeForce, ForceMode.Impulse);
        }

        var velocityMagnitude = rigidbody.velocity.magnitude;
        var turnVelocityThreshold = isAccelerating ? 0.5f : 10f;
        var canTurn = velocityMagnitude > turnVelocityThreshold;

        if (canTurn)
        {
            const float turnSpeed = 2f;
            if (m_inputLeft)
            {
                var turnTorque = new Vector3(0, -turnSpeed, 0);
                rigidbody.AddTorque(turnTorque);
            }
            else if (m_inputRight)
            {
                var turnTorque = new Vector3(0, turnSpeed, 0);
                rigidbody.AddTorque(turnTorque);
            }            
        }

        /*
         * b2Vec2 getLateralVelocity() 
         * {
      b2Vec2 currentRightNormal = m_body->GetWorldVector( b2Vec2(1,0) );
      return b2Dot( currentRightNormal, m_body->GetLinearVelocity() ) * currentRightNormal;
  }
         */


        // kill lateral force
        var rightNormal = transform.TransformDirection(new Vector3(1, 0, 0));
        var lateralVelocity = Vector3.Dot(rightNormal, rigidbody.velocity)*rightNormal;
        var impulse = - rigidbody.mass * lateralVelocity;
        rigidbody.AddForce(impulse, ForceMode.Impulse);


    }
}
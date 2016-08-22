using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Test per cercare di capire le differenze di ForceMode
/// </summary>
/// Differenza fra Force e Impulse
/// Una possibile spiegazione trovata è: 
/// - Impulse modifies the velocity of the body instantaneously
/// - Force modifies the velocity of the body *in terms of time passed*
/// As a result, ApplyForce has about 1/30th (or whatever your physics step is) of the apparent affect as ApplyImpulse.
/// That means applyImpulse(10) and 10 seconds applyForce(1) will result in the same velocity for the body
///  impulse is just an incredible huge force over a really short amount of time
/// Something like a player jumping is best handled as an impulse
/// A rocket pack, on the other hand, is best done as a force, because it happens over an extended time
/// <remarks>
/// La documentazione di ForceMode dice:
/// Force:	        Add a continuous force to the rigidbody, using its mass.
/// Acceleration:	Add a continuous acceleration to the rigidbody, ignoring its mass.
/// Impulse:	    Add an instant force impulse to the rigidbody, using its mass.
/// VelocityChange:	Add an instant velocity change to the rigidbody, ignoring its mass.
/// </remarks>-->
/// http://unity3d.com/learn/tutorials/modules/beginner/physics/addforce
/// 
public class TestAddForceMode : MonoBehaviour 
{
    public GUISkin Skin;

    private GameObject m_cube1;
    private GameObject m_cube2;
    private GameObject m_cube3;
    private GameObject m_cube4;

    private ButtonKeyState m_applyForceInstant;
    private ButtonKeyState m_applyForceContinuous;

    private int m_numberOfFixedUpdatesApplingForces;

    void Awake()
    {        
    }

	// Use this for initialization
	void Start ()
	{        
	    m_cube1 = GameObject.Find("Cube1");
        m_cube2 = GameObject.Find("Cube2");
        m_cube3 = GameObject.Find("Cube3");
        m_cube4 = GameObject.Find("Cube4");

	    GameObject.Find("Cube1Label").GetComponent<GUIText>().text = "ForceMode.Acceleration";
        GameObject.Find("Cube2Label").GetComponent<GUIText>().text = "ForceMode.Force";
        GameObject.Find("Cube3Label").GetComponent<GUIText>().text = "ForceMode.Impulse";
        GameObject.Find("Cube4Label").GetComponent<GUIText>().text = "ForceMode.VelocityChange";

        Debug.Log("TestAddForceMode.Start executed");
	}

    void OnGUI()
    {
        GUI.skin = Skin;

        var state = (GUI.RepeatButton(new Rect(10, 10, 200, 35), "Apply instant once"));
        m_applyForceInstant.NewState = state;

        state = (GUI.RepeatButton(new Rect(10, 60, 200, 35), "Apply continuous"));
        m_applyForceContinuous.NewState = state;
    }

	// Update is called once per frame
	void Update ()
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

    void FixedUpdate()
    {
        if (m_applyForceInstant.IsKeyDown)
        {
            Debug.Log("appling istant forces...");
            ApplyForcesOnce();
        }

        if (m_applyForceContinuous.IsKeyDown)
        {
            Debug.Log("appling continuous forces (w/coroutine)...");
            StartCoroutine(ApplyForcesOverAPeriodOfTimeAndWaitForFixedUpdate());
        }
    }

    IEnumerator ApplyForcesOverAPeriodOfTimeAndWaitForFixedUpdate()
    {
        Debug.Log("ApplyForcesOverAPeriodOfTimeAndWaitForFixedUpdate started...");
        for (var i = 0; i < 15; i++)
        {
            ApplyForcesOnce();
            yield return new WaitForFixedUpdate();    
        }
        Debug.Log("ApplyForcesOverAPeriodOfTimeAndWaitForFixedUpdate finished...");
    }
    

    void ApplyForcesOverAPeriodOfTime()
    {
        if (m_numberOfFixedUpdatesApplingForces-- <=0)
            return;

        ApplyForcesOverTime();
    }

    private void ApplyForcesOverTime()
    {
        var force = new Vector3(0, 0.3f, 0);

        // Add a continuous acceleration to the rigidbody, ignoring its mass.
        // Apply the acceleration in each FixedUpdate over a duration of time. In contrast to ForceMode.Force, Acceleration will move every 
        // rigidbody the same way regardless of differences in mass. This mode is useful if you just want to control the acceleration of 
        // an object directly. In this mode, the unit of the force parameter is applied to the rigidbody as distance/time^2.
        m_cube1.GetComponent<Rigidbody>().AddForce(force, ForceMode.Acceleration);

        // Add a continuous force to the rigidbody, using its mass.
        // Apply the force in each FixedUpdate over a duration of time. This mode depends on the mass of rigidbody so more force must be 
        // applied to push or twist higher-mass objects the same amount as lower-mass objects. This mode is useful for setting up realistic 
        // physics where it takes more force to move heavier objects. In this mode, the unit of the force parameter is applied to the 
        // rigidbody as mass*distance/time^2.
        m_cube2.GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);
    }

    private void ApplyForcesOnce()
    {        
        var force = new Vector3(0, 5, 0);

        // Add an instant force impulse to the rigidbody, using its mass.
        // Apply the impulse force instantly with a single function call. This mode depends on the mass of rigidbody so more force must be applied 
        // to push or twist higher-mass objects the same amount as lower-mass objects. This mode is useful for applying forces that happen instantly, 
        // such as forces from explosions or collisions. In this mode, the unit of the force parameter is applied to the rigidbody as mass*distance/time.
        m_cube3.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);

        // Add an instant velocity change to the rigidbody, ignoring its mass.
        // Apply the velocity change instantly with a single function call. In contrast to ForceMode.Impulse, VelocityChange will change the velocity 
        // of every rigidbody the same way regardless of differences in mass. This mode is useful for something like a fleet of differently-sized space ships 
        // that you want to control without accounting for differences in mass. In this mode, the unit of the force parameter is applied to the rigidbody as distance/time.
        m_cube4.GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);
    }

    static Vector3 CalculateForceWithForceMode(Rigidbody body, Vector3 force, ForceMode forceMode)
    {
        switch (forceMode)
        {
            case ForceMode.Force:
                return force;
            case ForceMode.Impulse:
                return force / Time.fixedDeltaTime;
            case ForceMode.Acceleration:
                return force * body.mass;
            case ForceMode.VelocityChange:
                return force * body.mass / Time.fixedDeltaTime;
        }

        throw new InvalidOperationException("unsupported ForceMode");
    }
}

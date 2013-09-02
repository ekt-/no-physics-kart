using System;
using System.Collections;
using UnityEngine;

public class TestAddForceMode : MonoBehaviour 
{

    private GameObject m_cube1;
    private GameObject m_cube2;
    private GameObject m_cube3;
    private GameObject m_cube4;

    private ButtonKeyState m_keyF;
    private ButtonKeyState m_keyG;
    internal ButtonKeyState Button1;
    internal ButtonKeyState Button2;

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
	
	// Update is called once per frame
	void Update ()
	{
        Log("Update");
	    m_keyF.NewState = Input.GetKey(KeyCode.F);
        m_keyG.NewState = Input.GetKey(KeyCode.G);
	}

    void FixedUpdate()
    {
        Log("FixedUpdate");
        if (m_keyF.IsKeyDown || Button1.IsKeyDown)
        {
            Debug.Log("appling istant forces...");
            ApplyForcesOnce();
        }

        const bool useCoroutine = true;
        const bool useCoroutineYieldNull = false;

        if (useCoroutine)
        {

            if (m_keyG.IsKeyDown || Button2.IsKeyDown)
            {
                Debug.Log("appling continuous forces (w/coroutine)...");
                if (useCoroutineYieldNull)
                {
                    StartCoroutine(ApplyForcesOverAPeriodOfTimeCoroutineYieldNull());    
                }
                else
                {
                    StartCoroutine(ApplyForcesOverAPeriodOfTimeCoroutineAndWaitForFixedUpdate());    
                }
                
            }            
        }
        else
        {
            if (m_keyG.IsKeyDown || Button2.IsKeyDown)
            {
                m_numberOfFixedUpdatesApplingForces = 100;
            }
            ApplyForcesOverAPeriodOfTime();
        }
    }

    IEnumerator ApplyForcesOverAPeriodOfTimeCoroutineYieldNull()
    {
        // siccome le coroutine vengono aggiornate ad ogni update
        // ho il dubbio che in questo caso non siano indicate (l'azione andrebbe fatta ad ogni FxiedUpdate)
        // todo: fare una scena pesantissima, loggare gli update, i fixedupdate e una coroutine
        Debug.Log("ApplyForcesOverAPeriodOfTimeCoroutineYieldNull started...");
        for (int i = 0; i < 100; i++)
        {
            Log("ApplyForcesOverAPeriodOfTimeCoroutineYieldNull step=" + i);
            ApplyForcesOnce();
            yield return null;
        }
        Debug.Log("ApplyForcesOverAPeriodOfTimeCoroutineYieldNull finished...");
    }

    IEnumerator ApplyForcesOverAPeriodOfTimeCoroutineAndWaitForFixedUpdate()
    {
        Debug.Log("ApplyForcesOverAPeriodOfTimeCoroutineAndWaitForFixedUpdate started...");
        for (int i = 0; i < 100; i++)
        {
            Log("ApplyForcesOverAPeriodOfTimeCoroutineAndWaitForFixedUpdate step=" + i);
            ApplyForcesOnce();
            yield return new WaitForFixedUpdate();    
        }
        Debug.Log("ApplyForcesOverAPeriodOfTimeCoroutineAndWaitForFixedUpdate finished...");
    }

    private int m_numberOfFixedUpdatesApplingForces;
    void ApplyForcesOverAPeriodOfTime()
    {
        if (m_numberOfFixedUpdatesApplingForces-- <=0)
            return;
        Log("ApplyForcesOverAPeriodOfTime");
        ApplyForcesOverTime();
    }

    private void ApplyForcesOverTime()
    {
        var force = new Vector3(0, 0, -10);

        // Add a continuous acceleration to the rigidbody, ignoring its mass.
        // Apply the acceleration in each FixedUpdate over a duration of time. In contrast to ForceMode.Force, Acceleration will move every 
        // rigidbody the same way regardless of differences in mass. This mode is useful if you just want to control the acceleration of 
        // an object directly. In this mode, the unit of the force parameter is applied to the rigidbody as distance/time^2.
        m_cube1.rigidbody.AddForce(force, ForceMode.Acceleration);

        // Add a continuous force to the rigidbody, using its mass.
        // Apply the force in each FixedUpdate over a duration of time. This mode depends on the mass of rigidbody so more force must be 
        // applied to push or twist higher-mass objects the same amount as lower-mass objects. This mode is useful for setting up realistic 
        // physics where it takes more force to move heavier objects. In this mode, the unit of the force parameter is applied to the 
        // rigidbody as mass*distance/time^2.
        m_cube2.rigidbody.AddForce(force, ForceMode.Force);
    }

    private void ApplyForcesOnce()
    {        
        var force = new Vector3(0, 0, -10);

        // Add an instant force impulse to the rigidbody, using its mass.
        // Apply the impulse force instantly with a single function call. This mode depends on the mass of rigidbody so more force must be applied to push or twist higher-mass objects the same amount as lower-mass objects. This mode is useful for applying forces that happen instantly, such as forces from explosions or collisions. In this mode, the unit of the force parameter is applied to the rigidbody as mass*distance/time.
        m_cube3.rigidbody.AddForce(force, ForceMode.Impulse);

        // Add an instant velocity change to the rigidbody, ignoring its mass.
        // Apply the velocity change instantly with a single function call. In contrast to ForceMode.Impulse, VelocityChange will change the velocity of every rigidbody the same way regardless of differences in mass. This mode is useful for something like a fleet of differently-sized space ships that you want to control without accounting for differences in mass. In this mode, the unit of the force parameter is applied to the rigidbody as distance/time.
        m_cube4.rigidbody.AddForce(force, ForceMode.VelocityChange);
    }

    private void Log(string message)
    {
        //Console.WriteLine(message);
    }
}

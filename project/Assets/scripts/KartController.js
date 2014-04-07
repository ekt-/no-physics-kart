//
//http://docs.unity3d.com/Documentation/Components/Layers.html
//http://docs.unity3d.com/Documentation/ScriptReference/Collider.Raycast.html

public var forwardSpeed:Number;
public var steerAngle:Number;
public var rotationT:float = 0.25;


function FixedUpdate () 
{
    var x:Number = Input.GetAxis("Horizontal");
	var y:Number = Input.GetAxis("Vertical");
	
	if (HitTestWithRoad()) 
	{
	}
	this.rigidbody.velocity += y * transform.forward * forwardSpeed;		
	this.rigidbody.AddTorque(transform.up * x * steerAngle, ForceMode.Acceleration);
}

function OnCollisionEnter(collision:Collision) {
	if (collision.gameObject.tag == 'roadWall') {
		//var contact:ContactPoint = collision.contacts[0];
		//this.gameObject.transform.position += contact.normal * 0.2;
	}
}



public var distance:float = 2.0;
public var smoothRatio:float = 0.2;
function HitTestWithRoad() 
{
    var position:Vector3 = transform.position + transform.TransformDirection(Vector3.up) * -0.5;
    var direction:Vector3 = transform.TransformDirection(Vector3.down);
    var ray:Ray = new Ray(position, direction);
    var hit:RaycastHit;
    
    Debug.DrawLine(ray.origin, ray.origin + ray.direction * distance, Color.red);
    var inGround:boolean = false;
    if (Physics.Raycast(ray, hit, distance)) 
    {
        Debug.Log(hit.collider.tag);
        
        if (hit.collider.tag == 'road'){
            inGround = true;
            this.transform.position = hit.point;
            
            Debug.DrawLine(hit.point, hit.point + hit.normal, Color.green);
            
            var current:Vector3 = position - hit.point;
            var target:Vector3 = hit.normal;
            Debug.DrawLine(hit.point, hit.point + current.normalized, Color.white);
            
            var targetQ:Quaternion;
            //TODO: Using "velocity.normalize" instead of "Vector3(0, 1.0, 1.0)"
            var fPosition:Vector3 = transform.position + transform.TransformDirection(Vector3(0, 1.0, 1.0));
            var fDirection:Vector3 = transform.TransformDirection(Vector3.down);
            var fRay:Ray = new Ray(fPosition, fDirection);
            var fHit:RaycastHit;
            var fDistance:float = 2;
            Debug.DrawLine(fRay.origin, fRay.origin + fRay.direction * fDistance, Color.cyan);
            if (Physics.Raycast(fRay, fHit, fDistance)) {
                if (fHit.collider.tag == 'road'){
                    Debug.DrawLine(fHit.point, fHit.point + fHit.normal * fDistance, Color.magenta);
                    targetQ.SetLookRotation(fHit.point - transform.position, target);
                }
            }
            if (targetQ == null) {
                targetQ.SetLookRotation(transform.TransformDirection(Vector3.forward), target);
            }
            this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, targetQ, smoothRatio);
        }
    }
    
    return inGround;
}

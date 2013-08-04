public var forwardSpeed:Number;
public var steerAngle:Number;
public var rotationT:float = 0.25;

function FixedUpdate () {
  var x:Number = Input.GetAxis("Horizontal");
	var y:Number = Input.GetAxis("Vertical");
	
	if (HitTestWithRoad()) {
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

function HitTestWithRoad() {
  /* See http://gist.github.com/1271157 */
}
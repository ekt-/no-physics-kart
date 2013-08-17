#pragma strict

function Start () {

}

function Update () 
{
	if(((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0)))
   	{
		var mPos = Camera.main.ScreenToWorldPoint(Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		this.transform.position = Vector3(mPos.x,mPos.y,Camera.main.nearClipPlane+1);
		
		var hit : RaycastHit;
		var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (!Physics.Raycast (ray, hit, 10000))
			return;
 
		//we've hit a sphere
		if(hit.transform.gameObject.name == "ball")
		{
			//shrink the sphere by half
			hit.transform.gameObject.transform.localScale = new Vector3(0.5,0.5,0.5);
			//create another sphere
			var sphere : GameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sphere.AddComponent("Rigidbody");
			sphere.transform.position = hit.transform.gameObject.transform.position;
			sphere.transform.localScale = new Vector3(0.5,0.5,0.5);
			sphere.rigidbody.AddForce(this.transform.forward * 800);
			sphere.gameObject.name = "ball";	
		}
   	}
}
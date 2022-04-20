
using UnityEngine;
using System.Collections;
	public class BodyCollider : MonoBehaviour
	{ 
		void FixedUpdate()
		{
		transform.eulerAngles = new Vector3(0, 0, 0);
			GetComponent<CapsuleCollider>().height = transform.parent.transform.position.y;
			GetComponent<CapsuleCollider>().center= new Vector3(0,-transform.parent.transform.position.y/2,0);
	}
	}


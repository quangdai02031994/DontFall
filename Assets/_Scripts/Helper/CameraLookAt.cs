using UnityEngine;
using System.Collections;

public class CameraLookAt : MonoBehaviour {

    public Transform Target;
    public Vector3 offset;

	void Start ()
    {
        offset = transform.position - Target.position;
	}
	
	void LateUpdate () 
    {
        transform.position = Target.position + offset;
	}
}

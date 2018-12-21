using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoDrawAI : MonoBehaviour {

	public float VisionRadius;
	
	private void OnDrawGizmosSelected()
	{	
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, VisionRadius);
	}
}

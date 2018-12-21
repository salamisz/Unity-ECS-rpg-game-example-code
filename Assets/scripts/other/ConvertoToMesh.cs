using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertoToMesh : MonoBehaviour {

	[ContextMenu("Convert to regular mesh")]
	void Convert()
	{
		SkinnedMeshRenderer Skins = GetComponent<SkinnedMeshRenderer>();
		MeshRenderer MeshRender = gameObject.AddComponent<MeshRenderer>();
		MeshFilter FilterMesh = gameObject.AddComponent<MeshFilter>();

		FilterMesh.sharedMesh = Skins.sharedMesh;
		MeshRender.sharedMaterials = Skins.sharedMaterials;
		
		DestroyImmediate(Skins);
		DestroyImmediate(this);
	} 
}

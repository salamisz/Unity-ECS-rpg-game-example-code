using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class Selected : MonoBehaviour
{

	public GameObjectEntity EntityObject;
	private Entity _entity;

	private void Awake()
	{
		EntityObject = GetComponent<GameObjectEntity>();
		if (EntityObject != null)
		{
			_entity = EntityObject.Entity;
		}
	}
}

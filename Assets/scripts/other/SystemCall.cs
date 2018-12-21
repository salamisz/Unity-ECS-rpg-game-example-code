using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class SystemCall : MonoBehaviour {

	public void UseThis()
	{
		EquipmentSystem.Instance.EquipItem();
	}
}

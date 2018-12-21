using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class InventoryList : MonoBehaviour {

	public List<Selected> Items = new List<Selected>();
	public List<Equiable> Equiables = new List<Equiable>();
	public List<int> AttackModifiers = new List<int>();
	public List<int> ProtectionModifiers = new List<int>();
}

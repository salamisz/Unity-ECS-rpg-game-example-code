using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class RemoveFunc : MonoBehaviour
{
  public void RemoveItemCall()
  {
    RemoveItemsSystem.Instance.RemoveItem();
  }
}

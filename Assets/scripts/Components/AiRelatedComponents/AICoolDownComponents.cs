using Unity.Entities;
using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public struct AICoolDownComponent : IComponentData
   {
       public float AIAttackSpeed;
       [HideInInspector]public float AIAttackCooldown;
       
   }
   public class AICoolDownComponents : ComponentDataWrapper<AICoolDownComponent>{}
}
  

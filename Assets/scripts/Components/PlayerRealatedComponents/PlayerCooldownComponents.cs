using Unity.Entities;
using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public struct PlayerCooldownComponent : IComponentData
   {
       public float PlayerAttackSpeed;
       [HideInInspector]public float PlayerAttackCooldown;
       public float AnimationDelay;
       [HideInInspector]public float AnimationDelaySpeed;
   }
   public class PlayerCooldownComponents : ComponentDataWrapper<PlayerCooldownComponent>{}
}
  

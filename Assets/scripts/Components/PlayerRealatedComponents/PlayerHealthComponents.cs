using Unity.Entities;
using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public struct PlayerHealthComponent : IComponentData
    {
        public float PlayerHealth;
        [HideInInspector]public float TakenDamage;
        public float MaxHP;
    }
   public class PlayerHealthComponents : ComponentDataWrapper<PlayerHealthComponent>{}
}
  

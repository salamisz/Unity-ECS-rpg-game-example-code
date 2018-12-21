using Unity.Entities;
using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public struct AiHealthComponent : IComponentData
    {
        public float AIHealth;
        [HideInInspector]public float AIDamage;
        public float AIMaxHP;
    }
   public class AiHealthComponents : ComponentDataWrapper<AiHealthComponent>{}
}
  

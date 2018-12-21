using Unity.Entities;
using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public struct ModifierComponent : IComponentData
    {
        [HideInInspector]public int ProtectionModifier;
        [HideInInspector]public int AttackModifier;
        public int FinalProtectionModifier;
        public int FinalAttackModifier;
    }
   public class ModifierComponents : ComponentDataWrapper<ModifierComponent>{}
}
  

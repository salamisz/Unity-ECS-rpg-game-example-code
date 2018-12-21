using Unity.Entities;
using System;

namespace DefaultNamespace
{
    [Serializable]
    public struct AIModifierComponent : IComponentData
   {
       public int AIProtectionModifier;
       public float AIAttackModifier;
   }
   public class AIModifierComponents : ComponentDataWrapper<AIModifierComponent>{}
}
  

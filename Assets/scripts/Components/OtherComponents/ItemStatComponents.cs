using Unity.Entities;
using System;

namespace DefaultNamespace
{
    [Serializable]
    public struct ItemStatComponent : IComponentData
   {
       public int AttackDamage;
       public int protection;
   }
   public class ItemStatComponents : ComponentDataWrapper<ItemStatComponent>{}
}
  

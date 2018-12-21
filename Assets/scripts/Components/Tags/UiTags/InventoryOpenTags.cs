using Unity.Entities;
using System;

namespace DefaultNamespace
{
    [Serializable]
    public struct InventoryOpenTag : IComponentData
   {
   
   }
   public class InventoryOpenTags : ComponentDataWrapper<InventoryOpenTag>{}
}
  

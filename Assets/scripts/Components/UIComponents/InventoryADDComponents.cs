using Unity.Entities;
using System;

namespace DefaultNamespace
{
    [Serializable]
    public struct InventoryComponent : IComponentData
    {
        public float space;
    }
   public class InventoryADDComponents : ComponentDataWrapper<InventoryComponent>{}
}
  

using Unity.Entities;
using System;

namespace DefaultNamespace
{
    public struct DynamicEquipmentTag : IComponentData
   {
   }
   public class DynamicEquipmentTags : ComponentDataWrapper<DynamicEquipmentTag>{}
}
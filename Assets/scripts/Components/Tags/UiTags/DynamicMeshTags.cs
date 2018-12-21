using Unity.Entities;
using System;

namespace DefaultNamespace
{
    public struct DynamicMeshTag : IComponentData
   {
   }
   public class DynamicMeshTags : ComponentDataWrapper<DynamicMeshTag>{}
}
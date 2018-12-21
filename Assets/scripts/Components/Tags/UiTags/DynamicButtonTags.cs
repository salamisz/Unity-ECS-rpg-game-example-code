using Unity.Entities;
using System;

namespace DefaultNamespace
{
    public struct DynamicButtonTag : IComponentData
   {
   }
   public class DynamicButtonTags : ComponentDataWrapper<DynamicButtonTag>{}
}
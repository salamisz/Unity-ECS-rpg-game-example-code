using Unity.Entities;
using System;

namespace DefaultNamespace
{
    public struct UiTag : IComponentData
   {
   }
   public class UiTags : ComponentDataWrapper<UiTag>{}
}
  

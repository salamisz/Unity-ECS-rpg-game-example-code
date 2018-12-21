using Unity.Entities;
using System;

namespace DefaultNamespace
{
    public struct BodyTag : IComponentData
   { 
   }
   public class BodyTags : ComponentDataWrapper<BodyTag>{}
}
  

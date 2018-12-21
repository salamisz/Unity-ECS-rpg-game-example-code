using Unity.Entities;
using System;

namespace DefaultNamespace
{

    public struct ArmorTag : IComponentData
   {   
   }
   public class ArmorTags : ComponentDataWrapper<ArmorTag>{}
}
  

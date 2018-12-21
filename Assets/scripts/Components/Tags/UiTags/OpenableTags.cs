using Unity.Entities;
using System;

namespace DefaultNamespace
{
    public struct OpenableTag : IComponentData
   {  
   }
   public class OpenableTags : ComponentDataWrapper<OpenableTag>{}
}
  

using Unity.Entities;
using System;

namespace DefaultNamespace
{
    public struct ButtonTag : IComponentData
   {
   
   }
   public class ButtonTags : ComponentDataWrapper<ButtonTag>{}
}
  

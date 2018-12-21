using Unity.Entities;
using System;

namespace DefaultNamespace
{
    public struct AIBodyTag : IComponentData
   {  
   }
   public class AIBodyTags : ComponentDataWrapper<AIBodyTag>{}
}
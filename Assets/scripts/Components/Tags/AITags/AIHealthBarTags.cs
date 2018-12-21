using Unity.Entities;
using System;

namespace DefaultNamespace
{
    public struct AIHealthBarTag : IComponentData
   {
   }
   public class AIHealthBarTags : ComponentDataWrapper<AIHealthBarTag>{}
}
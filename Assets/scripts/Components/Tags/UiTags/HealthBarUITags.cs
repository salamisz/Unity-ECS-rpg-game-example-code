using Unity.Entities;
using System;

namespace DefaultNamespace
{
    public struct HealthBarUITag : IComponentData
   {
   }
   public class HealthBarUITags : ComponentDataWrapper<HealthBarUITag>{}
}
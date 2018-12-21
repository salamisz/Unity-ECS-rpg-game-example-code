using Unity.Entities;
using System;

namespace DefaultNamespace
{
    public struct HealthBarSliderTag : IComponentData
   {
   }
   public class HealthBarSliderTags : ComponentDataWrapper<HealthBarSliderTag>{}
}
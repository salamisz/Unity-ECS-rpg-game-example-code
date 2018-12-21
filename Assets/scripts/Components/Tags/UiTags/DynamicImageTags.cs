using Unity.Entities;
using System;

namespace DefaultNamespace
{
    public struct DynamicImageTag : IComponentData
   {
   }
   public class DynamicImgheTags : ComponentDataWrapper<DynamicImageTag>{}
}
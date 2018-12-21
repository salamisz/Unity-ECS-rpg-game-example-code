using Unity.Entities;
using System;

namespace DefaultNamespace
{
    public struct LeftGripTag : IComponentData
   {
   }
   public class LeftGripTags : ComponentDataWrapper<LeftGripTag>{}
}
using Unity.Entities;
using System;

namespace DefaultNamespace
{
    public struct RightGripTag : IComponentData
   {
   }
   public class RightGripTags : ComponentDataWrapper<RightGripTag>{}
}
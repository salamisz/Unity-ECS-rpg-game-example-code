using Unity.Entities;
using System;

namespace DefaultNamespace
{
    public struct AiAnimTag : IComponentData
   {
   }
   public class AiAnimTags : ComponentDataWrapper<AiAnimTag>{}
}
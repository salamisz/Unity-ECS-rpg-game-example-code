using Unity.Entities;
using System;

namespace DefaultNamespace
{
    public struct PlayerBlendTag : IComponentData
   {
   }
   public class PlayerBlendTags : ComponentDataWrapper<PlayerBlendTag>{}
}
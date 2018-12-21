using Unity.Entities;
using System;

namespace DefaultNamespace
{
    public struct PlayerHealthBarTag : IComponentData
   {
   }
   public class PlayerHealthBarTags : ComponentDataWrapper<PlayerHealthBarTag>{}
}
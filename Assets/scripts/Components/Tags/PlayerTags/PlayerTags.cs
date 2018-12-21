using Unity.Entities;
using System;

namespace DefaultNamespace
{
    public struct PlayerTag : IComponentData
   {
   
   }
   public class PlayerTags : ComponentDataWrapper<PlayerTag>{}
}
  

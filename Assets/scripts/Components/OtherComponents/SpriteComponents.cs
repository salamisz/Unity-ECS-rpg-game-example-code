using Unity.Entities;
using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public struct SpriteComponent : ISharedComponentData
    {
        public Sprite Icon;
    }
   public class SpriteComponents : SharedComponentDataWrapper<SpriteComponent>{}
}
  

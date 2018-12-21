using Unity.Entities;
using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public struct BlendSpeedComp : IComponentData
    {
        public float BlendSpeed;
        public float smooth;
    }
   public class BlendSpeedComps : ComponentDataWrapper<BlendSpeedComp>{}
}
  

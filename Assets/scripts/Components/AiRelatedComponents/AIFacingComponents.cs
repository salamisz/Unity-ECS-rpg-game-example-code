using Unity.Entities;
using System;
using Unity.Mathematics;
using UnityEngine;

namespace DefaultNamespace
{
    public struct AIFacingComponent : IComponentData
    {
        public float AIDistance;
        public Quaternion AIRotation;
        public float3 AIDirection;
    }
   public class AIFacingComponents : ComponentDataWrapper<AIFacingComponent>{}
}
  

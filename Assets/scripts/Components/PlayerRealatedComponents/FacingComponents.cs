using Unity.Entities;
using System;
using Unity.Mathematics;
using UnityEngine;

namespace DefaultNamespace
{
    public struct FacingComponent : IComponentData
    {
        public float3 diretion;
        public Quaternion lookRotation;
        public float distance;
    }
   public class FacingComponents : ComponentDataWrapper<FacingComponent>{}
}
  

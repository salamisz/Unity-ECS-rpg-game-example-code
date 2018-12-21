using System;
using Unity.Entities;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public struct AgentDestination : ISharedComponentData
    {
        public LayerMask inputLayerMask;
        public float maxRayDistance;
    }
   public class AgentDestinationComponent : SharedComponentDataWrapper<AgentDestination>{}    
}

  

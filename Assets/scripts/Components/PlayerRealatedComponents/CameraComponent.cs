using Unity.Entities;
using UnityEngine;
using System;

namespace DefaultNamespace
{
    [Serializable]
    public struct CameraComponents : IComponentData
    {
        public Vector3 offset;
        public float pitch;
        public float zoomSpeed;
        public float maxZoom;
        public float MinZoom;
        public float CurrentZoom;
        public float YawSpeed;
        public float CurrentYaw;
    }
   public class CameraComponent : ComponentDataWrapper<CameraComponents>{}
}
  

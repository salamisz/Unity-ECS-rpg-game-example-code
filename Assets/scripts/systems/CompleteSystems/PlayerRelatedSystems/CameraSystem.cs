using Unity.Entities;
using UnityEngine;
 
 namespace DefaultNamespace
 {

     public class CameraSystem : ComponentSystem
     {
         struct CameraData
         {
             public readonly int Length;
             public ComponentDataArray<CameraComponents> CameraMove;
         }

         [Inject] private CameraData _cameraData;
         
         protected override void OnUpdate()
         {
             var dt = Time.deltaTime;
             for (int i = 0; i < _cameraData.Length; i++)
             {
                 var CameraMove = _cameraData.CameraMove[i];
                 //allows to zoom in and out
                CameraMove.CurrentZoom -= Input.GetAxis("Mouse ScrollWheel") * CameraMove.zoomSpeed;
                 CameraMove.CurrentZoom = Mathf.Clamp(CameraMove.CurrentZoom,
                     CameraMove.MinZoom, CameraMove.maxZoom);
                 //allows canera rotation
                 CameraMove.CurrentYaw -= Input.GetAxis("Horizontal") * CameraMove.YawSpeed * dt;
                 _cameraData.CameraMove[i] = CameraMove;
             }
         }
     }
 }
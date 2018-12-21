using Unity.Collections;
using Unity.Entities;
using UnityEngine.EventSystems;
 using UnityEngine;
 using UnityEngine.AI;
 
 namespace DefaultNamespace
 {
     public class MovementSystems : ComponentSystem
     {
         private struct Data
         {
             public readonly int Length;
             public ComponentArray<NavMeshAgent> agent;
             [ReadOnly] public SharedComponentDataArray<AgentDestination> agentDestination;
             
         }
         private struct CameraData
         {
             public ComponentArray<Camera> mainCamera;
         }
         [Inject] private Data _data;
         [Inject] private CameraData _cameraData;
 
         protected override void OnUpdate()
         {
             for (int i = 0; i < _data.Length; i++)
             {
                 if (_cameraData.mainCamera[i] != null)
                 {
                     var cameraRay = _cameraData.mainCamera[i].ScreenPointToRay(Input.mousePosition);
                     var agentDestination = _data.agentDestination[i];

                     if (EventSystem.current.IsPointerOverGameObject())
                         return;
                     if (Input.GetMouseButton(0))
                     {
                         if (Physics.Raycast(cameraRay, out var hit, agentDestination.maxRayDistance,
                             agentDestination.inputLayerMask))
                         {
                             //moves the player
                             _data.agent[i].destination = hit.point;
                         }
                     }
                 }
             }
         }
     }
 }
using DefaultNamespace;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;
//[UpdateAfter(typeof(PreLateUpdate))]
public class HeatlhBarSystem : ComponentSystem
     {
         private struct Data
         {
             public readonly int Length;
             public ComponentArray<Transform> PlayerTransform;
             public ComponentDataArray<PlayerTag> Tag;
         }
         private struct ImageData
         {
             public readonly int Length;
             public ComponentArray<Image> HealthBar;
             public ComponentDataArray<HealthBarUITag> Tags;
         }
         private struct CamData
         {
             public readonly int Length;
             private ComponentArray<Camera> Cam;
             public ComponentArray<Transform> Trans;
         }

         [Inject] private Data _data;
         [Inject] private ImageData _imageData;
         [Inject] private CamData _camData;
         
         protected override void OnUpdate()
         {
             for (int i = 0; i < _camData.Length; i++)
             {
                 for (int j = 0; j < _imageData.Length; j++)
                 {
                     //prevents the health bar from rotating with the player or AI
                     _imageData.HealthBar[j].transform.forward = -_camData.Trans[i].forward;
                 }
             }
         }
     }
using Unity.Entities;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
 
namespace DefaultNamespace
{
    public class CameraLookAtSystem : ComponentSystem
    {
        private struct Data_cam
        {
            public readonly int Length;
            public ComponentArray<Transform> cam;
            public ComponentDataArray<CameraComponents> camera;
        }
         
        private struct Data_player
        {
            public readonly int Length;
            public ComponentArray<Transform> transform;
            //Tag
            public ComponentDataArray<PlayerTag> p_Tags;
        }
 
        [Inject] private Data_cam _dataCam;
        [Inject] private Data_player _dataPlayer;
         
 
        protected override void OnUpdate()
        {

            for (int i = 0; i < _dataCam.Length; i++)
            {
                for (int j = 0; j < _dataPlayer.Length; j++)
                {
                    //this system tells the camera how to rotate and where to look
                    _dataCam.cam[i].position = _dataPlayer.transform[i].position -
                                               _dataCam.camera[i].offset * _dataCam.camera[i].CurrentZoom;
                    _dataCam.cam[i].LookAt(_dataPlayer.transform[i].position + Vector3.up * _dataCam.camera[i].pitch);
                    _dataCam.cam[i].RotateAround(_dataPlayer.transform[i].position, Vector3.up, _dataCam.camera[i].CurrentYaw);
                }
            }
        }
    }
}
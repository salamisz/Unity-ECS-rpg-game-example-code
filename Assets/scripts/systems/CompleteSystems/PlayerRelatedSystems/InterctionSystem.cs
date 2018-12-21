using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class InterctionSystem : ComponentSystem
    {

        private struct interData
        {
            public readonly int Length;
            public ComponentArray<Transform> InterTrans;
            public ComponentArray<GizmoDraw> lines;

        }

        private struct PlayerData
        {
            public readonly int Length;
            public ComponentArray<Transform> playerTrans;
            public ComponentDataArray<FacingComponent> Faceing;
            public ComponentArray<NavMeshAgent> agent;
            [ReadOnly] public SharedComponentDataArray<AgentDestination> agentDestination;
        }
        private struct Data
        {
            public readonly int Length;
            public ComponentArray<Camera> mainCamera;
        }
        [Inject] private Data _data;

        [Inject] private interData _interData;
        [Inject] private PlayerData _player;

        protected override void OnUpdate()
        {
            var Deltatime = Time.deltaTime;

            for (int i = 0; i < _player.Length; i++)
            {
                var agent = _player.agent[i];
                var playerTransform = _player.playerTrans[i];
                var playerPosition = playerTransform.position;
                var agentDestination = _player.agentDestination[i];
                var Facing = _player.Faceing[i];
                
                for (int k = 0; k < _interData.Length; k++)
                {
                    var cameraRay = _data.mainCamera[i].ScreenPointToRay(Input.mousePosition);

                    if (Input.GetMouseButtonDown(1))
                    {
                        if (Physics.Raycast(cameraRay, out var hit, agentDestination.maxRayDistance))
                        {
                            if (hit.collider.GetComponent<GizmoDraw>())
                            {
                                agent.SetDestination(hit.point); // moves the player torwards the interactible
                                agent.stoppingDistance = _interData.lines[k].radius; // tells him where to stop close to the interactible

                                //gets direction towards interactible
                                Facing.diretion = (_interData.InterTrans[k].position - playerPosition).normalized;
                                //find out how to rotate
                                Facing.lookRotation =
                                    Quaternion.LookRotation(new Vector3(Facing.diretion.x, 0f, Facing.diretion.z));
                                //move torwards the rotation
                                _player.playerTrans[i].rotation = Quaternion.Slerp(playerTransform.rotation,
                                    Facing.lookRotation, Deltatime * 5f);                                
                                _player.Faceing[i] = Facing;
                            }
                        }
                    }
                }

            }
        }
    }
}
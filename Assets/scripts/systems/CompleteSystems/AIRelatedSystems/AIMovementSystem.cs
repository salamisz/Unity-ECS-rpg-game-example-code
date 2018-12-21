using Unity.Entities;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    [UpdateBefore(typeof(CombatSystem))]
    public class AIMovementSystem : ComponentSystem
    {
        private struct Data
        {
            public readonly int Length;
            [ReadOnly] private ComponentDataArray<PlayerTag> Tag;
            public ComponentArray<Transform> PlayerTransform;
        }
        private struct AIData
        {
            public readonly int Length;
            public ComponentArray<Transform> AITransform;
            public ComponentDataArray<AIFacingComponent> Faces;
            public ComponentArray<GizmoDrawAI> Draw;
            public ComponentArray<NavMeshAgent> NavAgent;
        }

        [Inject] private Data _data;
        [Inject] private AIData _aiData;
        
        protected override void OnUpdate()
        {
            var deltaTime = Time.deltaTime;
            for (int i = 0; i < _data.Length; i++)
            {
                for (int j = 0; j < _aiData.Length; j++)
                {
                    var AIFace =_aiData.Faces[j];
                    //Tells the AI the distances between itself and the player
                    AIFace.AIDistance = Vector3.Distance(_data.PlayerTransform[i].position,
                        _aiData.AITransform[j].position);
                    if (AIFace.AIDistance <= _aiData.Draw[j].VisionRadius && _aiData.NavAgent[j].enabled == true)
                    {
                        //Moves the AI
                        _aiData.NavAgent[j].SetDestination(_data.PlayerTransform[i].position);
                        if (AIFace.AIDistance <= _aiData.NavAgent[j].stoppingDistance)
                        {
                            //tells the AI the direction
                            AIFace.AIDirection = (_data.PlayerTransform[i].position - _aiData.AITransform[j].position).normalized;
                            //tells the AI the rotation
                            AIFace.AIRotation = Quaternion.LookRotation(new Vector3(AIFace.AIDirection.x, 0, AIFace.AIDirection.z));
                            //Rotates the AI
                            _aiData.AITransform[j].rotation = Quaternion.Slerp(_aiData.AITransform[j].rotation, AIFace.AIRotation, deltaTime * 5f);
                            _aiData.Faces[i] = AIFace;
                        }
                    }
                }
            }
        }
    }
}
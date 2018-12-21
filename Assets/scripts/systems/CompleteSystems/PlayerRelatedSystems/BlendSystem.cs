using Unity.Entities;
using UnityEngine;
using UnityEngine.AI;

//in order to get a component from a child object, a tag, a component and a secon struct must be created

namespace DefaultNamespace
{
    [UpdateBefore(typeof(CombatSystem))]
    public class BlendSystem : ComponentSystem
    {

        private struct Data
        {
            public readonly int Length;
            public ComponentArray<CombatBool> InCombat;
            public ComponentArray<NavMeshAgent> Agent;
        }

        private struct Anim_data
        {
            public readonly int Length;
            public ComponentDataArray<BlendSpeedComp> Blend;
            public ComponentArray<Animator> Anim;
            public ComponentArray<AnimatorOverride> Override;
        }
 
        [Inject] private Data _data;
        [Inject] private Anim_data Anims;

        protected override void OnUpdate()
        {
            var Clock = Time.deltaTime;
            for (int i = 0; i < _data.Length; i++)
                             
                {
                    var Blendy = Anims.Blend[i];
                    if (Anims.Override[i].CurrentAnimation.Length == 0)
                    {
                        //adds animation to player and AI
                        Anims.Override[i].CurrentAnimation = Anims.Override[i].DefaultAnimation;
                    }
                    //Animates walking
                    Blendy.BlendSpeed = _data.Agent[i].velocity.magnitude / _data.Agent[i].speed;
                    Anims.Anim[i].SetFloat("BlendSpeed", Blendy.BlendSpeed, Blendy.smooth, Clock);
                    //set the bool for combat
                    Anims.Anim[i].SetBool("CombatBool", _data.InCombat[i].InCombat);
                    Anims.Blend[i] = Blendy;
                    
                    
                
            }
        }
    }
}
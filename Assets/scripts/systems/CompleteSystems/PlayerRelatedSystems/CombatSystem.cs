
using System.Security.Cryptography.X509Certificates;
using Unity.Entities;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    [UpdateAfter(typeof(EquipmentSystem))]
    public class CombatSystem : ComponentSystem
    {
        private struct PlayerData
        {
            public readonly int Length;
            public ComponentDataArray<PlayerHealthComponent> HealthComponent;
            public ComponentDataArray<ModifierComponent> Modify;
            public ComponentArray<InventoryList> Lists;
            public ComponentDataArray<FacingComponent> Faceing;
            public ComponentDataArray<PlayerCooldownComponent> PlayerCooldown;
            public ComponentArray<CombatBool> InCombat;
            public ComponentArray<Transform> PlayerTransform;
            public EntityArray _Entity;
        }

        private struct Data
        {
            public readonly int Length;
            public ComponentArray<NavMeshAgent> Nav;
            public ComponentDataArray<AiHealthComponent> HealthComponent;
            public ComponentDataArray<AIModifierComponent> AIModifications;
            public ComponentArray<GizmoDraw> GizmoLines;
            public ComponentArray<GizmoDrawAI> GizmoVersionLines;
            public ComponentArray<CombatBool> InCombat;
            public ComponentArray<Transform> AITransform;
            public ComponentDataArray<AICoolDownComponent> AICoolDown;
            [ReadOnly] public ComponentDataArray<AIFacingComponent> AIFacing;
            public EntityArray _Entity;
        }

        private struct AIGraphicsData
        {
            [ReadOnly] private ComponentDataArray<AIBodyTag> Tags;
            public ComponentArray<SkinnedMeshRenderer> SkinnedMeshRender;
            public EntityArray _Entity;
        }
        private struct Anim_data
        {
            public readonly int Length;
            public ComponentArray<Animator> Anim;
            public ComponentArray<AnimatorOverride> Override;
            [ReadOnly] private ComponentDataArray<PlayerBlendTag> Tag;
        }
        //the reason here for two identical component groups is because it would not work if player and AI had the same component group it would make them both trigger the animation at once
        private struct AIAnim_data
        {
            public readonly int Length;
            public ComponentArray<Animator> Anim;
            public ComponentArray<AnimatorOverride> Override;
            [ReadOnly] private ComponentDataArray<AiAnimTag> Tag;
            public EntityArray AnimEntity;
        }
        private struct ImageData
        {
            public readonly int Length;
            public ComponentArray<Image> HealthBar;
           [ReadOnly] public ComponentDataArray<HealthBarSliderTag> Tags;
            public EntityArray _EntityArray;
        }
        private struct ImageUIData
        {
            public readonly int Length;
            public ComponentArray<Image> HealthBarUI;
            [ReadOnly]public ComponentDataArray<HealthBarUITag> Tags;
            public EntityArray _EntityArray;
        }
        

        [Inject] private Data _data;
        [Inject] private AIGraphicsData _AIGraphics;
        [Inject] private PlayerData _playerData;
        [Inject] private Anim_data Anims;
        [Inject] private ImageData _imageData;
        [Inject] private ImageUIData _imageUiData;
        [Inject] private AIAnim_data _aiAnim;

        protected override void OnUpdate()
        {
            var UnityTime = Time.deltaTime;

            for (int i = 0; i < _playerData.Length; i++)
            {
                //writing to modify values;
                    var HealthPlayer = _playerData.HealthComponent[i];
                    var PlayerModification = _playerData.Modify[i];
                    var PlayerCoolDown = _playerData.PlayerCooldown[i];
                    var PlayerFace = _playerData.Faceing[i];

                    for (int j = 0; j < _data.Length; j++)
                    {
                        //for some reason the distance does not save when writen to so it need to be calculated for each system using distance separately
                        PlayerFace.distance = Vector3.Distance(_playerData.PlayerTransform[i].position,
                            _data.AITransform[j].position
                        );
                        var AICoolDown = _data.AICoolDown[j];
                        var AIFace = _data.AIFacing[j];
                        //for some reason the distance does not save when writen to so it need to be calculated for each system using distance separately
                        AIFace.AIDistance = Vector3.Distance(_playerData.PlayerTransform[i].position,
                            _data.AITransform[j].position);

                        var AImodification = _data.AIModifications[j];
                        var AIHealth = _data.HealthComponent[j];

                        if (EntityManager.HasComponent<GizmoDrawAI>(_data._Entity[j]) &&
                            AIFace.AIDistance <= _data.GizmoLines[j].radius * 2)
                        {

                            //Player Damage Calculations
                            HealthPlayer.TakenDamage = AImodification.AIAttackModifier;
                            HealthPlayer.TakenDamage -= PlayerModification.FinalProtectionModifier;
                            HealthPlayer.TakenDamage = Mathf.Clamp(HealthPlayer.TakenDamage, 0, int.MaxValue);
                            //AI Damage Calculations
                            AIHealth.AIDamage = PlayerModification.FinalAttackModifier;
                            AIHealth.AIDamage -= AImodification.AIProtectionModifier;
                            AIHealth.AIDamage = Mathf.Clamp(AIHealth.AIDamage, 0, int.MaxValue);
                            //writing
                            AICoolDown.AIAttackCooldown -= UnityTime;
                            PlayerCoolDown.PlayerAttackCooldown -= UnityTime;
                            _playerData.PlayerCooldown[i] = PlayerCoolDown;
                            _data.AICoolDown[j] = AICoolDown;
                            
                            for (int k = 0; k < _imageData.Length; k++)
                            {
                                    if (AICoolDown.AIAttackCooldown <= 0f)
                                    {                                       
                                        
                                        if (EntityManager.HasComponent<AIHealthBarTag>(_imageData._EntityArray[k]))
                                        {
                                            // changes the health bar
                                            float AIHealthPrecent =
                                                _data.HealthComponent[j].AIHealth / _data.HealthComponent[j].AIMaxHP;
                                            _imageData.HealthBar[k].fillAmount = AIHealthPrecent;
                                        }
                                             //AI Attack animation trigger                                   
                                            _aiAnim.Anim[j].SetTrigger("Attack");
                                            //Randomizes between two attack animations
                                            int AIAttackIndex = Random.Range(0,_aiAnim.Override[j].CurrentAnimation.Length);                                                    
                                            _aiAnim.Override[j].AnimatoinOverrides[_aiAnim.Override[j].ReplacebleAnimations.name]=
                                                _aiAnim.Override[j].CurrentAnimation[AIAttackIndex];

                                            //delay calculation
                                            AICoolDown.AIAttackCooldown = 1f / AICoolDown.AIAttackSpeed;

                                            //PLayer Taking damage
                                            HealthPlayer.PlayerHealth -= HealthPlayer.TakenDamage;

                                            if (HealthPlayer.PlayerHealth <= 0f)
                                            {
                                                //restart level if player is dead
                                                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                                            }

                                            _data.AICoolDown[j] = AICoolDown;
                                        
                                    }

                                    if (PlayerCoolDown.PlayerAttackCooldown <= 0f)
                                    {
                                        //delay the attack for the animations
                                        PlayerCoolDown.AnimationDelay -= UnityTime;
                                        _playerData.PlayerCooldown[i] = PlayerCoolDown;

                                        if (EntityManager.HasComponent<PlayerHealthBarTag>(_imageData._EntityArray[k]))
                                        {
                                            // changes the health bar
                                            float PlayerHealthPercent =
                                                _playerData.HealthComponent[i].PlayerHealth /
                                                _playerData.HealthComponent[i].MaxHP;
                                            _imageData.HealthBar[k].fillAmount = PlayerHealthPercent;
                                        }

                                        if (Input.GetMouseButtonDown(1))
                                        {

                                            if (_playerData.PlayerCooldown[i].AnimationDelay <= 0)
                                            {
                                                // animation delay must be equal to attack speed
                                                PlayerCoolDown.AnimationDelaySpeed = PlayerCoolDown.PlayerAttackSpeed;

                                                //Player Attack animation trigger
                                                Anims.Anim[i].SetTrigger("Attack");

                                                //Randomizes between two attack animations
                                                int AttackIndex = Random.Range(0,Anims.Override[i].CurrentAnimation.Length);                                                    
                                                Anims.Override[i].AnimatoinOverrides[Anims.Override[i].ReplacebleAnimations.name]=
                                                    Anims.Override[i].CurrentAnimation[AttackIndex];                                                        
                                                    
                                                //delay calculations
                                                PlayerCoolDown.AnimationDelay = 1f / PlayerCoolDown.AnimationDelaySpeed;
                                                _playerData.PlayerCooldown[i] = PlayerCoolDown;
                                                PlayerCoolDown.PlayerAttackCooldown = 1f / PlayerCoolDown.PlayerAttackSpeed;
                                                //AI Taking damage
                                                AIHealth.AIHealth -= AIHealth.AIDamage;

                                                if (AIHealth.AIHealth <= 0f)
                                                {
                                                    if (EntityManager.HasComponent<BlendSpeedComp>(_aiAnim.AnimEntity[j]))
                                                    {
                                                        //Remove blendSpeedComponent because if AI is dead it stops to work fr the player too
                                                        PostUpdateCommands.RemoveComponent<BlendSpeedComp>(_aiAnim.AnimEntity[j]);
                                                    }
                                                    //disable combat idle animations
                                                    _playerData.InCombat[i].InCombat = false;
                                                    //disables the healt bar
                                                    _imageUiData.HealthBarUI[k].enabled = false;
                                                    //disables the healt bar
                                                    _imageData.HealthBar[k].enabled = false;
                                                    //Removes the visuals of the skeleton 
                                                    _AIGraphics.SkinnedMeshRender[j].enabled = false;
                                                    //removes the nav mesh agent to prevent the AI to keep going
                                                    _data.Nav[j].enabled = false;
                                                    //destroy they entities
                                                    PostUpdateCommands.DestroyEntity(_AIGraphics._Entity[j]);
                                                    PostUpdateCommands.DestroyEntity(_data._Entity[j]);
                                                }
                                            }

                                            _playerData.PlayerCooldown[j] = PlayerCoolDown;
                                        }

                                    }
                                

                                _playerData.HealthComponent[i] = HealthPlayer;

                                _data.AIModifications[j] = AImodification;
                                _data.HealthComponent[j] = AIHealth;
                            }
                        }

                        if (PlayerFace.distance <= _data.GizmoVersionLines[j].VisionRadius / 2 && _AIGraphics.SkinnedMeshRender[j].enabled == true)
                        {
                            //Enable combat idle animations
                            _playerData.InCombat[i].InCombat = true;
                            _data.InCombat[i].InCombat = true;


                        }
                        else if (PlayerFace.distance > _data.GizmoVersionLines[j].VisionRadius / 2)
                        {
                            //disable combat idle animations
                            _playerData.InCombat[i].InCombat = false;
                            _data.InCombat[i].InCombat = false;
                        }
                    }
                
            }
        }
    }

}
using DefaultNamespace;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[UpdateBefore(typeof(BlendSystem))]
public class GripAnimSystem : ComponentSystem
     {
         private struct ItemData
         {
             public ComponentArray<Equiable> EquimentItem;
             public EntityArray _Entity;
         }
         private struct PlayerData
         {
             public readonly int Length;
             public ComponentArray<InventoryList> Lists;
         }
         private struct Anim_data
         {
             public readonly int Length;
             public ComponentDataArray<BlendSpeedComp> Blend;
             public ComponentArray<Animator> Anim;
             public ComponentArray<AnimatorOverride> Override;
             [ReadOnly] private ComponentDataArray<PlayerBlendTag> Tag;
         }
         protected override void OnStartRunning()
         {
             for (var k = 0; k < Anims.Length; k++)
             {
                 //initialize animatorOverrideController
                 Anims.Override[k].AnimatoinOverrides = new AnimatorOverrideController(Anims.Anim[k].runtimeAnimatorController);
                 Anims.Anim[k].runtimeAnimatorController = Anims.Override[k].AnimatoinOverrides;                
             }
         }
         
         [Inject] private Anim_data Anims;
         [Inject] private PlayerData _playerData;
         [Inject] private ItemData _itemData;
         
         protected override void OnUpdate()
         {
             for (int i = 0; i < Anims.Length; i++)
             {
                 for (int j = 0; j < _itemData._Entity.Length; j++)
                 {
                     if (EntityManager.HasComponent<LeftGripTag>(_itemData._Entity[j]) && _playerData.Lists[i].Equiables.Contains(_itemData.EquimentItem[j]))
                     {
                         //grip the sword
                         Anims.Anim[i].SetLayerWeight(2, 1);
                         //Change animations from punch to sword attack animations
                         Anims.Override[i].CurrentAnimation = Anims.Override[i].SwordAnimations;

                     }
                     else if (EntityManager.HasComponent<LeftGripTag>(_itemData._Entity[j]) &&
                              !_playerData.Lists[i].Equiables.Contains(_itemData.EquimentItem[j]))
                     {
                         //release the sword
                         Anims.Anim[i].SetLayerWeight(2, 0);  
                     }

                     if (EntityManager.HasComponent<RightGripTag>(_itemData._Entity[j]) && _playerData.Lists[i].Equiables.Contains(_itemData.EquimentItem[j]))
                     {
                         //grip the shield
                         Anims.Anim[i].SetLayerWeight(1, 1);
                     }
                     else if (EntityManager.HasComponent<RightGripTag>(_itemData._Entity[j]) &&
                              !_playerData.Lists[i].Equiables.Contains(_itemData.EquimentItem[j]))
                     {
                         //release the shield
                         Anims.Anim[i].SetLayerWeight(1, 0);  
                     }
                 }
             }
         }
     }
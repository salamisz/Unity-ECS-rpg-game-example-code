using DefaultNamespace;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class EquimentUpdateSystem : ComponentSystem
     {
         private struct Data
         {
             public readonly int Length;
             public ComponentArray<InventoryList> Lists;
         }
         private struct ButtonData
         {
             public readonly int Length;
             public ComponentArray<Button> Buttons;
             [ReadOnly] public ComponentDataArray<ButtonTag> Tag;
             public EntityArray _EntityY;
         }
         private struct ImageData
         {
             public readonly int Length;
             public ComponentArray<Image> Images;
             [ReadOnly] public ComponentDataArray<UiTag> Tags;
             public EntityArray _Entity;
         }
         private struct EquipmentData
         {
             public readonly int Length;
             public ComponentArray<Equiable> EquimentItem;
            [ReadOnly] private SubtractiveComponent<DynamicEquipmentTag> DynamicTag;
             public EntityArray _EEntity;
         }
         private struct ArmorMeshData
         {
             [ReadOnly] private ComponentDataArray<ArmorTag> Tagss;
             [ReadOnly] private SubtractiveComponent<DynamicMeshTag> MeshTags;
             public EntityArray _EntitY;
         }
         
         [Inject] private ArmorMeshData _armorMeshData;
         [Inject] private Data _data;
         [Inject] private ButtonData _buttonData;
         [Inject] private ImageData _imageData;
         [Inject] private EquipmentData _equipmentData;
         
         protected override void OnUpdate()
         {
             for (int i = 0; i < _data.Length; i++)
             {
                 for (int j = 0; j < _equipmentData.Length; j++)
                 {
                     if (_data.Lists[i].Equiables.Count < 6)
                     {
                         if (_data.Lists[i].Equiables.Contains(_equipmentData.EquimentItem[j]))
                         {
                             if (EntityManager.HasComponent<DynamicImageTag>(_imageData._Entity[j]) &&
                                 EntityManager.HasComponent<DynamicButtonTag>(_buttonData._EntityY[j]))
                             {
                                 if (!EntityManager.HasComponent<DynamicEquipmentTag>(_equipmentData._EEntity[j])
                                  && !EntityManager.HasComponent<DynamicMeshTag>(_armorMeshData._EntitY[j]))
                                 {
                                     //since the Equipment System does not have anything in its OnUpdate method i've moved the tags to a different system for cleaner code
                                     PostUpdateCommands.RemoveComponent<DynamicImageTag>(_imageData._Entity[j]);
                                     PostUpdateCommands.RemoveComponent<DynamicButtonTag>(_buttonData._EntityY[j]);
                                     //this tag tells the entityManager that this Skin is taken
                                     PostUpdateCommands.AddComponent(_armorMeshData._EntitY[j], new DynamicMeshTag());
                                     //this tag tells that the item equipped can not be equipped once more
                                     PostUpdateCommands.AddComponent(_equipmentData._EEntity[j], new DynamicEquipmentTag());
                                 }
                             }
                         }
                     }
                 }
             }
         }
     }
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class EquipmentSystem : ComponentSystem
    {
        public static EquipmentSystem Instance;
        
        protected override void OnStartRunning()
        {
            Instance = this;
        }

        private struct Data
        {
            public readonly int Length;
            public ComponentArray<InventoryList> Lists;
            public ComponentDataArray<ModifierComponent> CModify;
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
            public ComponentArray<Selected> Selections;
            public ComponentArray<MeshFilter> Filter;
            public ComponentArray<MeshRenderer> Render;
            public ComponentDataArray<ItemStatComponent> Stats;
            [ReadOnly]private SubtractiveComponent<DynamicEquipmentTag> DynamicTag;
            public EntityArray _EEntity;
        }
        private struct ArmorMeshData
        {
            public ComponentArray<SkinnedMeshRenderer> ArmorMesh;
            [ReadOnly] private SubtractiveComponent<DynamicMeshTag> MeshTags;
            [ReadOnly] private ComponentDataArray<ArmorTag> Tagss;
            public EntityArray _EntitY;
        }

        [Inject] private ArmorMeshData _armorMeshData;
        [Inject] private Data _data;
        [Inject] private ButtonData _buttonData;
        [Inject] private ImageData _imageData;
        [Inject] private EquipmentData _equipmentData;

        public void EquipItem()
        {
            for (int i = 0; i < _data.Length; i++)
            {
               var Modifyer = _data.CModify[i];
                for (int j = 0; j < _equipmentData.Length; j++)
                {
                    
                    if (_data.Lists[i].Equiables.Count < 6 && _imageData.Images[j].sprite != null)
                    {                            
                            if (!_data.Lists[i].Equiables.Contains(_equipmentData.EquimentItem[j]))
                            {
                                //disables the item image
                                _imageData.Images[j].enabled = false;
                                //disables interaction on the remove button
                                _buttonData.Buttons[j].interactable = false;
                                //set the current sprite to empty
                                _imageData.Images[j].sprite = null;  
                                //adds the chosen Equipable to the equipables list 
                                _data.Lists[i].Equiables.Add(_equipmentData.EquimentItem[j]);
                                //revomes a chosen selectable frm the list
                                _data.Lists[i].Items.Remove(_equipmentData.Selections[j]);

                                //sets the items mesh
                                _armorMeshData.ArmorMesh[j].sharedMesh = _equipmentData.Filter[j].sharedMesh;
                                //sets the items materials
                                _armorMeshData.ArmorMesh[j].sharedMaterials = _equipmentData.Render[j].sharedMaterials;
                                //sets modifiers    
                                Modifyer.AttackModifier = _equipmentData.Stats[j].AttackDamage;
                                Modifyer.ProtectionModifier = _equipmentData.Stats[j].protection;

                                if (Modifyer.ProtectionModifier != 0)
                                    {
                                        //add modifiers to the modifier list
                                        _data.Lists[i].ProtectionModifiers.Add(Modifyer.ProtectionModifier);
                                        //adds modifier to the final value
                                            Modifyer.FinalProtectionModifier += _equipmentData.Stats[j].protection;
                                            _data.CModify[i] = Modifyer;
                                    }

                                    if (Modifyer.AttackModifier != 0)
                                    {
                                        //add modifiers to the modifier list
                                           _data.Lists[i].AttackModifiers.Add(Modifyer.AttackModifier);
                                        //adds modifier to the final value
                                            Modifyer.FinalAttackModifier += _equipmentData.Stats[j].AttackDamage;
                                            _data.CModify[i] = Modifyer;
                                    }
                                }
                            
                    }
                }
            }
        }

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
                                     //remove the tags so new items can be placed in the same place
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
}
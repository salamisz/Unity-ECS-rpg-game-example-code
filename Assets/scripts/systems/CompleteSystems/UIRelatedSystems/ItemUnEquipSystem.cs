using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    [UpdateAfter(typeof(EquipmentSystem))]
    public class ItemUnEquipSystem : ComponentSystem
    {
        private struct PlayerData
        {
            public readonly int Length;
            public ComponentArray<InventoryList> Lists;
            public ComponentDataArray<ModifierComponent> CModify;
        }

        private struct ImageData
        {
            public readonly int Length;
            [ReadOnly] private ComponentDataArray<UiTag> Tags;
            public ComponentArray<Image> Images;
            public EntityArray _Entity;
        }

        private struct ButtonData
        {
            public readonly int Length;
            public ComponentArray<Button> RemoveButt;
            [ReadOnly] private ComponentDataArray<ButtonTag> ButtonTags;
            public EntityArray _EntityY;
        }
        private struct Data
        {
            public readonly int Length;
            public ComponentArray<Equiable> Equiables;
            public ComponentArray<Selected> selection;
            [ReadOnly]public SharedComponentDataArray<SpriteComponent> Sprites;
            public ComponentDataArray<ItemStatComponent> Stats;
            public EntityArray _EEntity;
        }
        private struct ArmorMeshData
        {
            public readonly int Length;
            public ComponentArray<SkinnedMeshRenderer> ArmorMesh;
            [ReadOnly] private ComponentDataArray<ArmorTag> Tags;
            public EntityArray _EntitY;

        }
        
        [Inject] private ArmorMeshData _armorMeshData;
        [Inject] private PlayerData _playerData;
        [Inject] private ImageData _imageData;
        [Inject] private ButtonData _buttonData;
        [Inject] private Data _data;
        
        protected override void OnUpdate()
        {
            for (int i = 0; i < _playerData.Length; i++)
            {
                var Modifyer = _playerData.CModify[i];
                for (int j = 0; j < _data.Length; j++)
                {
                    if (Input.GetKeyDown(KeyCode.U) && _playerData.Lists[i].Equiables.Count != 0)
                    {
                        if (!EntityManager.HasComponent<DynamicImageTag>(_imageData._Entity[j]) &&
                            !EntityManager.HasComponent<DynamicButtonTag>(_buttonData._EntityY[j]))
                        {
                            if (EntityManager.HasComponent<DynamicEquipmentTag>(_data._EEntity[j])
                                && EntityManager.HasComponent<DynamicMeshTag>(_armorMeshData._EntitY[j]))
                            {
                                //this system basically reverses what the InventoryAddSystem and the EquipmentUpdateSystem and the EquipmentSystem have done

                                //if you have not been in the InventoryAddSystem AND the EquipmentUpdateSystem you will not know what these tags are for
                                PostUpdateCommands.AddComponent(_imageData._Entity[j] ,new DynamicImageTag());
                                PostUpdateCommands.AddComponent(_buttonData._EntityY[j] ,new DynamicButtonTag());
                                PostUpdateCommands.RemoveComponent<DynamicMeshTag>(_armorMeshData._EntitY[j]);
                                PostUpdateCommands.RemoveComponent<DynamicEquipmentTag>(_data._EEntity[j]);

                                _imageData.Images[j].sprite = _data.Sprites[j].Icon;
                                _playerData.Lists[i].Items.Add(_data.selection[j]);
                                _playerData.Lists[i].Equiables.Remove(_data.Equiables[j]);
                                _imageData.Images[j].enabled = true;
                                _buttonData.RemoveButt[j].interactable = true;
                                _armorMeshData.ArmorMesh[j].sharedMesh = null;
                                Modifyer.AttackModifier = _data.Stats[j].AttackDamage;
                                Modifyer.ProtectionModifier = _data.Stats[j].protection;
                                if (Modifyer.ProtectionModifier != 0)
                                {
                                    _playerData.Lists[i].ProtectionModifiers.Clear();
                                    Modifyer.FinalProtectionModifier = 5;
                                    _playerData.CModify[i] = Modifyer;
                                }

                                if (Modifyer.AttackModifier != 0)
                                {
                                    _playerData.Lists[i].AttackModifiers.Clear();
                                    Modifyer.FinalAttackModifier = 2;
                                    _playerData.CModify[i] = Modifyer;
                                }

                            }
                        }
                    }
                }
            
            }
        }
    }
}
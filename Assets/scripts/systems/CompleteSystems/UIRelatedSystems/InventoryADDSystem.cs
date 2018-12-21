using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    [UpdateAfter(typeof(InterctionSystem))]
    public class InventoryADDSystem : ComponentSystem
    {
        
        private struct Data
        {
            public readonly int Length;
            public ComponentArray<GizmoDraw> GizmoLines;
            public ComponentArray<Selected> Seletions;
            public ComponentArray<Transform> InterTransform;
            [ReadOnly]public SharedComponentDataArray<SpriteComponent> Sprites;
            public ComponentArray<MeshRenderer> Mesh;
            public EntityArray _entity;

        }
        private struct PlayerData
        {
            public readonly int Length;
            public ComponentDataArray<FacingComponent> Face;
            public ComponentArray<Transform> PlayerTransform;
            public ComponentDataArray<InventoryComponent> Spaces;
            public ComponentArray<InventoryList> List;
        }

        private struct ImageData
        {
            [ReadOnly] private ComponentDataArray<UiTag> Tags;
            [ReadOnly]SubtractiveComponent<DynamicImageTag> Dynamictag;
            public ComponentArray<Image> Images;
            public EntityArray _Entity;
        }

        private struct ButtonData
        {
            public ComponentArray<Button> RemoveButt;
            [ReadOnly] private ComponentDataArray<ButtonTag> ButtonTags;
            [ReadOnly]SubtractiveComponent<DynamicButtonTag> Dynamictags;
            public EntityArray _EntityY;
        }

        [Inject] private PlayerData _playerData;
        [Inject] private Data _data;
        [Inject] private ImageData _imageData;
        [Inject] private ButtonData _buttonData;

        protected override void OnUpdate()
        {            
            for (int i = 0; i < _playerData.Length; i++)
            {
                var facingForDistance = _playerData.Face[i];

                for (int j = 0; j < _data.Length; j++)
                {
                    //for some reason the distance does not save when writen to so it need to be calculated for each system using distance separately
                    facingForDistance.distance = Vector3.Distance(_playerData.PlayerTransform[i].position, _data.InterTransform[j].position
                        );
                    if (Input.GetMouseButtonDown(1))
                    {

                        if (facingForDistance.distance <= _data.GizmoLines[j].radius)
                        {

                            if (_playerData.List[i].Items.Count < _playerData.Spaces[i].space)
                            {
                                    if (_imageData.Images[j].sprite == null)
                                    {
                                        if (!EntityManager.HasComponent<DynamicImageTag>(_imageData._Entity[j]) &&
                                            !EntityManager.HasComponent<DynamicButtonTag>(_buttonData._EntityY[j]))
                                        {
                                            //this tag prevent from the image being changed
                                            PostUpdateCommands.AddComponent(_imageData._Entity[j], new DynamicImageTag());
                                                
                                            //this prevent from the remove button being changed
                                            PostUpdateCommands.AddComponent(_buttonData._EntityY[j], new DynamicButtonTag());
                                                
                                            //Allows interaction whit the remove button
                                            _buttonData.RemoveButt[j].interactable = true;
                                            //Sets the sprite
                                            _imageData.Images[j].sprite = _data.Sprites[j].Icon;
                                            //Adds a Selectible to the list
                                            _playerData.List[i].Items.Add(_data.Seletions[j]);
                                            // enables the sprite
                                            _imageData.Images[j].enabled = true;
                                            // disables the items visuals
                                            _data.Mesh[j].enabled = false;
                                            // removes the interaction component so item could not be picked up again
                                            PostUpdateCommands.RemoveComponent<GizmoDraw>(_data._entity[j]);
                                        }
                                    }

                                
                            }
                        
                        }
                      }                        
                   
                }
            }
        }
     }
 }
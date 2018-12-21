using Unity.Collections;
using Unity.Entities;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class RemoveItemsSystem : ComponentSystem
    {
        public static RemoveItemsSystem Instance;
                
        protected override void OnStartRunning()
        {
            Instance = this;
        }
        
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
        private struct SelectedData
        {
            public readonly int Length;
            public ComponentArray<Selected> Selecteds;
        }

        [Inject] private Data _data;
        [Inject] private ButtonData _buttonData;
        [Inject] private ImageData _imageData;
        [Inject] private SelectedData _selectedData;

        public void RemoveItem()
        {
            for (int i = 0; i < _data.Length; i++)
            {
                for (int j = 0; j < _selectedData.Length; j++)
                {
                    _imageData.Images[j].enabled = false;
                    _buttonData.Buttons[j].interactable = false;
                    _imageData.Images[j].sprite = null;
                    _data.Lists[i].Items.Remove(_selectedData.Selecteds[j]);
                }
            } 
        }

        protected override void OnUpdate()
        {
                for (int j = 0; j < _selectedData.Length; j++)
                {
                            if (EntityManager.HasComponent<DynamicImageTag>(_imageData._Entity[j]) &&
                                EntityManager.HasComponent<DynamicButtonTag>(_buttonData._EntityY[j]))
                            {
                                if (_imageData.Images[j].sprite == null)
                                {
                                    //allows items to got in the slot that was previously taken
                                    PostUpdateCommands.RemoveComponent<DynamicImageTag>(_imageData._Entity[j]);
                                    PostUpdateCommands.RemoveComponent<DynamicButtonTag>(_buttonData._EntityY[j]);
                                }
                            }
                        
                    
                }
            
        }
    }
}
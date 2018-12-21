using Unity.Entities;
using UnityEngine;

namespace DefaultNamespace
{
    public class InventoryOpenSystem : ComponentSystem
    {
        private struct Data
        {
            public readonly int Length;
            public ComponentDataArray<InventoryOpenTag> Tags;
            public ComponentArray<Canvas> Canvis;
        }

        [Inject] private Data _data;
        
        protected override void OnUpdate()
        {
            for (int i = 0; i < _data.Length; i++)
            {
                if (Input.GetButtonDown("InventoryOpen"))
                {
                    //enables the inventory
                    _data.Canvis[i].enabled = true;
                }

                if (Input.GetButtonDown("Cancel"))
                {
                    //disables the inventory
                    _data.Canvis[i].enabled = false;
                }
            }
        }
    }
}
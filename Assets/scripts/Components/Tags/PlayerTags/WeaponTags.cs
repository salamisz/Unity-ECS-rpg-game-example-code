using Unity.Entities;
using System;

namespace DefaultNamespace
{
    public struct WeaponTag : IComponentData
   {  
   }
   public class WeaponTags : ComponentDataWrapper<WeaponTag>{}
}
using Unity.Entities;
using UnityEngine;

namespace ShiftFeature
{
    public class ShiftAuthoring : MonoBehaviour
    {
        public float ShiftValue;
        public float ShiftCooldown;
        private class ShiftAuthoringBaker : Baker<ShiftAuthoring>
        {
            public override void Bake(ShiftAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new ShiftComponent
                {
                    ShiftValue = authoring.ShiftValue,
                    ShiftCooldown = authoring.ShiftCooldown
                });
            }
        }
    }

    public struct ShiftComponent : IComponentData
    {
        public float ShiftValue;
        public float ShiftCooldown;
        public float NextShiftTime;
    }
}
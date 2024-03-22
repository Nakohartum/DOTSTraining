using Unity.Entities;
using UnityEngine;

namespace ECSTutotial
{
    public class SpeedAuthoring : MonoBehaviour
    {
        public float Speed;
        private class SpeedAuthoringBaker : Baker<SpeedAuthoring>
        {
            public override void Bake(SpeedAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new SpeedData
                {
                    Speed = authoring.Speed
                });
            }
        }
    }

    public struct SpeedData : IComponentData
    {
        public float Speed;
    }
}
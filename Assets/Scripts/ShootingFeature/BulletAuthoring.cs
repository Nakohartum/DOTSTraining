using Interfaces;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace ECSTutotial
{
    public class BulletAuthoring : MonoBehaviour
    {
        public GameObject BulletPrefab;
        public float ShootingRate;
        private class BulletAuthoringBaker : Baker<BulletAuthoring>
        {
            public override void Bake(BulletAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new BulletData
                {
                    BulletPrefab = GetEntity(authoring.BulletPrefab, TransformUsageFlags.Dynamic),
                    ShootingRate = authoring.ShootingRate
                });
            }
        }
    }

    public struct BulletData : IComponentData
    {
        public Entity BulletPrefab;
        public float ShootingRate;
        public float NextShootingTime;
    }
}
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ECSTutotial
{
    public class UserInputAuthoring : MonoBehaviour
    {
        private class UserInputAuthoringBaker : Baker<UserInputAuthoring>
        {
            public override void Bake(UserInputAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
                AddComponent(entity, new UserInputData());
            }
        }
    }

    public struct UserInputData : IComponentData
    {
        public float2 Move;
        public float Shoot;
        public float Shift;
    }
}
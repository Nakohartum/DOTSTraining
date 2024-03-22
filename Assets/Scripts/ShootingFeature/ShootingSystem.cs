using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace ECSTutotial
{
    public partial struct ShootingSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {

            var ecb = 
                SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();

            new ShootingJob
            {
                ECB = ecb,
                ElapsedTime = (float)SystemAPI.Time.ElapsedTime
            }.ScheduleParallel();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }

    public partial struct ShootingJob : IJobEntity
    {
        public float ElapsedTime;
        public EntityCommandBuffer.ParallelWriter ECB;
        public void Execute([ChunkIndexInQuery] int chunkIndex, ref BulletData bulletData, in UserInputData inputData)
        {
            if (inputData.Shoot > 0 && ElapsedTime > bulletData.NextShootingTime)
            {
                Entity spawnedBullet = ECB.Instantiate(chunkIndex, bulletData.BulletPrefab);
                bulletData.NextShootingTime = ElapsedTime + bulletData.ShootingRate;
            }
        }
    }
}
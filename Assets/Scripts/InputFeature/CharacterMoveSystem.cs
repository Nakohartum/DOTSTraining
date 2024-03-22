using ShiftFeature;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECSTutotial
{
    public partial struct CharacterMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var movePlayerJob = new MovePlayerJob
            {
                DeltaTime = SystemAPI.Time.DeltaTime,
                ElapsedTime = (float)SystemAPI.Time.ElapsedTime
            };

            movePlayerJob.Schedule();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
    
    public partial struct MovePlayerJob : IJobEntity
    {
        public float DeltaTime;
        public float ElapsedTime;
        public void Execute(ref LocalTransform localTransform, in UserInputData inputData, 
            in SpeedData speedData, ref ShiftComponent shiftData)
        {
            float3 moveVector;
            if (inputData.Shift > 0 && shiftData.NextShiftTime < ElapsedTime)
            {
                moveVector = new float3(0, 0, shiftData.ShiftValue);
                localTransform = localTransform.Translate(moveVector * DeltaTime);
                shiftData.NextShiftTime = ElapsedTime + shiftData.ShiftCooldown;
            }
            else
            {
                var inputValue = inputData.Move;
                moveVector = new float3(inputValue.x, 0, inputValue.y);
                localTransform = localTransform.Translate(moveVector * DeltaTime * speedData.Speed);
            }
            
        }
    }
}
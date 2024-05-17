using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct CameraFollowSystem : ISystem
{
    
    public void OnUpdate(ref SystemState state)
    {
        foreach (var locToWo in SystemAPI.Query<RefRO<LocalToWorld>>().WithAll<Collector>())
        {
            Camera.Instance.transform.position = Vector3.Lerp(Camera.Instance.transform.position, locToWo.ValueRO.Position + -locToWo.ValueRO.Forward * 5, Camera.Instance.lerp.x);
            //Camera.Instance.transform.LookAt(locToWo.ValueRO.Position);

            //Debug.Log(-locToWo.ValueRO.Position * 5 * SystemAPI.Time.DeltaTime);
            /*
            var distancePos = Camera.Instance.distancePosition;
            var camPos = Camera.Instance.transform.position;
            var lerpValues = Camera.Instance.lerp;
            var fwrDistance = new float3();

            fwrDistance.x = Mathf.Lerp(camPos.x, locToWo.ValueRO.Position.x + (-locToWo.ValueRO.Forward.x * distancePos.x), lerpValues.x);
            fwrDistance.y = Mathf.Lerp(camPos.y, locToWo.ValueRO.Position.y + (-locToWo.ValueRO.Forward.y * distancePos.y), lerpValues.y);
            fwrDistance.z = Mathf.Lerp(camPos.z, locToWo.ValueRO.Position.z + (-locToWo.ValueRO.Forward.z * distancePos.z), lerpValues.z);

            Camera.Instance.transform.position = fwrDistance;
            */
            Camera.Instance.transform.LookAt(locToWo.ValueRO.Position);
            
        }
    }
}

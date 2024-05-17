
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CollectorAuthoring : MonoBehaviour
{
    [SerializeField]
    private float forceMovement;
    [SerializeField]
    private float forceSteer; 
    [SerializeField]
    private Transform Offset;
    public class Baker : Baker<CollectorAuthoring>
    {
        public override void Bake(CollectorAuthoring authoring)
        {
            AddComponent(GetEntity(TransformUsageFlags.Dynamic), new Collector() {
                forceMovement = authoring.forceMovement,
                forceSteer = authoring.forceSteer,
                Offset = authoring.Offset.localPosition,
            });
        }
    }
}
public struct Collector : IComponentData
{
    public float3 Offset;
    public float forceMovement;
    public float forceSteer;
}
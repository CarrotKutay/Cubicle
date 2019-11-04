using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCube : MonoBehaviour
{
    float Value;

    GameObject Body;
    Rigidbody RigidBody;
    BoxCollider HealthCubeCollider;
    Light PointLightGlow;
    Renderer HealthCubeRenderer;

    Shader Shader;
    Material HealthCubeMaterial;
    
    void Init(int BodyScaleValue, Vector3 HitPosition)
    {
        Value = 0.5f * (BodyScaleValue / 25f); // 0.5 = size of a normal Cube character = 100 % Health
        Debug.Log(Value);
        
        if (HealthCubeMaterial != null) Debug.Log("Loaded successfully");
        
        //adding cube
        Body = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Body.transform.position = HitPosition;
        Body.transform.parent = transform;

        //scaling gameObject
        transform.localScale = new Vector3(Value, Value, Value);

        //adding components
        gameObject.AddComponent<Rigidbody>();
        gameObject.AddComponent<BoxCollider>();
        gameObject.AddComponent<Light>();
        gameObject.AddComponent<MeshRenderer>();

        //material setup
        HealthCubeRenderer = Body.GetComponent<MeshRenderer>();
        HealthCubeRenderer.enabled = true;
        HealthCubeRenderer.sharedMaterial = HealthCubeMaterial;
        //Rigidbody
        RigidBody = GetComponent<Rigidbody>();
        RigidBody.constraints = RigidbodyConstraints.FreezePositionZ;
        //Collider
        HealthCubeCollider = GetComponent<BoxCollider>();
        //Light
        PointLightGlow = GetComponent<Light>();
        PointLightGlow.type = LightType.Point;
        PointLightGlow.color = new Color(54/256f, 170/256f, 10/256f);
        PointLightGlow.range = 4;
}

    private void Start()
    {
        HealthCubeMaterial = Resources.Load<Material>("TranslucentMat_green");
        Init(10, new Vector3(0,-2,0));
    }
}

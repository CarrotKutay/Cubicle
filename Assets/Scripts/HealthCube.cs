using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCube : MonoBehaviour
{
    int Value;
    float Scale;

    GameObject Body;
    Rigidbody RigidBody;
    BoxCollider HealthCubeCollider;
    Light PointLightGlow;
    Renderer HealthCubeRenderer;

    Shader Shader;
    Material HealthCubeMaterial;
    
    void Init(int Value, Vector3 HitPosition)
    {
        this.Value = Value;
        Scale = 0.5f * (Value / 25f); // 0.5 = size of a normal Cube character = 100 % Health
        Debug.Log(Value);
        
        if (HealthCubeMaterial != null) Debug.Log("Loaded successfully");
        
        //adding cube
        Body = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Body.transform.position = HitPosition;
        Body.transform.parent = transform; // remove once auto generation is possible

        //scaling gameObject
        transform.localScale = new Vector3(Scale, Scale, Scale);

        //adding components
        Body.AddComponent<BoxCollider>();
        Body.AddComponent<Light>();
        Body.AddComponent<MeshRenderer>();
        
        //material setup
        HealthCubeRenderer = Body.GetComponent<MeshRenderer>();
        HealthCubeRenderer.enabled = true;
        HealthCubeRenderer.sharedMaterial = HealthCubeMaterial;
        //Collider
        HealthCubeCollider = GetComponent<BoxCollider>();
        //Light
        PointLightGlow = Body.GetComponent<Light>();
        PointLightGlow.transform.parent = Body.transform;
        PointLightGlow.transform.localPosition = Vector3.zero;
        PointLightGlow.type = LightType.Point;
        PointLightGlow.color = new Color(54/256f, 170/256f, 10/256f);
        PointLightGlow.range = 4;

        //adding RigidBody
        gameObject.AddComponent<Rigidbody>();
        RigidBody = GetComponent<Rigidbody>();
        RigidBody.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    private void Start()
    {
        HealthCubeMaterial = Resources.Load<Material>("TranslucentMat_green");
        Init(10, new Vector3(0,-2,0));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.GetComponent<Character>() != null)
        {
            GameObject Player = collision.collider.gameObject;
            Player.GetComponent<Character>().UpdateHealth(Value);
            Destroy(this.Body);
        }
    }
}

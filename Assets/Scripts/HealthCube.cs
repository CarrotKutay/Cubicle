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

    void Init(int Value)
    {
        this.Value = Value;
        Scale = 0.5f * (Value / 25f); // 0.5 = size of a normal Cube character = 100 % Health
        Debug.Log(Value);

        //adding cube
        Body = gameObject;
        Body.transform.parent = transform; // remove once auto generation is possible

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
        PointLightGlow.color = new Color(54 / 256f, 170 / 256f, 10 / 256f);
        PointLightGlow.range = 4;

        //adding RigidBody
        RigidBody = GetComponent<Rigidbody>();
        RigidBody.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    private void Start()
    {
        HealthCubeMaterial = Resources.Load<Material>("TranslucentMat_green");
        Init(10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.GetComponent<Character>() != null)
        {
            GameObject Player = collision.collider.gameObject;
            Player.GetComponent<Character>().UpdateHealth(Value);
            Destroy(Body);
            SpawnManager manager = GameObject.FindGameObjectWithTag("ArenaManager").GetComponent<SpawnManager>();
            manager.ObjectList.Remove(Body);
        }
    }
}

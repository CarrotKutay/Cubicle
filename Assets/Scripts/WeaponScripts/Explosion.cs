using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Vector3 HitPosition, CubesPivot;
    private float CubesPivotDistance;
    private float CubePieceSize = 0.09f;
    private int PieceSize = 5; // 1 Dimension
    private float ExplosionForce = 70f;

    // Start is called before the first frame update
    void Start()
    {
        //calculate pivot distance
        CubesPivotDistance = CubePieceSize * PieceSize / 2;
        CubesPivot = new Vector3(CubesPivotDistance, CubesPivotDistance, CubesPivotDistance);
    }

    public void Explode(Vector3 HitPosition)
    {
        this.HitPosition = HitPosition;
        
        //loop creating explosion particles

        for(int y = 0; y < PieceSize; y++)
        {
            for(int x = 0; x < PieceSize; x++)
            {
                for(int z = 0; z < PieceSize; z++)
                {
                    CreatePieces(x, y, z);
                }
            }
        }

        //Add explosion
        Collider[] colliders = Physics.OverlapSphere(HitPosition, PieceSize);
        foreach(Collider c in colliders)
        {
            Rigidbody rb = c.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(ExplosionForce, HitPosition, PieceSize, 0.4f);
            }
        }
    }

    private void CreatePieces(int x, int y, int z)
    {
        GameObject Piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Piece.AddComponent<ExplosionParticle>();

        //set pieces position and scale
        Piece.transform.position = HitPosition + new Vector3(CubePieceSize * x, CubePieceSize * y, CubePieceSize * z) - CubesPivot;
        Piece.transform.localScale = new Vector3(CubePieceSize, CubePieceSize, CubePieceSize);

        //add Rigidbody and setup mass
        Piece.AddComponent<Rigidbody>();
        Piece.GetComponent<Rigidbody>().mass = 0.2f;
    }
}

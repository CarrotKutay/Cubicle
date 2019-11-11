using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticle : MonoBehaviour
{
    private Material ParticleMaterial;
    private MeshRenderer rend;

    private void Start()
    {
        ParticleMaterial = Resources.Load<Material>("ProjectileMaterial");
        rend = GetComponent<MeshRenderer>();
        rend.material = ParticleMaterial;
        StartCoroutine(cleanup());
    }

    /**
        ** cleaning up explosion particles */
    private IEnumerator cleanup()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(this.gameObject);
    }

}

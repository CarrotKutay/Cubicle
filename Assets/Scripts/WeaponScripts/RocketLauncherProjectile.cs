using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherProjectile : Projectile
{
    public Vector3 ScaleSize;
    private bool exploded = false;

    public void BuildProjectile()
    {
        Init();
        //* scaling projectile
        ScaleSize = new Vector3(0.2f, 0.1f, 0.1f);
        gameObject.transform.localScale = ScaleSize;
        //* add explosive radius to projectile
        ExplosionRadius = 5f;
        //* adding explosive behaviour to rocketlauncher projectiles */
        gameObject.AddComponent<Explosion>();
    }
    /**
        **  Checking for Collision with a target
        **  After Target was hit projectile will be cleaned up to not clutter scene
        */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.GetMask("Player1"))
        {
            if (!exploded)
            {
                exploded = true;
                //adding explosion at point of collision
                Vector3 collisionPoint = collision.GetContact(0).point;
                GetComponent<Explosion>().Explode(collisionPoint);

                //* Adding a Triggerfield which checks for player objects inside of it
                GameObject field = new GameObject();
                field.transform.position = collisionPoint;
                field.AddComponent<TriggerField>();
                field.GetComponent<TriggerField>().Init(Damage, ExplosionRadius);

                cleanup();
            }
        }
    }

    class TriggerField : MonoBehaviour
    {
        private List<GameObject> objectsInField;
        private SphereCollider sc;
        private int DamageValue;

        public void Init(int DamageValue, float radius)
        {
            this.gameObject.AddComponent<SphereCollider>();
            sc = GetComponent<SphereCollider>();
            sc.isTrigger = true;
            sc.radius = radius > 1 ? 1 : radius;
            objectsInField = new List<GameObject>();
            this.DamageValue = DamageValue;
        }

        private void OnTriggerEnter(Collider other)
        {
            //only call for a damage update for objects that are players and have not been updated yet
            if (other.gameObject.CompareTag("Player") && !objectsInField.Contains(other.gameObject))
            {
                objectsInField.Add(other.gameObject);
                Character profile = other.gameObject.GetComponent<Character>();
                profile.UpdateHealth(-DamageValue);
            }
        }

        protected IEnumerator waitToClean()
        {
            yield return new WaitForSeconds(1.5f);
            Destroy(this.gameObject);
        }
        private void Start()
        {
            StartCoroutine(waitToClean());
        }

    }

}

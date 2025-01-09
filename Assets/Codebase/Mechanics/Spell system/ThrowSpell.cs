using System.Collections;
using UnityEngine;
using Mechanics.MoveSystem;

namespace Mechanics.SpellSystem
{
    [RequireComponent(typeof(Fly))]
    public class ThrowSpell : MonoBehaviour, ISpell
    {
        private Transform _target=null;

        [SerializeField] 
        private float _lifeTime = 3f;

        [SerializeField] 
        private float _reloadTime = 3f;
        public float ReloadTime
        {
            get { return _reloadTime; }
        }

        private Transform FindTarget()
        { 
            float searchRadius = 10f;
            LayerMask enemyLayer = LayerMask.GetMask("Enemy");
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, searchRadius, enemyLayer);
            Debug.Log(hits.Length);
            float minDistance = Mathf.Infinity;
            Transform closest = null;

            foreach (var hit in hits)
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = hit.transform;
                }
            }

            return closest;
        }

        public void Cast()
        {
            StartCoroutine(StartLifeTime(_lifeTime));
            _target = FindTarget();
        }

        private void Update()
        {
            Vector2 direction = _target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            GetComponent<Fly>().Turn(direction.normalized);
        }

        private IEnumerator StartLifeTime(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }
    }
}



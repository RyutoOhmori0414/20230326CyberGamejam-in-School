using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    [SerializeField]
    GameObject _decal;

    ParticleSystem _particleSystem;
    List<ParticleCollisionEvent> _events = new List<ParticleCollisionEvent>();

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        _particleSystem.GetCollisionEvents(other, _events);

        foreach (var n in _events)
        {
            var obj = Instantiate(_decal);

            obj.transform.position = n.intersection;
            obj.transform.forward = -n.normal;
        }
    }
}

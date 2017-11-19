using UnityEngine;
using UnityEngine.AI;

public class UnitAnimator : MonoBehaviour
{
    private NavMeshAgent m_navAgent;
    private Animator m_animator;
    
    private void Awake()
    {
        m_navAgent = GetComponentInParent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        m_animator.SetFloat("Speed", m_navAgent.velocity.magnitude);
    }

    [BitStrap.Button]
    private void PlayShootAnimation()
    {
        m_animator.Play("Shoot", 0);
    }
}

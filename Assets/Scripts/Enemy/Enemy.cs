using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float coolDownTimer = Mathf.Infinity;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        coolDownTimer += Time.deltaTime;

        if(PlayerInSight())
        {
            if(coolDownTimer >= attackCoolDown)
            {
                Attack();
                coolDownTimer = 0f;
                animator.SetTrigger("attack");
            }
        }
    }

    bool PlayerInSight()
    {
        Vector2 boxSize = new Vector2(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y);
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            boxSize, 
            0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }


    void Attack()
    {
        // Perform attack logic here
    }

    private void OnDrawGizmos()
    {
        Vector3 boxSize = new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            boxSize);
    }

}

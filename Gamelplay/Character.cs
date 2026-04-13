using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    private bool busy;

    [SerializeField] private int hp;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackRange;
    [SerializeField] private List<Collectable> loot;

    [SerializeField] private float moveSpeed;

    private Rigidbody rb;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isRunning;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        animator = GetComponent<Animator>();
    }

    public void MoveTo(Vector3 point)
    {
        animator.SetBool("Walk", true);
        if (isRunning)
        {
            animator.SetBool("Run", true);
        }

        // move

        if (isRunning)
        {
            animator.SetBool("Run", false);
        }
        animator.SetBool("Walk", false);
    }
    private void Attack(Character target, int damage)
    {
        animator.SetBool("Attack", true);
        target.GetAttacked(damage);
        animator.SetBool("Attack", false);
    }
    public void GetAttacked(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        rb.linearVelocity = Vector3.zero;
        animator.SetTrigger("Death");
        DropLoot();
    }

    private void DropLoot()
    {
        foreach (var lootItem in loot)
        {
            lootItem.Drop(transform.position);
        }
    }


}

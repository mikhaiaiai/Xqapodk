using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    private int HP;
    public NavMeshAgent agent;

    [SerializeField] private TextMeshProUGUI damageTMP;
    public Animator animator;

    [SerializeField] private Item[] loot;
    public void TakeDamage(int damage)
    {
        HP -= damage;
        StartCoroutine(VisualiseDamage(damage));
        if (HP <= 0) Die();
    }
    private IEnumerator VisualiseDamage(int dmg)
    {
        damageTMP.text = "-" + dmg.ToString();
        damageTMP.enabled = true;
        yield return new WaitForSeconds(1);
        damageTMP.enabled = false;
    }

    public void Die()
    {
        animator.SetTrigger("Death");
        DropLoot();
    }

    public void MoveRandom()
    {
        float point1 = Random.value * 20 - 10;
        float point2 = Random.value * 20 - 10;

        MoveTo(new Vector3(point1, 0, point2));

    }
    public void MoveTo(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void DropLoot()
    {
        foreach(var lootItem in loot)
        {
            Instantiate(lootItem, transform.position, Quaternion.identity);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;

public class EnemyController : MonoBehaviour
{
    public delegate void Death(GameObject enemyType,EnemyController enemy);
    public event Death OnDeath;

    [SerializeField]
    private EnemyModel enemyModel;
    [HideInInspector]
    public NavMeshAgent navMesh;
    [SerializeField]
    private Transform eyesPosition;
    [SerializeField]
    private Transform PlayerHead;

    private bool seePlayer;
    private bool playerInAttackRange;
    private bool playerInStoppingDistance;

    private int waitTime=5;

    private Animator animator;
    private float detectionRadius { get { return enemyModel.detectionRadius; } }
    private float attackRange { get { return enemyModel.attackRange; } }
    private LayerMask layerMask { get { return enemyModel.layerMask; } }
    public Transform playerTransform { get { return enemyModel.playerTransform; } set { enemyModel.playerTransform = value; } }
    private float maxHealth { get { return enemyModel.maxHealth; } }


    [SerializeField]
    private float currentHealth;
    public float CurrentHealth { get { return currentHealth; } set {currentHealth = value; GetHit(); } }
    [SerializeField] public float currentPhysicalDMG;
    [SerializeField] public float currentMagicalDMG;
    [SerializeField] public float currentPhysicalArmor;
    [SerializeField] public float currentMagicalArmor;



    // Start is called before the first frame update
    void Start()
    {
        playerTransform = Player.instance.transform.Find("SteamVRObjects").Find("VRCamera").Find("FollowHead").Find("HeadCollider");
        Debug.Log(playerTransform);
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.stoppingDistance = enemyModel.stoppingDistance;
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        StartCoroutine("CheckStatus");
    }
    private IEnumerator CheckStatus()
    {
        while(true)
        {
            yield return new WaitForSeconds(waitTime);
            CheckPlayerStatus();
            CheckEnemyStatus();
        }        
    }
    private void CheckPlayerStatus()
    {
        CheckPlayerVisible();
        if (seePlayer)
        {
            CheckPlayerIsInRange();
            waitTime = 1;
        }
        else
        {
            waitTime = 5;
        }

        SetBools();
    }
    private void CheckPlayerVisible()
    {
        if (Physics.OverlapSphere(transform.position, detectionRadius, layerMask).Length > 0)
        {
            Debug.Log("wykryto colider gracza");
            //wykryto collider gracza
            RaycastHit hit = new RaycastHit();
            Physics.Raycast(eyesPosition.position, playerTransform.position - eyesPosition.position, out hit);
            Debug.DrawRay(eyesPosition.position, playerTransform.position  - eyesPosition.position,Color.red,detectionRadius+5);
            if (hit.collider == null) return;
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                seePlayer = true;
                Debug.Log("widzi gracza");
            }
            else
            {
                seePlayer = false;
                Debug.Log("nie widze gracza");
            }
        }
        else
        {
            seePlayer = false;
        }

    }
    private void CheckPlayerIsInRange()
    {
        float distanceBetweenPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceBetweenPlayer < attackRange)
        {
            playerInAttackRange = true;
        }
        else
        {
            playerInAttackRange = false;
        }

        if (distanceBetweenPlayer <= navMesh.stoppingDistance)
        {
            playerInStoppingDistance = true;
        }
        else
        {
            playerInStoppingDistance = false;
        }

    }
    private void SetBools()
    {
        animator.SetBool("seePlayer", seePlayer);
        animator.SetBool("playerInAttackRange",playerInAttackRange);
        animator.SetBool("playerInStoppingDistance", playerInStoppingDistance);
    }

    public void GetHit()
    {
        animator.SetTrigger("GetHit");
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    private void CheckEnemyStatus()
    {
        if(currentHealth<=0)
        {
            animator.SetBool("isAlive", false);
        }
    }

    public void Die()
    {
        StartCoroutine(setDeactive());
    }
    public IEnumerator setDeactive()
    {
        yield return new WaitForSeconds(5);
        OnDeath.Invoke(enemyModel.prefab,this);
        yield return 0;
    }

}

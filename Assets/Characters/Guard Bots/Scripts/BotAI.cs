using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BotAI : MonoBehaviour
{
    [SerializeField]
    private Transform botSprite;

    [SerializeField]
    private float moveRadius = 1f;

    [SerializeField]
    private float randomMoveSpeed = 1f;

    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField]
    private float attackRadius = 2;

    [SerializeField]
    private LineRenderer laser;

    private Vector2 startingPosition;
    private Vector2 targetPosition;

    private bool attackDisabled = false;

    [SerializeField]
    private UnityEvent OnFirstAttack;

    private static bool hasAttacked = false;

    private Rigidbody2D rb;
    [SerializeField]
    private UnityEvent onDestroyed;

    [SerializeField]
    private GameObject killFlash;

    private void Awake()
    {
        laser.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
    }

    private void OnEnable()
    {
        startingPosition = targetPosition = this.transform.position;
        StartCoroutine(DoAttackIteration());
        StartCoroutine(DoAttract());
    }

    private void Update()
    {
        DoRandomMotion();
        DoAttractMotion();
    }

    private void DoAttractMotion()
    {
        float distanceFromTarget = Vector2.Distance(this.transform.position, this.targetPosition);
        this.transform.position = Vector2.MoveTowards(this.transform.position, targetPosition, Time.deltaTime * Mathf.Max(distanceFromTarget * moveSpeed, moveSpeed));
    }

    private IEnumerator DoAttract()
    {
        while (Attractor.instance == null || !Attractor.instance.isActiveAndEnabled)
        {
            targetPosition = startingPosition;
            yield return null;
        }
        attackDisabled = true;
        //Vector2 targetDirection = ((Vector2)Attractor.instance.transform.position - (Vector2)this.transform.position).normalized;
        //float distance = Vector2.Distance(Attractor.instance.transform.position, this.transform.position);
        //targetPosition = (Vector2)Attractor.instance.transform.position +  targetDirection * (distance + 3f);

        int attractCount = 0;

        while (Attractor.instance != null && Attractor.instance.isActiveAndEnabled)
        {
            while (Vector2.Distance(targetPosition, this.transform.position) > 0.5f) yield return null;

            Vector2 directionToAttractor = -((Vector2)this.transform.position - (Vector2)Attractor.instance.transform.position).normalized;
            float distanceToAttractor = Vector2.Distance(this.transform.position, Attractor.instance.transform.position);
            targetPosition = (Vector2)this.transform.position + (Vector2)(Quaternion.AngleAxis(Random.Range(-5, 5), Vector3.forward) * directionToAttractor * Mathf.Max( Random.Range(distanceToAttractor, attractRadius), distanceToAttractor));

            attractCount++;

            if (attractCount >= 15)
            {
                killFlash.SetActive(true);
                yield return new WaitForSeconds(.1f);
                killFlash.SetActive(false);
                botSprite.localPosition = Vector2.zero;
                this.enabled = false;
                rb.simulated = true;
                onDestroyed?.Invoke();
                GetComponent<Animator>().enabled = false;
                GetComponent<Item>().enabled = true;
            }

            yield return null;
            //targetPosition = Attractor.instance.transform.position + Quaternion.AngleAxis(Random.Range(0,360), Vector3.forward) * Vector2.right * moveRadius*4;
        }

        yield return null;
        attackDisabled = false;
        StartCoroutine(DoAttract());
    }

    [SerializeField]
    private float attractRadius = 5f;

    private IEnumerator DoAttackIteration()
    {
        float distanceToCharacter = Vector2.Distance(botSprite.position, MainCharacter.instance.transform.position);
        if (distanceToCharacter < attackRadius && !attackDisabled)
        {
            laser.enabled = true;
            laser.positionCount = 2;
            laser.SetPositions(new Vector3[] { transform.InverseTransformPoint(botSprite.position), botSprite.InverseTransformPoint(MainCharacter.instance.transform.position) });

            Vector2 kick = new Vector2(botSprite.position.x < MainCharacter.instance.transform.position.x? 900:-900, 600);

            MainCharacter.instance.Kick(kick);

            yield return new WaitForSeconds(0.125f);
            laser.enabled = false;

            if (!hasAttacked)
                OnFirstAttack?.Invoke();
            hasAttacked = true;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(DoAttackIteration());
    }

    private float currentAngle = 0;
    private void DoRandomMotion()
    {
        Vector2 targetLocation = Quaternion.AngleAxis(currentAngle, Vector3.forward) * Vector2.right * moveRadius;

        float distanceFromTarget = Vector2.Distance(targetLocation, botSprite.localPosition);

        botSprite.localPosition = Vector2.MoveTowards(botSprite.localPosition, targetLocation, Time.deltaTime * Mathf.Max( distanceFromTarget * randomMoveSpeed, randomMoveSpeed));

        if (distanceFromTarget < 0.1f)
            currentAngle = Random.Range(0, 360);
    }
}

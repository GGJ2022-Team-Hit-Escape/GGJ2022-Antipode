using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform;

    [SerializeField]
    private float deadzone = 0;

    [SerializeField]
    private float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cameraPosition = this.transform.position;
        Vector2 targetTransformPosition = targetTransform.transform.position;

        float distanceFromCenter = Vector2.Distance(cameraPosition, targetTransformPosition);

        if (distanceFromCenter > deadzone)
        {
            float distanceFromDeadZoneEdge = distanceFromCenter - deadzone;
            Vector3 cameraTargetPosition = new Vector3(targetTransformPosition.x, targetTransformPosition.y, this.transform.position.z);
            this.transform.position = Vector3.MoveTowards(this.transform.position, cameraTargetPosition, Time.deltaTime * distanceFromDeadZoneEdge * speed);
        }
    }
}

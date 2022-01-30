using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ZoneTrigger : Trigger
{
    protected override string icon => "Zone";

    private CircleCollider2D _circleCollider;
    private CircleCollider2D circleCollider
    {
        get
        {
            if (_circleCollider == null)
                _circleCollider = GetComponent<CircleCollider2D>();
            return _circleCollider;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.enabled == false)
            return;

        int characterlayerMask = LayerMask.GetMask("Character");
        if (collision.gameObject.GetComponent<MainCharacter>() != null)
            Run();
    }

    protected override void DoExtraGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, circleCollider.radius);
    }
}

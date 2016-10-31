using UnityEngine;
using System.Collections;

public class BounceBoulder : EmpoweredBoulder {
    
    protected override void reactToHit(Duck duck) {
        base.reactToHit(duck);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(
            CatapultClicky.forceToReach(rb, rb.position, rb.position + bounceBackScale * WorldWidth.Instance.width ) +
            new Vector2(rb.velocity.x * -1f, Mathf.Max(.5f, -1f * rb.velocity.y)), 
            ForceMode2D.Impulse);
    }
}

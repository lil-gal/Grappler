using Unity.VisualScripting;
using UnityEngine;

public class HookScript : MonoBehaviour
{
    public float speed = 10f;
    public float maxDistance;
    [HideInInspector] public bool retract;
    Rigidbody2D rb;
    Transform gun;

    GameObject player;
    LineRenderer lineRenderer;
    public float lineWidth;
    public Color lineColor;
    
    public LayerMask LatchOnto;
    public LayerMask PassThrough;
    public LayerMask RetractFrom;

    bool latched;
    
    void Start()
    {
        gun = transform.parent;
        transform.SetParent(null);
        player = GameObject.FindWithTag("Player");

        rb = GetComponent<Rigidbody2D>();
        
        lineRenderer = GetComponent<LineRenderer>();
    }

    
    void Update()
    {
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;

        // Set the number of vertices
        lineRenderer.positionCount = 2;

        lineRenderer.SetPosition(0, gun.transform.position);
        lineRenderer.SetPosition(1, transform.position);
        
        
        
        if (latched) {
            //rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
            return;
        }
        
        if(!retract){
            if(Vector2.Distance(gun.position, transform.position) < maxDistance) {
                rb.linearVelocity = transform.right * speed;
            }
            else {
                Retract();
            }
        }
        else {
            Vector2 direction = (gun.position - transform.position).normalized;

            // rotate to face agianst gun
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.MoveRotation(angle + 180f);

            // move toward gun
            rb.linearVelocity = direction * speed;
        }
    }

    public void Retract() {
        retract = true;
        latched = false;

        rb.bodyType = RigidbodyType2D.Dynamic;
        DistanceJoint2D joint = player.GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        player.GetComponent<PlayerScript>().StopSwing();
    }

    
    void OnTriggerEnter2D(Collider2D collision) {
        // if retracting
        if (retract) { 
            if(collision.transform.GetComponent<HookGunScript>() != null) {
                gun.GetComponent<HookGunScript>().OnReturn();
                Destroy(gameObject);
            }
            return;
        }

        // if still firing
    
        if(((1 << collision.gameObject.layer) & LatchOnto) != 0) {
            latched = true;

            DistanceJoint2D joint = player.GetComponent<DistanceJoint2D>();

            player.GetComponent<PlayerScript>().StartSwing();

            joint.enabled = true;
            joint.connectedAnchor = transform.position;

            joint.distance = Vector2.Distance(player.transform.position, transform.position);
        }


        if(((1 << collision.gameObject.layer) & PassThrough) != 0) {
        }
        

        if(((1 << collision.gameObject.layer) & RetractFrom) != 0) {
            Retract();
        }




        // QUICK PROGRAMMING LESSON!
        // 1 << (NUMBER) shifts the bit of the number "1" (00000001)
        //               NUMBER times to the left. 
        //               so, if NUMBER == 7, then the whole byte is gonna go to the left 7 times
        //               and the result will be (1000000)

        // "&" is a bitwise AND operator
        // if i have 2 sets of bit numbers (1000000) and (11000000), 
        // it will return a 1 if both of the numbers have a 1 in them, or otherwise returns 0
        // like this:
        //
        // (10000000)
        // (11000000)
        //
        //  ||||||||
        //  VVVVVVVV
        //
        // (10000000)

        // "!= 0", basically, if it were 0, nothing mothing
    
    }


    
}

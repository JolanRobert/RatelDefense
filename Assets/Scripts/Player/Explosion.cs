using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Vector3[] points;
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float force;

    public static Explosion instance;

    [SerializeField] private int nbPoint;
    

    private RaycastHit hit;
    private Vector3 origin;
    private Vector3 direction;
    [SerializeField] private float maxDistance;

    [SerializeField] private float a;
    [SerializeField] private float alpha;
    [SerializeField] private float beta;

    [SerializeField] private float x;

    [SerializeField] private GameObject dynamite;
    public bool isMove;
    [SerializeField] private int index;
    
    [SerializeField] private float explosionCooldown;

    [SerializeField] private GameObject cam;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (isMove && dynamite.activeSelf == true)
        {
            Throw();
        }

        if (explosionCooldown!=0)
        {
            explosionCooldown -= Time.deltaTime;
        }
        if (explosionCooldown<0)
        {
            explosionCooldown = 0;
        }
    }

    public void Line()
    {
        if (explosionCooldown == 0)
        {
            origin = transform.position;
            direction = cam.transform.forward;
            points = new Vector3[nbPoint + nbPoint/5];
            if (Physics.Raycast(origin, direction, out hit, maxDistance))
            {
                lineRenderer.enabled = true;
                Debug.DrawRay(origin,direction*hit.distance,Color.red);
                alpha = Vector3.Distance(transform.position,hit.point)/2;
                a = (1 - beta) / ((x - alpha) * (x - alpha)) ;
                points[0] = new Vector3(transform.position.x,a*(x-alpha)*(x-alpha) + beta + transform.position.y,transform.position.z) - transform.right;
                if (x==0)
                {
                    lineRenderer.positionCount = nbPoint + nbPoint/5;
                    for (int i = 1; i < nbPoint + nbPoint/5; i++)
                    {
                        x += Vector3.Distance(transform.position, hit.point) / nbPoint;
                        points[i] = new Vector3(transform.position.x,a*(x-alpha)*(x-alpha) + beta + transform.position.y,transform.position.z)+transform.forward.normalized*x - transform.right;
                    }
                
                    lineRenderer.SetPositions(points);
                    x = 0;
                }
            
            }
            else
            {
                lineRenderer.enabled = false;
                Debug.DrawRay(origin,direction*maxDistance,Color.blue);
            }
        }
    }

    
    
    void Throw()
    {
        explosionCooldown = 10;
        lineRenderer.enabled = false;
        dynamite.transform.position = points[index];
        if (index < points.Length-1)
        {
            index++;
        }
        else
        {
            DynamiteCollision.instance.CircleRaycast();
            ResetThrow();
        }
        
    }

    public void Pop()
    {
        if (explosionCooldown==0)
        {
            dynamite.SetActive(true);
        }
    }

    public void ResetThrow()
    {
        dynamite.SetActive(false);
        index = 0;
        isMove = false;
    }
}

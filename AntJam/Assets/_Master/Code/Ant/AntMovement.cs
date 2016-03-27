using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AntMovement : MonoBehaviour 
{
	[SerializeField]
	protected float m_speed = 1;
	[SerializeField][Tooltip("The game object containing the graphics for this ant")]
	protected GameObject m_graphics;
    public float Speed { get { return m_speed; } }

    public bool isHalted { get; set; }

    private Collider m_collider;
    private float m_yVelocityDown = 0;
    private bool m_falling = false;
	private float m_startX;

    void Awake()
    {
        m_collider = GetComponent<Collider>();
    }

	void Start()
	{
		m_startX = transform.position.x;
	}
    
	// Update is called once per frame
	protected virtual void FixedUpdate () 
	{
        if (!isHalted)
        {
            //Calculate velocity
            Vector3 velocity = Vector3.left * m_speed * Time.deltaTime;

            transform.rotation = Quaternion.Euler(0, 0, 0);

            Vector3 offset = new Vector3(m_collider.bounds.size.x / 2, -m_collider.bounds.size.y / 2);
            //Check if another ant is in front of this one
            if (RaycastAnt(transform.position + offset, velocity, m_collider.bounds.size.x + 0.1f))
            {
                //Stack on top of another ant
                velocity += Vector3.up * Mathf.Abs(velocity.x) * 10;

				if (m_graphics != null)
					m_graphics.transform.rotation = Quaternion.Euler (velocity);
            }

			if (GetComponent<BlissComponent>().isConverted) //Performance waste, too lazy to fix
				velocity.x = -velocity.x;

            transform.Translate(velocity, Space.Self);
        }

        if (m_falling)
        {
            m_yVelocityDown += Physics.gravity.y * Time.deltaTime; //Gravity.y is decreasing down
            transform.Translate(Vector3.up * m_yVelocityDown);
        }
	}

    protected virtual void Update()
    {
		Vector3 offset = new Vector3(m_collider.bounds.size.x / 2, m_collider.bounds.size.y / 2);
        float distanceToGround = PlaceOnGround.GetDistanceFromGround(m_collider);
        //Check if there's an ant below this
        if (distanceToGround > 0.0001f)
        {
            if (!RaycastAnt(transform.position + offset, Vector3.down, 2))
            {
                //Not below the back of the ant, check in front of it
                offset.x = -m_collider.bounds.size.x / 2;
                if (!RaycastAnt(transform.position + offset, Vector3.down, 2))
                {
                    //No ants below this one
                    m_falling = true;
                    m_yVelocityDown = 0;
                }
            }
        }
        else
        {
            if (m_falling)
            {
                //Landed this frame
                m_falling = false;
                transform.position += Vector3.down * distanceToGround;
            }
        }

		if (transform.position.x > m_startX && GetComponent<BlissComponent>().isConverted) 
		{
			Debug.Log ("Game won");
			enabled = false;
		}
    }

    private bool RaycastAnt(Vector3 origin, Vector3 direction, float rayLength)
    {
        //Check if the ant should stack on another
        Ray ray = new Ray(origin, direction);

        Debug.DrawRay(origin, direction * rayLength, Color.green);
        
        foreach (RaycastHit hit in Physics.RaycastAll(ray, rayLength))
        {
            if (hit.collider.GetComponent<AntMovement>() != null && hit.collider.gameObject != gameObject)
            {
                //Ray is hitting another ant
                return true;
            }
        }

        //Didn't hit another ant
        return false;
    }

    private bool RaycastAntDown(Vector3 offset)
    {
        return RaycastAnt(transform.position + offset, Vector3.down, 0.1f);
    }
}

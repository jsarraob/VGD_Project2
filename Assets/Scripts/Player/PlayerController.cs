using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("How fast the playr should move when running around.")]
    private float m_Speed;
    #endregion

    #region Private Variables
    private Vector3 p_Velocity;
    #endregion

    #region Initialization
    private void Awake()
    {
        cc_Rb = gameObject.GetComponent<Rigidbody>();
        p_Velocity = Vector3.zero;
    }
    #endregion

    #region Cached Components
    private Rigidbody cc_Rb;
    #endregion

    #region Main Updates
    private void Update()
    {
        //Input
        float forward = Input.GetAxis("Vertical");
        float right = Input.GetAxis("Horizontal");
        //Set velocity
        p_Velocity.Set(right, 0, forward);
    }

    private void FixedUpdate()
    {
        cc_Rb.MovePosition(cc_Rb.position + m_Speed * Time.fixedDeltaTime * p_Velocity);
    }
    #endregion
}

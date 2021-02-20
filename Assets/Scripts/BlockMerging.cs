using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlockMerging : MonoBehaviour
{
    #region Cached Components
    
    private Rigidbody cc_Rigidbody;
    
    private Renderer cc_Rend;
    #endregion
    
    private bool inPlayerContact;
    
    private List<GameObject> connectedBlocks;
    
    // Start is called before the first frame update
    void Start()
    {
        inPlayerContact = false;
        cc_Rigidbody = GetComponent<Rigidbody>();
        cc_Rend = GetComponent<Renderer>();
        connectedBlocks = new List<GameObject>();
        connectedBlocks.Add(gameObject);
    }

    void OnCollisionEnter(Collision coll) {
        GameObject other = coll.gameObject;
        if (other.CompareTag("Player")) {
            inPlayerContact = true;
            foreach (GameObject block in connectedBlocks) {
                block.GetComponent<BlockMerging>().inPlayerContact = true;
            }
        }
        else if (other.CompareTag("ColorBlock") && inPlayerContact && !connectedBlocks.Contains(other)) {
            //Vector3 contactPoint = coll.GetContact(0).point;
            Vector3 normal = coll.GetContact(0).normal;
            Vector3 otherPosition = other.transform.position;
            Vector3 originalPosition = transform.position;
            Vector3 newPosition = transform.position;
            if (normal.z > 0) {
                newPosition = otherPosition + Vector3.forward;
            } else if (normal.z < 0) {
                newPosition = otherPosition + Vector3.back;
            } else if (normal.x > 0){
                newPosition = otherPosition + Vector3.right;
            } else if (normal.x < 0){
                newPosition = otherPosition + Vector3.left;
            }
            Vector3 translation = newPosition - originalPosition;
            foreach (GameObject block in connectedBlocks) {
                block.transform.position += translation;
            }
             FixedJoint joint = gameObject.AddComponent<FixedJoint>();
             joint.anchor = coll.GetContact(0).point;
             joint.connectedBody = other.GetComponent<Rigidbody>();
             joint.enableCollision = false;
             List<GameObject> otherCB = other.GetComponent<BlockMerging>().connectedBlocks;
             connectedBlocks.AddRange(otherCB);
             foreach (GameObject block in connectedBlocks) {
                block.GetComponent<BlockMerging>().inPlayerContact = true;
                block.GetComponent<BlockMerging>().connectedBlocks = connectedBlocks;
             }
             changeColor(other.GetComponent<Renderer>().material.color);
        }
    }
    
    void changeColor(Color c) {
        Color o = cc_Rend.material.color;
        Color mix = new Color(0,0,0,1);
        mix.r = (float)(255 - Math.Pow((Math.Pow(255-o.r,2) + Math.Pow(255-c.r,2))/2, (float)0.5));
        mix.g = (float)(255 - Math.Pow((Math.Pow(255-o.g,2) + Math.Pow(255-c.g,2))/2, (float)0.5));
        mix.b = (float)(255 - Math.Pow((Math.Pow(255-o.b,2) + Math.Pow(255-c.b,2))/2, (float)0.5));
        foreach (GameObject block in connectedBlocks) {
            block.GetComponent<Renderer>().material.color = mix;
        }
    }
    
    void OnCollisionExit(Collision coll) {
        GameObject other = coll.gameObject;
        if (other.CompareTag("Player")) {
            foreach (GameObject block in connectedBlocks) {
                block.GetComponent<BlockMerging>().inPlayerContact = false;
            }
        }
    }
    
}

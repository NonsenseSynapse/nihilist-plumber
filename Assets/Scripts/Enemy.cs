using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour {

	public float moveSpeed = 50f;

	private Rigidbody2D rb2D;
	private bool isDead = false;

	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D>();
		rb2D.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
		rb2D.velocity = new Vector2(-moveSpeed, 0f);
	}
		
}

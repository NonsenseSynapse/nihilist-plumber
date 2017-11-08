using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public int columns = 8;
	public Transform floorTile;

	private Transform player;
	private float minX;
	private float maxX;
	private float minY;
	private float maxY;

	// Use this for initialization
	void Start() {
		if (instance == null) {
			instance = this;
		} 
		else if (instance != this) {
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
		InitGame();
	}

	void InitGame() {
		InitFloor();
		InitPlayer();
	}
		
	void InitFloor() {
		for (int x = (int) Mathf.Floor(minX); x < (int)Mathf.Ceil(maxX) + 1; x++) {
			Instantiate(floorTile, new Vector3(x, minY + 0.5f, 0), Quaternion.identity);
		}
	}

	void InitPlayer() {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		float totalWidth = maxX - minX;
		int leftOffset = Mathf.Max(2, (int)Mathf.Floor(totalWidth / 4f));
		int startingX = (int) Mathf.Floor(minX) + leftOffset;
		int startingY = (int) minY + 1;
		Instantiate(player, new Vector3(startingX, startingY, 0), Quaternion.identity);
	}

	void Awake () {
		float vertExtent = Camera.main.orthographicSize;    
		float horzExtent = vertExtent * Screen.width / Screen.height;

		// Calculations assume map is position at the origin
		minX = -horzExtent;
		maxX = horzExtent;
		minY = -vertExtent;
		maxY = vertExtent;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

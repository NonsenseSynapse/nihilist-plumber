using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public Transform floorTile;
	public Transform enemy;

	private GameObject player;
	private GameObject fullFloor;
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
		InitEnemies();
	}
		
	void InitFloor() {
		fullFloor = GameObject.FindGameObjectWithTag("FullFloor");
		int floorStart = (int) Mathf.Floor(minX);
		int floorEnd = (int) Mathf.Ceil(maxX) + 3;
		for (int x = floorStart; x < floorEnd; x++) {
			Transform newTile = Instantiate(floorTile, new Vector3(x, minY + 0.5f, 0), Quaternion.identity);
			newTile.parent = fullFloor.transform;
		}
	}

	void InitPlayer() {
		player = GameObject.FindGameObjectWithTag("Player");
		float totalWidth = maxX - minX;
		int leftOffset = Mathf.Max(2, (int)Mathf.Floor(totalWidth / 4f));
		int startingX = (int) Mathf.Floor(minX) + leftOffset;
		int startingY = (int) minY + 3;
		player.transform.position = new Vector3(startingX, startingY, 0);
	}

	void InitEnemies() {
		InvokeRepeating("SpawnEnemy", 2.0f, 5.0f);
	}

	void SpawnEnemy() {
		Transform newEnemy = Instantiate(enemy, new Vector3(maxX, minY + 2, 0), Quaternion.identity);
	}

	void RemoveOffscreenEnemies() {
		GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in allEnemies) {
			if (enemy.transform.position.x <= minX - 2 || enemy.transform.position.y <= minY - 2) {
				Destroy(enemy);
			}
		}
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
		RemoveOffscreenEnemies();
	}
}

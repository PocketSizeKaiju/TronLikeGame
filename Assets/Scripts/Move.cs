using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	//Botones con los que se mueve (Se costumizan en el Inspector)
	public KeyCode upKey;
	public KeyCode downKey;
	public KeyCode rightKey;
	public KeyCode leftKey;

	//Velocidad de mobimiento
	public float speed = 16;

	//Prefab de la pared
	public GameObject wallPrefab;

	//Pared actual
	Collider2D wall;

	//Donde termino la ultima pared
	Vector2 lastWallEnd;

	void Start () {
		//Velocidad inicial
		GetComponent<Rigidbody2D> ().velocity = Vector2.up * speed;
		spawnWall ();
	}

	void Update () {
		//Se fija si se apretaron botones
		if (Input.GetKeyDown (upKey)) {
			GetComponent<Rigidbody2D> ().velocity = Vector2.up * speed;
			spawnWall ();
		} else if (Input.GetKeyDown (downKey)) {
			GetComponent<Rigidbody2D> ().velocity = -Vector2.up * speed;
			spawnWall ();
		} else if (Input.GetKeyDown (rightKey)) {
			GetComponent<Rigidbody2D> ().velocity = Vector2.right * speed;
			spawnWall ();
		} else if (Input.GetKeyDown (leftKey)) {
			GetComponent<Rigidbody2D> ().velocity = -Vector2.right * speed;
			spawnWall ();
		}

		fitColliderBetween (wall, lastWallEnd, transform.position);
	}

	void spawnWall() {
		//guarda la posicion de la ultima pared
		lastWallEnd = transform.position;

		//Crea una nueva pared
		GameObject g = (GameObject) Instantiate(wallPrefab, transform.position, Quaternion.identity);  //Transform.position es la posicion actual del jugador y quaternion.identity es la rotacion por defecto
		wall = g.GetComponent<Collider2D> (); //Se guarda el component en la variable wall para estar al tanto de la pared actual
	}

	void fitColliderBetween(Collider2D co, Vector2 a, Vector2 b){
		//Calcula la posicion central
		co.transform.position = a + (b - a) * 0.5f;

		//Alarga (Horizontal o verticalmente)
		float dist = Vector2.Distance(a, b);
		if(a.x != b.x)
			co.transform.localScale = new Vector2(dist + 1, 1);
		else
			co.transform.localScale = new Vector2(1, dist + 1);
	}

	void OnTriggerEnter2D(Collider2D co) {
		if (co != wall) { //la pared actual no cuenta
			print("Perdio el jugador: " + name);
			Destroy (gameObject);
		}
	}		
}

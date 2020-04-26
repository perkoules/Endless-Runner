using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public Text text, buttonText;
	public Button btn;

	private Color cl;
	private CharacterController controller;
	private float speed = 5.0f, multiplier = 1;
	private Touch androidTouch;

	// Use this for initialization
	void Start () {
		cl = buttonText.GetComponent<Text>().color;
		controller = GetComponent<CharacterController>();
		text = GameObject.FindGameObjectWithTag("Points").GetComponent<Text>();
		text.text = string.Empty;
		PlayerPrefs.SetFloat("PlayersPoints", 0);
		text.text = PlayerPrefs.GetFloat("PlayersPoints").ToString();
	}
	
	// Update is called once per frame
	void Update () {
		controller.Move((Vector3.forward * speed)* Time.deltaTime);
		text.text = PlayerPrefs.GetFloat("PlayersPoints").ToString();
		if (Time.timeSinceLevelLoad > 30 * multiplier){
			speed ++;
			multiplier++;
			PlayerPrefs.SetFloat("PlayersPoints", PlayerPrefs.GetFloat("PlayersPoints") + multiplier*speed*30);
		}
		if (Input.GetKey(KeyCode.RightArrow)){
			controller.transform.position += Vector3.right * 0.1f;
		}
		if (Input.GetKey(KeyCode.LeftArrow)){
			controller.transform.position += Vector3.left * 0.1f;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Coin(Clone)"){
			Destroy(other.gameObject);
			float points = PlayerPrefs.GetFloat("PlayersPoints") + 100f;
			PlayerPrefs.SetFloat("PlayersPoints", points);
		}else{
			Destroy(this.gameObject);
		}
	}
}

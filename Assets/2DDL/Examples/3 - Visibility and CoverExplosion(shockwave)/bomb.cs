using UnityEngine;
using System.Collections;

public class bomb : MonoBehaviour {

	DynamicLight2D.DynamicLight thisLight;
	TimerClass timer;
	GameObject player;

	float tmpValue = 0;

	Vector3 pos;


	IEnumerator Start () {
		thisLight = GameObject.Find("2DLight").GetComponent<DynamicLight2D.DynamicLight>();
		player = GameObject.Find("MartinHead");

		timer = gameObject.AddComponent<TimerClass>();


		// Subscribe timer events //
		timer.OnUpdateTimerEvent += timerUpdate;
		timer.OnTargetTimerEvent += tick;

		timer.InitTimer(1.2f, true);

		StartCoroutine(LoopUpdate());
	
		yield return  null;

	}


		IEnumerator LoopUpdate(){

		while(true){
			pos.x += Input.GetAxis ("Horizontal") * 20f * Time.deltaTime;
			pos.y += Input.GetAxis ("Vertical") * 20f * Time.deltaTime;
			

			Vector3 martinPos = Input.mousePosition;
			martinPos = Camera.main.ScreenToWorldPoint(martinPos);
			martinPos.z = 0;


			yield return new WaitForEndOfFrame();
			thisLight.gameObject.transform.position = pos;
			player.transform.position = martinPos;
		}

		



	}

	
	void tick(){
		thisLight.LightRadius = 1.7f;
		tmpValue *=0;
	}
	void timerUpdate(float value){
	thisLight.LightRadius += value*.8f;
	}
}

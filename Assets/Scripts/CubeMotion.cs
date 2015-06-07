using UnityEngine;
using System.Collections;

public class CubeMotion : MonoBehaviour {



	GoTweenChain TriangleMotion,VerticalMotion;

	GoTween firstvertical,secondvertical,initialTriangle,secondTriangle,thirdTriangle;

	float trianglemotion,verticalmotion,timetocheck;

	bool isTween,isCoroutine;

	GoTweenCollectionConfig settoinfitine;

	Transform mypos;


	// Use this for initialization

	void Start () {

		isTween = false;

		isCoroutine=false;

	}




	IEnumerator triangle1(){

		float timetocheck = 0;

		while (timetocheck<2) {

			timetocheck=Time.time-trianglemotion;

			transform.position=Vector3.Lerp(Vector3.zero,new Vector3(5,0,0),timetocheck/2);

			if(transform.position.y==0 && Input.GetKeyDown(KeyCode.Alpha3))

			{
				yield return StartCoroutine (vertical());

				trianglemotion+=2;
			}

			yield return null;
		}

	}

	IEnumerator triangle2(){

	
		float timetocheck = 0;
		while (timetocheck<2) {

			timetocheck=Time.time-trianglemotion;

			transform.position=Vector3.Lerp(new Vector3(5,0,0),new Vector3(5,0,5),timetocheck/2);

			if(transform.position.y==0 && Input.GetKeyDown(KeyCode.Alpha3))

			{
				yield return StartCoroutine (vertical());

				trianglemotion+=2;
			}

			yield return null;
		}
	}
	IEnumerator triangle3(){

		float timetocheck = 0;

		while (timetocheck<2) {

			timetocheck=Time.time-trianglemotion;

			transform.position=Vector3.Lerp(new Vector3(5,0,5),Vector3.zero,timetocheck/2);

			if(transform.position.y==0 && Input.GetKeyDown(KeyCode.Alpha3))

			{
				yield return StartCoroutine (vertical());

				trianglemotion+=2;
			}

			yield return null;
		}
	}

	IEnumerator verticalup(){

		float timetocheck = 0;

		while (timetocheck<1) {

			timetocheck = Time.time - verticalmotion;


			transform.position=Vector3.Lerp(new Vector3(mypos.position.x,0,mypos.position.z), new Vector3 (mypos.position.x,5,mypos.position.z),timetocheck);

			yield return null;

		}
	}
	IEnumerator verticaldown(){
	
		float timetocheck = 0;

		while (timetocheck<1) {
			timetocheck = Time.time - verticalmotion;
			transform.position=Vector3.Lerp(new Vector3(transform.position.x,5,transform.position.z), new Vector3 (mypos.position.x,0,mypos.position.z),timetocheck);
			yield return null;
		}
	}
	
	IEnumerator vertical(){


		verticalmotion = Time.time;

		yield return StartCoroutine (verticalup());

		verticalmotion=Time.time;

		yield return StartCoroutine (verticaldown());

	}
	
	IEnumerator triangle (){

		trianglemotion = Time.time;

		yield return StartCoroutine (triangle1());
		
		trianglemotion = Time.time;

		yield return StartCoroutine (triangle2());

		trianglemotion= Time.time;

		yield return StartCoroutine (triangle3());

		yield return StartCoroutine(triangle());
	}



	void TweenMotion(){

		GoTweenConfig t1 = new GoTweenConfig ().position (new Vector3 (5, 0, 0));

		GoTweenConfig t2 = new GoTweenConfig ().position (new Vector3 (5, 0, 5));

		GoTweenConfig t3=new GoTweenConfig ().position (new Vector3 (0, 0, 0));

		initialTriangle = new GoTween (transform, 2f,t1 );

		secondTriangle = new GoTween (transform, 2f,t2);

		thirdTriangle = new GoTween (transform, 2f,t3);

		settoinfitine= new GoTweenCollectionConfig().setIterations(-1, GoLoopType.RestartFromBeginning);

		TriangleMotion = new GoTweenChain(settoinfitine).append(initialTriangle).append(secondTriangle).append(thirdTriangle);

		TriangleMotion.play ();
		
		
	}
	void CoroutineMotion(){

		StartCoroutine (triangle ());

	}
	// Update is called once per frame
	void Update () {

		mypos = transform;

		if (Input.GetKeyDown (KeyCode.Alpha1) && isCoroutine == false && isTween !=true) {
			isTween=true;
			TweenMotion();
		}
		if (isTween == true && transform.position.y==0 && Input.GetKeyDown (KeyCode.Alpha3)) {
			TriangleMotion.pause();

			firstvertical = new GoTween (transform, 1f, new GoTweenConfig ().position (new Vector3 (mypos.position.x, 5, mypos.position.z)));

			secondvertical= new GoTween (transform, 1f, new GoTweenConfig ().position (new Vector3 (mypos.position.x, 0, mypos.position.z)));

			VerticalMotion = new GoTweenChain().append(firstvertical).append(secondvertical);

			VerticalMotion.play();

			VerticalMotion.setOnCompleteHandler(c => TriangleMotion.play());

		}
		if (Input.GetKeyDown (KeyCode.Alpha2) && isTween == false && isCoroutine!=true) {

			isCoroutine = true;

			CoroutineMotion();
		}
	}
}
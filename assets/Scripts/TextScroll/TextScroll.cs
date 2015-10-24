using UnityEngine;
using System.Collections;

public class TextScroll : MonoBehaviour {

	public string textName = "textscroll";
	string text;

	public int padding = 0;

	float y;
	float textHeight;
	public float scrollSpeed = 5;

	public GUIStyle style;

	// Use this for initialization
	void Start () {
		text = Resources.Load<TextAsset>("Text/" + textName).text;

		y = Screen.height;
		textHeight = style.CalcHeight(new GUIContent(text), Screen.width-padding*2);
	}

	void Update() {
		y -= scrollSpeed * Time.deltaTime;
		if (Input.GetMouseButton(0)) y -= 9 * scrollSpeed * Time.deltaTime;

		if (y <= -textHeight) Application.LoadLevel("Test");
	}
	
	// Update is called once per frame
	void OnGUI() {
		GUI.Label(new Rect(padding, (int)y, Screen.width-padding*2, textHeight), text, style);
	}
}

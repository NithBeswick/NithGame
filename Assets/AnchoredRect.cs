using UnityEngine;
using System.Collections;

[System.Serializable]
public class AnchoredRect {
	public enum horizontalAnchors {left, middle, right};
	public enum verticalAnchors {top, middle, bottom};
	public horizontalAnchors horizontalAnchor = horizontalAnchors.left;
	public verticalAnchors verticalAnchor = verticalAnchors.top;
	
	public float x, y = 0;
	public float width, height = 0;
	
	public Vector2 anchorValue {
		get {
			Vector2 v = new Vector2();
			switch(horizontalAnchor){
			case horizontalAnchors.left:
				v.x = 0;
				break;
			case horizontalAnchors.middle:
				v.x = (Camera.main.pixelWidth/2) - (width/2);
				break;
			case horizontalAnchors.right:
				v.x = Camera.main.pixelWidth - width;
				break;
			}
			switch(verticalAnchor){
			case verticalAnchors.top:
				v.y = 0;
				break;
			case verticalAnchors.middle:
				v.y = (Camera.main.pixelHeight/2) - (height/2);
				break;
			case verticalAnchors.bottom:
				v.y = Camera.main.pixelHeight - height;
				break;
			}
			return v;
		}
	}
	
	public Rect toRect() {
		return new Rect(x + anchorValue.x, y + anchorValue.y, width, height);
	}
	
	public AnchoredRect() {
		
	}
	
	public AnchoredRect(float x, float y, float width, float height) {
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
	}
	
	public AnchoredRect(float x, float y, float width, float height, horizontalAnchors hAnchor, verticalAnchors vAnchor) {
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
		this.horizontalAnchor = hAnchor;
		this.verticalAnchor = vAnchor;
	}
}
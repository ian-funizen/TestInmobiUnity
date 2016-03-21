using UnityEngine;
using System.Collections.Generic;
using InMobiMiniJSON;

public class InMobiUI : MonoBehaviour
{
	#if UNITY_ANDROID
	public static string nativeContent=null;
	public static string nativeError;
	private long placementId = 1458202348495;

	void OnGUI()
	{
		
		float yPos = 5.0f;
		float xPos = 5.0f;
		float width = ( Screen.width >= 960 || Screen.height >= 960 ) ? 600 : 500;
		float height = ( Screen.width >= 960 || Screen.height >= 960 ) ? 60 : 55;
		float heightPlus = height + 10.0f;

		yPos += heightPlus;
		
		if( GUI.Button( new Rect( xPos, yPos, width, height ), "Initialize" ) )
		{
			var dict = new Dictionary<string,string> ();
			dict.Add ("age", "10");
			dict.Add ("gender", "GENDER_MALE");
			InMobiAndroid.init ("01c705db77264c6e84de7fd7400ee7e3", dict);			
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Set Log Level to Debug" ) )
		{
			InMobiAndroid.setLogLevel (InMobiLogLevel.Verbose);
		}

		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "load native" ) )
		{
			InMobiAndroid.loadNativeAd(placementId);
			
		}
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "attach to view" ) )
		{
			InMobiAndroid.bindNative();
			
		}

		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Detach from View" ) )
		{
			InMobiAndroid.unbindNative();
			
		}
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "report click" ) )
		{
			var dict = new Dictionary<string,string>();
			dict.Add( "testkey", "testvalue" );
			InMobiAndroid.reportAdClick(dict);
			
		}
		
		if(nativeContent != null && nativeError == null)
		{
			Debug.Log("Native Content in GUI: " + nativeContent);
			print(nativeContent);
			var content = Json.Deserialize(nativeContent) as Dictionary<string,object>;
			string adTitle = (string) content["title"];
			string landing = (string) content["landingURL"];
			string cta = (string) content["cta"];
			
			Debug.Log("AD title: " + adTitle);
			Debug.Log("landing url: " + landing);
			GUI.skin.label.fontSize = 25;
			GUI.contentColor = Color.red;
			GUI.Label(new Rect(xPos,yPos +=heightPlus,width, height),"Ad details");
			GUI.Label(new Rect(xPos,yPos +=heightPlus,width, height),"Title: "+ adTitle);
			GUI.Label(new Rect(xPos,yPos +=heightPlus,width, height),"Landing URL: "+ landing);
			GUI.Label(new Rect(xPos,yPos +=heightPlus,width, height),"CTA: "+ cta);
			
		}

		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Destroy Native Ad" ) )
		{
			InMobiAndroid.destroyNativeAd();
			
		}

		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Create Banner (320x50) centered" ) )
		{
			var dict = new Dictionary<string,string> ();
			dict.Add("hi","hello2");
			InMobiAndroid.createBanner (placementId,"ban1" ,InMobiAdPosition.Centered, 320, 50, 30,"test1,test2,test3",dict );

			
		}
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Load Banner (320x50) centered" ) )
		{
			InMobiAndroid.loadBanner("ban1");
			
			
		}
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Destroy Banner centered " ) )
		{
			InMobiAndroid.destroyBanner ("ban1");
		}



		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Load Interstitial1" ) )
		{
			var dict = new Dictionary<string,string> ();
			dict.Add("hi","hello");
			InMobiAndroid.loadInterstitial(placementId,"interstitial1","test1",dict);
		}
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Is Interstitial1 Ready" ) )
		{
			if(InMobiAndroid.getInterstitialState("interstitial1") == true)
				Debug.Log("Interstitial1 is Ready");
			else
				Debug.Log("Interstitial1 is not Ready");
		}
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "show Interstitial1" ) )
		{
			InMobiAndroid.showInterstitial("interstitial1");
		}
		


	}	
		#endif	
	

}
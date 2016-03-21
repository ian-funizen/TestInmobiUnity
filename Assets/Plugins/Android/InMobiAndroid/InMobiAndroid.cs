using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InMobiMiniJSON;


#if UNITY_ANDROID

public enum InMobiLogLevel
{
	None      = 0,
	Debug     = 1,
	Verbose   = 2,
	
}

public enum InMobiAdPosition
{
	TopLeft,
	TopCenter,
	TopRight,
	Centered,
	BottomLeft,
	BottomCenter,
	BottomRight,
}



public class InMobiAndroid
{
	private static AndroidJavaObject _plugin;
	
	static InMobiAndroid()
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
		
		// find the plugin instance
		using( var pluginClass = new AndroidJavaClass( "com.unity.InMobiPlugin" ) )
			_plugin = pluginClass.CallStatic<AndroidJavaObject>( "instance" );
	}
	
	
	// Sets the log level for the InMobi SDK
	public static void setLogLevel( InMobiLogLevel logLevel )
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
		
		_plugin.Call( "setLogLevel", (int)logLevel );
	}
	
	
	public static void init(string accountId, Dictionary<string,string> optionalInMobiParams = null )
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
		optionalInMobiParams.Add( "tp-ver", Application.unityVersion );
		_plugin.Call( "initialize",accountId,optionalInMobiParams != null ? Json.Serialize(optionalInMobiParams) : string.Empty);
	}
	
	
	#region interstitials
	
	// Preloads an interstitial
	public static void loadInterstitial(long placementId, string keys ,string keywords = null,Dictionary<string,string> publisherExtra = null )
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
		
		_plugin.Call( "loadInterstitial", placementId,keys, keywords,publisherExtra != null ? Json.Serialize(publisherExtra) : string.Empty);
	}
	
	
	
	// Gets the current state of the interstitial either true or false
	public static bool getInterstitialState(string keys)
	{
		if( Application.platform != RuntimePlatform.Android )
			return false;
		return _plugin.Call<bool>( "getInterstitialState" ,keys);
	}
	
	
	// Presents the interstitial if it is loaded and ready
	public static void showInterstitial(string keys)
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
		
		_plugin.Call( "showInterstitial" ,keys);
	}

	// Destroys the intersitial
	public static void removeInterstitial(string keys) {
		if( Application.platform != RuntimePlatform.Android )
			return;
		
		_plugin.Call( "removeInterstitial",keys);
	}
	
	#endregion
	
	
	#region banners
	
	// Creates a banner
	public static void createBanner(long placementId, string keys,InMobiAdPosition alignment, int width,  int height, int refreshInterval, string keywords = null,Dictionary<string,string>  publisherExtra =null)
	{
		if( Application.platform != RuntimePlatform.Android )
			return;

		_plugin.Call( "createBanner", (int)alignment,   width,  height,placementId, refreshInterval,keys, keywords, publisherExtra != null ? Json.Serialize(publisherExtra) : string.Empty);
	}
	
	// Reloads the banner
	public static void loadBanner(string keys)
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
		
		_plugin.Call( "loadBanner",keys);
	}
	
	
	
	// Destroys the banner
	public static void destroyBanner(string keys)
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
		
		_plugin.Call( "destroyBanner",keys);
	}
	
	#endregion

	#region native
	
	// load the natives
	public static void loadNativeAd(long placementId) {
		if( Application.platform != RuntimePlatform.Android )
			return;
		
		_plugin.Call( "loadNativeAd",placementId);
	}
	
	public static void bindNative() {
		if( Application.platform != RuntimePlatform.Android )
			return;
		
		_plugin.Call( "bindNative");
	}
	
	public static void reportAdClick(Dictionary<string,string>  publisherExtra =null) {
		if( Application.platform != RuntimePlatform.Android )
			return;	
		
		_plugin.Call( "reportAdClick",publisherExtra != null ? Json.Serialize(publisherExtra) : string.Empty);
	}

	public static void unbindNative() {
		if( Application.platform != RuntimePlatform.Android )
			return;
		
		_plugin.Call( "unbindNative");
	}

	public static void destroyNativeAd() {
		if( Application.platform != RuntimePlatform.Android )
			return;
		
		_plugin.Call( "destroyNativeAd");
	}
	
	
	#endregion




	
}

#endif
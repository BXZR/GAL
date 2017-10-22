using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class saveLoadPicture : MonoBehaviour {

	private  string path = "";//保存图片的位置
	private  int width = 100;
	private  int height = 80;

	//缩放的过程
	private Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
	{
		Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, false);

		float incX = (1.0f / (float)targetWidth);
		float incY = (1.0f / (float)targetHeight);

		for (int i = 0; i < result.height; ++i)
		{
			for (int j = 0; j < result.width; ++j)
			{
				UnityEngine.Color newColor = source.GetPixelBilinear((float)j / (float)result.width, (float)i / (float)result.height);
				result.SetPixel(j, i, newColor);
			}
		}

		result.Apply();
		return result;
	}

	//保存截图的方法
	//摄像机方法

	//为此需要准备的方法：保存引用
	Camera  theCamera = null ;
	public void getCamera()
	{
		if (theCamera == null) //这是一种单例模式的简单形式
		{ 
			theCamera = GameObject.Find ("/shotCamera").GetComponent <Camera> ();
			theCamera.gameObject.SetActive (false);
		}
	}
	public  IEnumerator OnScreenCapture2 (int index   )
	{

		path  = Application .persistentDataPath+"/save"  + index + ".jpg";
		if (string.IsNullOrEmpty (path) || theCamera == null)
			StopCoroutine (OnScreenCapture(index));

		theCamera.gameObject.SetActive (true);
        //短暂的等待
		yield return new WaitForEndOfFrame();
		try
		{
			int widthUse = Screen.width;
			int heightUse = Screen.height;

			RenderTexture rt = new RenderTexture( widthUse, heightUse, 0);

			theCamera.targetTexture = rt;
			theCamera.Render();
			RenderTexture.active = rt;

			Texture2D tex =  new Texture2D(widthUse,heightUse, TextureFormat.RGB24, false);//新建一张图
			tex.ReadPixels (new Rect (0, 0, widthUse, heightUse), 0, 0, true);//从屏幕开始读点

			tex = ScaleTexture(tex , width ,  height);//缩放的过程

			byte[] imagebytes = tex.EncodeToJPG ();//用的是JPG(这种比较小)
			//使用它压缩实时产生的纹理，压缩过的纹理使用更少的显存并可以更快的被渲染
			//通过true为highQuality参数将抖动压缩期间源纹理，这有助于减少压缩伪像
			//因为压缩后的图像不作为纹理使用，只是一张用于展示的图
			//但稍微慢一些这个小功能暂时貌似还用不到
			tex.Compress (false);
			tex.Apply();
			Texture2D mScreenShotImgae = tex;

			try
			{
				File.WriteAllBytes (path, imagebytes);
			}
			catch
			{
				print("保存存档截图失败");
			}
			mScreenShotImgae = tex;


		}
		catch (System.Exception e)
		{
			print ("截图失败");
		}
		finally
		{
			if (theCamera != null)
				theCamera.gameObject.SetActive (false);
		}
	}



	//保存截图的方法
	public  IEnumerator OnScreenCapture (int index)
	{
		path  = Application .persistentDataPath+"/save"  + index + ".jpg";
		if (string.IsNullOrEmpty (path))
			StopCoroutine (OnScreenCapture(index));

		GameObject.Find ("/Canvas").SetActive (false);

		yield return new WaitForEndOfFrame();
		try
		{
			int widthUse = Screen.width;
			int heightUse = Screen.height;


			Texture2D tex = new Texture2D (widthUse, heightUse, TextureFormat.RGB24, false);//新建一张图
			tex.ReadPixels (new Rect (0, 0, widthUse, heightUse), 0, 0, true);//从屏幕开始读点

			tex = ScaleTexture(tex , width ,  height);//缩放的过程

			byte[] imagebytes = tex.EncodeToJPG ();//用的是JPG(这种比较小)
			//使用它压缩实时产生的纹理，压缩过的纹理使用更少的显存并可以更快的被渲染
			//通过true为highQuality参数将抖动压缩期间源纹理，这有助于减少压缩伪像
			//因为压缩后的图像不作为纹理使用，只是一张用于展示的图
			//但稍微慢一些这个小功能暂时貌似还用不到
			tex.Compress (false);
			tex.Apply();
			Texture2D mScreenShotImgae = tex;

			try
			{
			File.WriteAllBytes (path, imagebytes);
			}
			catch
			{
				print("保存存档截图失败");
			}
			mScreenShotImgae = tex;


		}
		catch (System.Exception e)
		{
			print ("截图失败");
		}
		finally 
		{
			GameObject.Find ("/Canvas").SetActive (true);
		}
	}
}

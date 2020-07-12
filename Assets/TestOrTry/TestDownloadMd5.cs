using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using System.IO;

public class TestDownloadMd5 : MonoBehaviour
{
    [SerializeField] private Button _download;
    [SerializeField] private RawImage _image;
    [SerializeField] private Text _md5;

    private UnityWebRequest requestTexture;
    private const string url = "https://cdn.dribbble.com/users/650155/screenshots/1839333/attachments/307170/%E5%8D%83%E5%B2%9B%E6%B9%96%E5%A4%9C%E6%99%9A%E5%A3%81%E7%BA%B8.png";
    private static string path = "/pic/download_request.png";
    // Start is called before the first frame update
    void Start()
    {
        _download.onClick.AddListener(DownloadByRequest);
    }

    // Update is called once per frame
    void Update()
    {
        if(requestTexture != null)
        {
            if(requestTexture.isDone)
            {
                var data = requestTexture.downloadHandler.data;
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(data);
                _image.texture = tex;
                MD5 md5 = MD5.Create();
                var md5Data = md5.ComputeHash(data);
                StringBuilder sb = new StringBuilder();
                foreach(var b in md5Data)
                {
                    sb.Append(b.ToString("x2"));
                }
                _md5.text = sb.ToString();
                _md5.SetNativeSize();
                File.WriteAllBytes(Application.dataPath + "\\TestOrTry\\texture\\1.png", data);
                requestTexture = null;
            }
            else
            {
                Debug.Log("in downlown");
            }
        }
    }

    void DownloadByRequest()
    {
        requestTexture = UnityWebRequestTexture.GetTexture(url);
        requestTexture.timeout = 150;
        requestTexture.SendWebRequest();
    }
}

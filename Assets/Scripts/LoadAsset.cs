using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UniRx;
using UniRx.Async;

public class LoadAsset : MonoBehaviour
{
    private List<string> assetIDList;
    // Start is called before the first frame update
    void Start()
    {
        var path = Path.Combine(Application.dataPath, "simple-assets");
        this.assetIDList = Directory.GetFiles(path, "*.png", SearchOption.AllDirectories).Select((e) => "Assets/" + e.Substring(Application.dataPath.Length + 1).Replace('\\', '/')).ToList();
        assetIDList.ForEach(Debug.Log);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPush()
    {
        Load().Forget();
    }

    private async UniTask Load()
    {
    }
}

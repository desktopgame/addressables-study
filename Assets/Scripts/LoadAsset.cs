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
    private bool loading;

    [SerializeField]
    private UnityEngine.UI.Image image;

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

    public void OnPush2()
    {
        Load2().Forget();
    }

    private async UniTask Load()
    {
        if(loading)
        {
            return;
        }
        this.loading = true;
        var file = assetIDList.OrderBy((e) => System.Guid.NewGuid()).First();
        image.sprite = await Addressables.LoadAssetAsync<Sprite>(file).Task;
        this.loading = false;
    }

    private async UniTaskVoid Load2()
    {
        if (loading)
        {
            return;
        }
        this.loading = true;
        var sprites = await Addressables.LoadAssetsAsync<Sprite>("MyLabel", null).Task;
        foreach(var sprite in sprites)
        {
            image.sprite = sprite;
            await UniTask.Delay(1000);
        }
        this.loading = false;
    }
}

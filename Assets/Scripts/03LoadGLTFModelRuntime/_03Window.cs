public class _03Window : BaseWindow
{
    public string _gltfUrl;
    void Start()
    {
        SetGLTFModelOnlineAsync(gameObject, _gltfUrl);
    }
}
using System.Collections;
using UnityEngine;
using YooAsset;

public class HotFixLinkedFSM_UpdatePackageVersion : FSMLinkedStaterItemBase
{
    private CoroutineHandler coroutine = null;

    public override void OnEnter()
    {
        HotFixEventMessage.PatchStatesChange.SendEventMessage("获取最新的资源版本 !");
        coroutine = UpdatePackageVersion().Start();
    }

    private IEnumerator UpdatePackageVersion()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        var packageName = (string)_fsmLinkedStater.GetBlackboardValue("PackageName");
        var package = YooAssets.GetPackage(packageName);
        var operation = package.UpdatePackageVersionAsync();
        yield return operation;

        if (operation.Status != EOperationStatus.Succeed)
        {
            Debug.LogWarning(operation.Error);
            HotFixEventMessage.PackageVersionUpdateFailed.SendEventMessage();
        }
        else
        {
            _fsmLinkedStater.SetBlackboardValue("PackageVersion", operation.PackageVersion);
            _fsmLinkedStater.InvokeTargetStaterItem<HotFixLinkedFSM_UpdatePackageManifest>();
        }
    }
}

using GalleryPixels.UI.Enums;

namespace GalleryPixels.UI.Services;

public class ToastService
{
    public event Func<ToastData, Task>? OnShow;

    private void ShowToast(ToastData toastData)
    {
        OnShow?.Invoke(toastData);
    }

    public void ShowSuccess(string message, int dismissAfter = 3) => ShowToast(new ToastData(ToastType.Success, message, dismissAfter));
    public void ShowError(string message, int dismissAfter = 3) => ShowToast(new ToastData(ToastType.Error, message, dismissAfter));
    public void ShowWarning(string message, int dismissAfter = 3) => ShowToast(new ToastData(ToastType.Warning, message, dismissAfter));
    public void ShowInfo(string message, int dismissAfter = 3) => ShowToast(new ToastData(ToastType.Info, message, dismissAfter));
}
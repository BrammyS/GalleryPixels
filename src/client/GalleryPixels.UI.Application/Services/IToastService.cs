using GalleryPixels.UI.Domain.Models;

namespace GalleryPixels.UI.Application.Services;

public interface IToastService
{
    event Func<ToastData, Task>? OnShow;
    
    void ShowSuccess(string message, int dismissAfter = 3);
    void ShowError(string message, int dismissAfter = 3);
    void ShowWarning(string message, int dismissAfter = 3);
    void ShowInfo(string message, int dismissAfter = 3);
}
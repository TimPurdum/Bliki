using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;


namespace Bliki.Data
{
    public static class Extensions
    {
        public static async Task Focus(this ElementReference elementRef, IJSRuntime jsRuntime,
            int? start = null, int? end = null)
        {
            await jsRuntime.InvokeVoidAsync("focusOnElement", elementRef, start, end);
        }
    }
}
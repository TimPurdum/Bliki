using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bliki.Data
{
    public static class Extensions
    {
        public static async Task Focus(this ElementReference elementRef, IJSRuntime jsRuntime, int? start = null, int? end = null)
        {
            await jsRuntime.InvokeVoidAsync("focusOnElement", new object?[] { elementRef, start, end });
        }
    }
}

﻿using Microsoft.AspNetCore.Http;


namespace TaskManagerCore.Extensions;

public static class HttpRequestExtensions
{
    public static bool IsAjax(this HttpRequest request)
    {
        return request.Headers["X-Requested-With"] == "XMLHttpRequest";
    }
}
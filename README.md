# Project

Demo project that creates a dynamic zip file on the server and downloads it in browser with the click of a button.

## The project runs in ASP.NET version 4.8

### Steps to run

The solution has two projects, run both simultaneously to get the result

WebApi Part

1. uses [ZipArchive](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.ziparchive?view=netframework-4.8) to zip a file (using it's byte array).
2. returns a [HttpResponseMessage](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage?view=netframework-4.8) with ByteArray content with type "application/zip".

Front End Part

1. uses the [Fetch API](https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API/Using_Fetch#supplying_request_options).
2. retrieves the [blob response](https://developer.mozilla.org/en-US/docs/Web/API/Response/blob) and creates a url using [createObjectURL](https://developer.mozilla.org/en-US/docs/Web/API/URL/createObjectURL_static).

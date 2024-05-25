using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using ZipDownloader.API.Models;
namespace ZipDownloader.API.Controllers
{
    [RoutePrefix("api/zipper")]
    public class ZipperController : ApiController
    {
        [HttpGet]
        public string Get()
        {
            return "Test Zipper";
        }

        [HttpPost]
        [Route("DownloadFile")]
        public HttpResponseMessage DownloadFile(FlexRequestModel model)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {

                byte[] fileBytes = GetExcelFileAsByteArray(); //here is your created file in bytes
                string fileNameZip = $"FlexQuery_{DateTime.Now.ToString("yyyyMMMddHHmmss")}.zip";
                using (var outStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(outStream, ZipArchiveMode.Create, true))
                    {

                        string timeStampedExcelFileName = "FlexQuery_" + model.QuerCode.ToUpperInvariant() + "_" + GetDateTimeInNumber() + ".xlsx";
                        var fileInArchive = archive.CreateEntry(timeStampedExcelFileName, CompressionLevel.Optimal);
                        using (var entryStream = fileInArchive.Open())
                        using (var fileToCompressStream = new MemoryStream(fileBytes))
                        {
                            fileToCompressStream.CopyTo(entryStream);
                        }
                    }
                    outStream.ToArray();
                    result.Content = new ByteArrayContent(outStream.ToArray());
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
                }
            }
            catch (Exception)
            {
                result = new HttpResponseMessage(HttpStatusCode.InternalServerError);

            }
            return result;
        }

        private string GetDateTimeInNumber()
        {
            return DateTime.Now.ToString("yyyyMMddhhmmss");
        }
        private byte[] GetExcelFileAsByteArray()
        {
            var filePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/sprint-plan.xlsx");
            return File.ReadAllBytes(filePath); //here is your created file in bytes
        }
    }
}

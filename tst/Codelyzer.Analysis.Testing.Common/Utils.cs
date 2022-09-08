using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codelyzer.Analysis.Testing.Common
{
    internal static class Utils
    {
        internal static void DeleteDirectory(string? directory)
        {
            if (directory != null)
            {
                if (Directory.Exists(directory))
                {
                    Directory.Delete(directory, true);
                }
            }
        }

        internal static async Task DownloadAndExtractAsync(string url, string directory)
        {
            using var client = new HttpClient();
            await using var streamRead = await client.GetStreamAsync(url);
            var fileName = Path.GetTempFileName();
            await using (var streamWrite = File.Create(fileName))
            {
                await streamRead.CopyToAsync(streamWrite);
            }
            ZipFile.ExtractToDirectory(fileName, directory, true);
            File.Delete(fileName);
        }
    }
}

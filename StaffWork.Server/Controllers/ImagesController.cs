using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace StaffWork.Server.Controllers
{
    public class ImagesController : Controller
    {
        public IActionResult Get(string filename)
        {
            var memory = GetImage(filename, "wwwroot/Images");
            return File(memory.ToArray(), $"image/{filename.Split(".")[1]}", filename);
        }
        private MemoryStream GetImage(string filename, string imagesPath)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory().Replace("/Controllers", ""), imagesPath, filename);
            var memory = new MemoryStream();
            if (System.IO.File.Exists(path))
            {
                var net = new System.Net.WebClient();
                var data = net.DownloadData(path);
                var content = new System.IO.MemoryStream(data);
                memory = content;
            }
            memory.Position = 0;
            return memory;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using image_ai_analyser.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using image_ai_analyser.Interfaces;

namespace image_ai_analyser.Controllers
{
    public class HomeController : Controller
    {
        private readonly IImageViewModelBuilder _imageViewModelBuilder;


        public HomeController(IImageViewModelBuilder imageViewModelBuilder)
        {
            _imageViewModelBuilder = imageViewModelBuilder;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ProcessImage(HttpPostedFileBase file)
        {
            DeleteImage();

            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Server.MapPath("~/images"), fileName);
                file.SaveAs(filePath);

                List<ImageViewModel> model = new List<ImageViewModel>();
                model.Add(await _imageViewModelBuilder.MapToImageModel(filePath));
                model.First().Image = file; 

                string localPath = "/images/";
                string localPathToImage = localPath + fileName;
                model.First().ImageString = localPathToImage;
                return View("ImageResult", model);
            }

            return View("Index");
        }

        public ActionResult ImageResult()
        {
            return View();
        }

        private void DeleteImage()
        {
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/images"));
            foreach (FileInfo img in dir.GetFiles())
            {
                img.Delete();
            }
        }
    }
}

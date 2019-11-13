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

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // Post send api
        [HttpPost]
        public async Task<ActionResult> ProcessImage(HttpPostedFileBase file)
        {

            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Server.MapPath("~/images"), fileName);
                file.SaveAs(filePath);

                string jsonString = await  _imageViewModelBuilder.MakeAnalysisRequest(filePath);

                List<ImageViewModel> model = new List<ImageViewModel>();
                model.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<ImageViewModel>(jsonString));
                model.First().Image = file; 

                string localPath = "/images/";
                string localPathToImage = localPath + fileName;
                model.First().ImageString = localPathToImage;

                return View("ImageResult", model);
            }

            return View();
        }



        public ActionResult ImageResult()
        {
            return View();
        }
    }
}

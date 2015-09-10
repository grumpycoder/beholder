using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace splc.beholder.web.Utility
{
    public static class ControllerExtensions
    {
        public static ImageResult Image(this Controller controller, Stream imageStream, string contentType)
        {
            return new ImageResult(imageStream, contentType);
        }

        public static ImageResult Image(this Controller controller, byte[] imageBytes, string contentType)
        {
            return new ImageResult(new MemoryStream(imageBytes), contentType);
        }
    }
}
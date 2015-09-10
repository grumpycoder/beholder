using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO.Compression;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Web.Mvc;
using System.Linq.Expressions;
using ICSharpCode.SharpZipLib.Core;

namespace splc.beholder.web.Utility
{
    public static class ImageHelper
    {
        public static MvcHtmlString UploadImageFor<TModel, TProperty>
       (this HtmlHelper<TModel> helper,
       Expression<Func<TModel, TProperty>> expression, string id = null,
       string name = null, string cssclass = null, string alt = null,
       string imgId = null, string height = null, string width = null)
        {
            const string type = "file";

            if (cssclass == null)
            {
                cssclass = "upload";
            }

            if (id == null)
            {
                id = "File1";
            }

            if (name == null)
            {
                name = "imageLoad2";
            }

            if (alt == null)
            {
                alt = "Preview";
            }

            if (imgId == null)
            {
                imgId = "imgThumbnail2";
            }

            if (height == null)
            {
                height = "126px";
            }

            if (width == null)
            {
                width = "126px";
            }

            var fileBuilder = new TagBuilder("input");
            var imgBuilder = new TagBuilder("img");
            var mergedBuilder = new TagBuilder("p");

            ///Firstly we build the input tag. //--Add the CSS class. fileBuilder.AddCssClass(cssclass);        
            //--Add the name. fileBuilder.MergeAttribute("name", name);        
            //--Add the id. fileBuilder.MergeAttribute("id",id);        
            //--Add the type. fileBuilder.MergeAttribute("type",type);

            ///Secondly we build the img tag. //--Add the alt. imgBuilder.MergeAttribute("alt", alt);        
            //--Add the id. imgBuilder.MergeAttribute("id", imgId);        
            //--Add the height and width. imgBuilder.MergeAttribute("height", height);        
            imgBuilder.MergeAttribute("width", width);

            ///Merge the two together into a p tag. 
            mergedBuilder.AddCssClass("file-upload-");
            mergedBuilder.InnerHtml += fileBuilder;
            mergedBuilder.InnerHtml += imgBuilder;
            return MvcHtmlString.Create(mergedBuilder.ToString());

        }

        public static byte[] Decompress(byte[] gzip)
        {
            // Create a GZIP stream with decompression mode.
            // ... Then create a buffer and write into while reading from the GZIP stream.
            using (MemoryStream mstream = new MemoryStream(gzip))
            {
                using (ZipInputStream zstream = new ZipInputStream(mstream))
                {
                    var myZipEntry = zstream.GetNextEntry();
                    var filename = myZipEntry.Name;

                    const int size = 4096;
                    byte[] buffer = new byte[size];
                    using (MemoryStream memory = new MemoryStream())
                    {
                        int count = 0;
                        do
                        {
                            count = zstream.Read(buffer, 0, size);
                            if (count > 0)
                            {
                                memory.Write(buffer, 0, count);
                            }
                        }
                        while (count > 0);
                        return memory.ToArray();
                    }
                }
            }
        }

        public static byte[] Compress(byte[] gzip, string filename)
        {
            // Create a GZIP stream with compression mode.
            // ... Then create a buffer and write into while reading from the GZIP stream.
            using (MemoryStream mstreamIn = new MemoryStream(gzip))
            {
                using (MemoryStream mstreamOut = new MemoryStream())
                {
                    using (ZipOutputStream zstream = new ZipOutputStream(mstreamOut))
                    {
                        zstream.SetLevel(3);

                        ZipEntry newEntry = new ZipEntry(filename);
                        newEntry.DateTime = DateTime.Now;

                        zstream.PutNextEntry(newEntry);

                        StreamUtils.Copy(mstreamIn, zstream, new byte[4096]);
                        zstream.CloseEntry();

                        zstream.IsStreamOwner = false;
                        zstream.Close();

                        mstreamOut.Position = 0;
                        return mstreamOut.ToArray();

                        //const int size = 4096;
                        //byte[] buffer = new byte[size];
                        //using (MemoryStream memory = new MemoryStream())
                        //{
                        //    int count = 0;
                        //    do
                        //    {
                        //        count = zstream.Read(buffer, 0, size);
                        //        if (count > 0)
                        //        {
                        //            memory.Write(buffer, 0, count);
                        //        }
                        //    }
                        //    while (count > 0);
                        //    return memory.ToArray();
                        //}
                    }
                }
            }
        }

    }
}
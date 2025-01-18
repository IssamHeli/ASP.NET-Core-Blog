using System.Diagnostics;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PagedList;
using Technexa.Data;
using Technexa.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Technexa.Controllers
{

    public class HomeAccesController : Controller
    {
        private readonly DBContextApplication _context;

        public HomeAccesController(DBContextApplication context)
        {
            _context = context;
        }

        [Route("sitemap.xml")]
        public IActionResult Sitemap()
        {
            var sitemapNodes = new List<SitemapNode>();

            var sitemapNodesvedio = new List<SitemapNodeVideo>();

            var baseUri = new Uri("https://priorityclips.azurewebsites.net"); // Replace with your actual base URL
            var currentDate = DateTime.UtcNow;

            sitemapNodes.Add(new SitemapNode
            {
                Location = new Uri(baseUri, Url.Action("Index", "Home")).ToString(),
                LastModified = currentDate,
                ChangeFrequency = SitemapChangeFrequency.Never,
                Priority = 1.0
            });
            sitemapNodes.Add(new SitemapNode
            {
                Location = new Uri(baseUri, Url.Action("Privacy", "HomeAcces")).ToString(),
                LastModified = currentDate,
                ChangeFrequency = SitemapChangeFrequency.Yearly,
                Priority = 0.8
            });

            sitemapNodes.Add(new SitemapNode
            {
                Location = new Uri(baseUri, Url.Action("ThankYou", "HomeAcces")).ToString(),
                LastModified = currentDate,
                ChangeFrequency = SitemapChangeFrequency.Never,
                Priority = 0.8
            });




            // Add category URLs
            var categories = _context.Categorie.ToList();

            List<int> nomprepageparcategorie = new List<int>();

            for (int i = 0; i < categories.Count; i++)
            {
                int nombrepostparcategorie = _context.Posts.Where(p => p.categorie == categories[i].Designation).Count();

                nomprepageparcategorie.Add((int)Math.Ceiling((double)nombrepostparcategorie / 6));
            }


            for (int i = 0; i < categories.Count; i++)
            {
                for (int x = 1; x <= nomprepageparcategorie[i]; x++)
                {
                    sitemapNodes.Add(new SitemapNode
                    {
                        Location = new Uri(baseUri, Url.Action("SearchByCategorie", "HomeAcces", new { categorie = categories[i].Designation, page = x, })).ToString(),
                        LastModified = currentDate,
                        ChangeFrequency = SitemapChangeFrequency.Monthly,
                        Priority = 0.5
                    }); ;
                }
            }

            /*
            // Add post URLs

            var postIds = _context.Posts.Select(P => P.idpost).ToList();
            int pageCount = (int)Math.Ceiling((double)postIds.Count / 6);
            for (int i = 1; i <= pageCount; i++)
            {
                sitemapNodes.Add(new SitemapNode
                {
                    Location = new Uri(baseUri, Url.Action("Index", "Home", new { page = i })).ToString(),
                    LastModified = currentDate,
                    ChangeFrequency = SitemapChangeFrequency.Weekly,
                    Priority = 1.0
                });
            }
            // Add post URLs vor vedios
            */
            var posts = _context.Posts.ToList();
            foreach (var post in posts)
            {

                var urlvedio = new Uri(baseUri, Url.Action("DetailsPublic", "HomeAcces", new { id = post.idpost })).ToString();
                sitemapNodesvedio.Add(new SitemapNodeVideo
                {
                    Location = urlvedio,
                    LastModified = currentDate,
                    ChangeFrequency = SitemapChangeFrequency.Weekly,
                    Priority = 0.5,
                    // Video-specific properties
                    VideoTitle = post.Title,
                    VideoDescription = post.Description,
                    VideoThumbnailUrl = "https://i.ytimg.com/vi/" + post.Srcimage + "/mqdefault.jpg",

                    // Google Search Console video properties
                    VideoContentLoc = post.SrcYoutubeVedio
                });
            }

            // Assuming sitemapXml is the generated XML content

            // Rest of your code...

            var xmlDocument = GenerateSitemapXml(sitemapNodes);

            var xmlDocumentvedios = GenerateSitemapXmlVideo(sitemapNodesvedio);

            return Content(CombineSitemaps(xmlDocument, xmlDocumentvedios).OuterXml, "application/xml");
        }

        private XmlDocument CombineSitemaps(XmlDocument xmlDocument1, XmlDocument xmlDocument2)
        {
            // Create a new XML document with a root element
            XmlDocument combinedDoc = new XmlDocument();
            XmlElement rootElement = combinedDoc.CreateElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");
            combinedDoc.AppendChild(rootElement);

            // Import and append nodes from the first document
            foreach (XmlNode node in xmlDocument1.DocumentElement.ChildNodes)
            {
                rootElement.AppendChild(combinedDoc.ImportNode(node, true));
            }

            // Import and append nodes from the second document
            foreach (XmlNode node in xmlDocument2.DocumentElement.ChildNodes)
            {
                rootElement.AppendChild(combinedDoc.ImportNode(node, true));
            }

            return combinedDoc;
        }




        private XmlDocument GenerateSitemapXml(List<SitemapNode> nodes)
        {
            var xmlDoc = new XmlDocument();

            var declaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);

            // Create the root element 'urlset' with the specified namespace
            var urlset = xmlDoc.CreateElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

            xmlDoc.AppendChild(declaration);


            xmlDoc.AppendChild(urlset);

            foreach (var node in nodes)
            {
                var url = xmlDoc.CreateElement("url", urlset.NamespaceURI);

                var loc = xmlDoc.CreateElement("loc", urlset.NamespaceURI);
                loc.InnerText = node.Location;
                url.AppendChild(loc);

                var lastmod = xmlDoc.CreateElement("lastmod", urlset.NamespaceURI);
                lastmod.InnerText = node.LastModified.ToString("yyyy-MM-ddTHH:mm:sszzz");
                url.AppendChild(lastmod);

                var changefreq = xmlDoc.CreateElement("changefreq", urlset.NamespaceURI);
                changefreq.InnerText = node.ChangeFrequency.ToString().ToLower();
                url.AppendChild(changefreq);

                var priority = xmlDoc.CreateElement("priority", urlset.NamespaceURI);
                priority.InnerText = node.Priority.ToString("F1");
                url.AppendChild(priority);

                urlset.AppendChild(url);
            }

            xmlDoc.AppendChild(urlset);
            return xmlDoc;
        }



        private XmlDocument GenerateSitemapXmlVideo(List<SitemapNodeVideo> nodes)
        {
            var xmlDoc = new XmlDocument();

            var declaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(declaration);

            // Create the root element 'urlset' with the specified namespace
            var urlset = xmlDoc.CreateElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

            // Add video namespace declaration
            var videoNamespace = xmlDoc.CreateAttribute("xmlns:video");
            videoNamespace.Value = "http://www.google.com/schemas/sitemap-video/1.1";

            urlset.Attributes.Append(videoNamespace);

            xmlDoc.AppendChild(urlset);

            foreach (var node in nodes)
            {
                var url = xmlDoc.CreateElement("url", urlset.NamespaceURI);

                var loc = xmlDoc.CreateElement("loc", urlset.NamespaceURI);
                loc.InnerText = node.Location;
                url.AppendChild(loc);

                var lastmod = xmlDoc.CreateElement("lastmod", urlset.NamespaceURI);
                lastmod.InnerText = node.LastModified.ToString("yyyy-MM-ddTHH:mm:sszzz");
                url.AppendChild(lastmod);

                var changefreq = xmlDoc.CreateElement("changefreq", urlset.NamespaceURI);
                changefreq.InnerText = node.ChangeFrequency.ToString().ToLower();
                url.AppendChild(changefreq);

                var priority = xmlDoc.CreateElement("priority", urlset.NamespaceURI);
                priority.InnerText = node.Priority.ToString("F1");
                url.AppendChild(priority);

                var video = xmlDoc.CreateElement("video", "http://www.google.com/schemas/sitemap-video/1.1");

                var videoTitle = xmlDoc.CreateElement("video:title", video.NamespaceURI);
                videoTitle.InnerText = node.VideoTitle;
                video.AppendChild(videoTitle);

                var videoDesc = xmlDoc.CreateElement("video:description", video.NamespaceURI);
                videoDesc.InnerText = node.VideoDescription;
                video.AppendChild(videoDesc);

                var videoThumbnail = xmlDoc.CreateElement("video:thumbnail_loc", video.NamespaceURI);
                videoThumbnail.InnerText = node.VideoThumbnailUrl;
                video.AppendChild(videoThumbnail);

                var videoContentLoc = xmlDoc.CreateElement("video:content_loc", video.NamespaceURI);
                videoContentLoc.InnerText = node.VideoContentLoc;
                video.AppendChild(videoContentLoc);


                url.AppendChild(video);
                urlset.AppendChild(url);
            }

            xmlDoc.AppendChild(urlset);
            return xmlDoc;
        }









        [HttpGet]
        public async Task<PartialViewResult> getposts(int? page)
        {
            try
            {
                var categorie = await _context.Categorie.OrderByDescending(c => c.id).ToListAsync();

                var posts = await _context.Posts.OrderByDescending(p => p.DateCreated).ToListAsync();

                if (categorie != null)
                {
                    ViewData["Categories"] = new SelectList(categorie, "Designation", "Designation");
                    HomeController.Categories = new SelectList(categorie, "Designation", "Designation");
                }
                if (posts != null)
                {

                    int pageSize = 6;
                    int pageNumber = (page ?? 1);
                    IPagedList<Models.Post> paginatedPosts = posts.ToPagedList(pageNumber, pageSize);

                    return PartialView("_PostList", paginatedPosts);

                }

                return PartialView("_PostListNonLoading");


            }
            catch (SqlException)
            {
                return PartialView("_PostListErrorSql");
            }

        }


        [HttpGet]
        public async Task<IActionResult> DetailsPublic(int? id)
        {


            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            ViewData["Categories"] = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");
            ViewBag.idselected = id.ToString();

            var post = _context.Posts.FirstOrDefault(p => p.idpost == id);

            if (post == null)
            {

                return NotFound();
            }

            var posts = await _context.Posts.Where(p => p.categorie == post.categorie).OrderByDescending(p => p.DateCreated).Take(7).ToListAsync();
            var ithaspost = posts.FirstOrDefault(post);


            //check post if it has post if not add one
            if (ithaspost == null)
            {
                posts.RemoveAt(posts.Count - 1);
                posts.Add(post);
            }


            return View(posts);
        }

        [HttpPost]
        public IActionResult SearchByCategorie(int? page)
        {

            var categorieserchby = Request.Form["categorie"].ToString();
            if (categorieserchby != null)
            {

                return RedirectToAction(actionName: "SearchByCategorie", new { categorie = categorieserchby, page = 1 });
            }


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> SearchByCategorie(string categorie, int page)
        {
            ViewData["Categories"] = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");
            ViewBag.selectedcategorie = categorie;
            if (categorie != null)
            {
                int pageSize = 6;
                int pageNumber = page;
                var Posts = await _context.Posts.Where(p => p.categorie == categorie).OrderByDescending(p => p.DateCreated).ToListAsync();

                IPagedList<Models.Post> paginatedPosts = Posts.ToPagedList(pageNumber, pageSize);

                return View(paginatedPosts);
            }
            return View();
        }



        public IActionResult Privacy()
        {

            ViewData["Categories"] = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }





        public IActionResult ThankYou(string name)
        {

            ViewData["Categories"] = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");

            ViewBag.name = name;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage()
        {

            try
            {
                var message = new Message
                {
                    name = Request.Form["name"].ToString(),
                    email = Request.Form["email"].ToString(),
                    message = Request.Form["message"].ToString(),
                };
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(actionName: "ThankYou", new { name = message.name });

            }
            catch
            {

                ViewData["Categories"] = new SelectList(_context.Categorie.OrderByDescending(c => c.id), "Designation", "Designation");
                return View();

            }

        }


    }


}


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using WebSvc1.Models;

namespace WebSvc1.Services
{
    public class ReviewService
    {
        private static readonly IList<Review> reviewList = LoadReviews();

        public IList<Review> ListReviews()
        {
            if (reviewList == null || reviewList.Count == 0)
            {
                throw new InvalidDataException("List of reviews was not loaded.");
            }

            return reviewList;
        }

        public async Task<IList<Review>> ListReviewsAsync()
        {
            if (reviewList == null || reviewList.Count == 0)
            {
                throw new InvalidDataException("List of reviews was not loaded.");
            }

            return await Task.Run(() => reviewList);
        }
        

        private static IList<Review> LoadReviews()
        {
            List<Review> loadedReviews = new List<Review>();
            try
            {
                const string resourceName = "WebSvc1.Resources.reviews.xml";
                var serialiser = new XmlSerializer(typeof(Review));

                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    using (var xmlRdr = XmlReader.Create(stream))
                    {
                        while (xmlRdr.Read())
                        {
                            switch (xmlRdr.NodeType)
                            {
                                case XmlNodeType.Element:
                                {
                                    if (xmlRdr.Name == "review")
                                    {
                                        var emp = serialiser.Deserialize(xmlRdr) as Review;
                                        loadedReviews.Add(emp);
                                    }
                                    break;
                                }
                                default:
                                    break;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FileLoadException("Could not read file.", ex);
            }

            return loadedReviews;
        }
    }
}
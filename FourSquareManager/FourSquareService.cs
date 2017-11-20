using FourSquare.SharpSquare.Core;
using FourSquare.SharpSquare.Entities;
using System.Collections.Generic;
using System.Configuration;


namespace FourSquareManager
{
    public class FourSquareService : IFourSquareService
    {
        private readonly SharpSquare _FourSquare;

        public static string _ClientId = ConfigurationManager.AppSettings["CLIENT_ID"].ToString();
        public static string _ClientSecret = ConfigurationManager.AppSettings["CLIENT_SECRET"].ToString();

        public FourSquareService()
        {
            _FourSquare = new SharpSquare(_ClientId, _ClientSecret);
        }


        public List<Venue> SearchVenues(string location)
        {
            var venues = _FourSquare.SearchVenues(new Dictionary<string, string>
                {
                    {"near",location },
                    {"query",location }
                });

            return venues;
        }
    }
}

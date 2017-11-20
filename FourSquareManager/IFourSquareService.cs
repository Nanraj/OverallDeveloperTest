using FourSquare.SharpSquare.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourSquareManager
{
    public interface IFourSquareService
    {
        List<Venue> SearchVenues(string location);
    }
}

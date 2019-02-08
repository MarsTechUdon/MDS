using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class AddbookingModel
    {
        public List<BookingModel> Booking { get; set; }
        public List<BookingDetailModel> BookingDetail { get; set; }
        public ResultBookingModel ResultBooking { get; set; }
    }
}
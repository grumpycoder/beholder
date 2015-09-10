

namespace splc.beholder.web.Models
{
    public class ChapterViewModel
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string ChapterName { get; set; }
        public int? ActiveYear { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string AddressStateCode { get; set; }
        public string MovementClassName { get; set; }

        public AddressViewModel Address { get; set; }

        //public ICollection<AddressViewModel> Addresses { get; set; }

        //public string AddressChapterRelAddressZip { get; set; }
        //public decimal? Latitude { get; set; }
        //public decimal? Longitude { get; set; }

        public string ActiveStatusName { get; set; }
        public string ApprovalStatusName { get; set; }

    }


    public class AddressViewModel
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public int? StateId { get; set; }
        public string Zip5 { get; set; }
        public string Zip4 { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
    }
}
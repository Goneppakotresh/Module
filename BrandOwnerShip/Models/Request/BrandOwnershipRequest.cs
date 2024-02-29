namespace IEMS_WEB.Areas.BrandOwnerShip.Models.Request
{
    public class BrandOwnershipRequest
    {
        public int LicenseId { get; set; }
        public int BrandId { get; set; }
        public int FinYearId { get; set; }
        public int ResponseCode { get; set; }
        public string Message { get; set; } = "";
    }
}

using Lateetud.Utilities;

namespace Lateetud.PrimeritusService.Manager.Models
{
    public class VMExcel
    {
        public VMExcel()
        {
            Status = PStatus.None;
        }

        public string OriginalExcelFileName { get; set; }
        public string UploadedExcelFileName { get; set; }
        public string UploadedExcelFilePath { get; set; }
        public string ConvertedXmlFileName { get; set; }
        public string ConvertedXmlFilePath { get; set; }
        public string ConvertedXmlDownloadLink { get; set; }
        public string ConvertedXmlContent { get; set; }
        public string Message { get; set; }
        public PStatus Status { get; set; }
    }
}
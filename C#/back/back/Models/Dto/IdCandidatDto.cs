using static back.Models.Enum.Enumeration;

namespace back.Models.Dto
{
    public class IdCandidatDto
    {
        public string IdCandidat { get; set; }
    }

    public class UploadCandidatFileRequestDto
    {
        public IFormFile ActeFile { get; set; }
        public IFormFile PhotoFile { get; set; }
        public IFormFile BordereauFile { get; set; }
    }
}

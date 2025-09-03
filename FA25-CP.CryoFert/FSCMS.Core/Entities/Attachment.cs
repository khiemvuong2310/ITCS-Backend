using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    public class Attachment : BaseEntity
    {
        public int RelatedID { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public string UploadedBy { get; set; }
    }
}

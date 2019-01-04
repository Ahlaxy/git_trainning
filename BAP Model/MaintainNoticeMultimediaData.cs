using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAP_Model
{
    public class MaintainNoticeMultimediaData
    {
        public string OrderId { get; set; }
        public string ImageLocation { get; set; }
        public string VoiceLocation { get; set; }
        public string VideoLocation { get; set; }
        public string VideoScreenshotLocation { get; set; }
        public string BeforeMaintenanceImgUrl { get; set; }
        public string AfterMaintenanceImgUrl { get; set; }
        public string SignatureImgUrl { get; set; } //签名图片
    }
}

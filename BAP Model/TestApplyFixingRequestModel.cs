
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json; 


namespace BAP_Model
{
    public class TestApplyFixingRequestModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string EmpRepairsCode { get; set; }
        public string DeptCode { get; set; }
        public string EmpPhone { get; set; }
        public string DeviceCode { get; set; }
        public string MaintainLocation { get; set; }
        public string NoticeDescription { get; set; }
        public string UploadedImgUrl { get; set; }
        public string UploadedVideoUrl { get; set; }

        public string UploadedVoiceMediaId { get; set; }
        public string UploadedVoiceAccessToken { get; set; }
        //public string UploadedVoiceUrl { get; set; }
    }
}

 

﻿namespace WebTMDT_Client.ResponseModel
{
    public class PostReviewResponse
    {
        public bool success { get; set; }
        public bool newReview { get; set; }
        public bool update { get; set; }
        public string error { get; set; }
    }
}

namespace WebTMDT_Client.ViewModel
{
    public class RatingViewModel
    {
        public int FiveStar { get; set; }
        public int FourStar { get; set; }
        public int ThreeStar { get; set; }
        public int TwoStar { get; set; }
        public int OneStar { get; set; }

        public int TotalReview { get; set; }
        public RatingViewModel()
        {
            FiveStar = 0;
            FourStar = 0;
            ThreeStar = 0;
            TwoStar = 0;
            OneStar = 0;
            TotalReview = 0;
        }
    }
}

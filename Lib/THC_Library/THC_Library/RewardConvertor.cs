using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THC_Library.Reward
{
    public class RewardConvertor
    {

        public RewardType RewardType
        {
            get;
            set;
        }

        public string RewardName
        {
            get;
            set;
        }

        public string RewardImage
        {
            get;
            set;
        }

        public string CouponNumber
        {
            get;
            set;
        }
    }

    public class Edenred : RewardConvertor
    {
       

        public string ValidPeriod
        {
            get;
            set;
        }
    }

    public class Phyicalenred : RewardConvertor
    {
        public string Description
        {
            get;
            set;
        }                
    }



    public enum RewardType
    {
        ElectricCoupon,
        PhyicalReward
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PatternMatchingDemo.Models
{
    public class RewardsMember
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Purchase> Purchases { get; set; }
    }
}

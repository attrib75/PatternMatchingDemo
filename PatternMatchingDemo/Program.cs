using PatternMatchingDemo.Models;
using System;
using System.Linq;

namespace PatternMatchingDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the C# pattern matching demo!");
            Console.WriteLine("Let\'s calculate the price of Ice cream using the old way and the new pattern matching way.");
            Console.WriteLine("");

            Console.WriteLine("Ice cream sundaes are priced as follows:");
            Console.WriteLine("small: $2.50, medium: $3.50, large: $4.50");
            Console.WriteLine("");

            Console.WriteLine("Cones are priced as follows:");
            Console.WriteLine("small: $1.50, medium: $2.50, large: $3.50");
            Console.WriteLine("");

            ////////////////////////////// CALC SUNDAES

            Sundae smallSundae = new Sundae { Size = Size.Small };
            Sundae medSundae = new Sundae { Size = Size.Medium };
            Sundae largeSundae = new Sundae { Size = Size.Large };

            var oldSmallCalc = ComputeCostOldWay(smallSundae);
            var newSmallCalc = ComputeCostNewWay(smallSundae);

            Console.WriteLine($"Small sundae old way {oldSmallCalc}.  New way {newSmallCalc}.");

            var oldMedCalc = ComputeCostOldWay(medSundae);
            var newMedCalc = ComputeCostNewWay(medSundae);
            var newMedNestedCalc = ComputeCostNewWayNestedSwitches(smallSundae);

            Console.WriteLine($"Med sundae old way {oldMedCalc}.  New way {newMedCalc}.");

            var oldLargeCalc = ComputeCostOldWay(largeSundae);
            var newLargeCalc = ComputeCostNewWay(largeSundae);

            Console.WriteLine($"Large sundae old way {oldLargeCalc}.  New way {newLargeCalc}");
            Console.WriteLine("");

            var smNested = ComputeCostNewWayNestedSwitches(smallSundae);
            var mdNested = ComputeCostNewWayNestedSwitches(medSundae);
            var lgNested = ComputeCostNewWayNestedSwitches(largeSundae);
            Console.WriteLine($"Small sundae nested way {smNested}");
            Console.WriteLine($"Medium sundae nested way {mdNested}");
            Console.WriteLine($"Large sundae nested way {lgNested}");

            Console.WriteLine("");
            ///////////////////////////// CALC CONES

            IceCreamCone smallCone = new IceCreamCone { Size = Size.Small };
            IceCreamCone medCone = new IceCreamCone { Size = Size.Medium };
            IceCreamCone largeCone = new IceCreamCone { Size = Size.Large };

            var oldSmallConeCalc = ComputeCostOldWay(smallCone);
            var newSmallConeCalc = ComputeCostNewWay(smallCone);

            Console.WriteLine($"Small cone old way {oldSmallConeCalc}.  New way {newSmallConeCalc}");

            var oldMedConeCalc = ComputeCostOldWay(medCone);
            var newMedConeCalc = ComputeCostNewWay(medCone);

            Console.WriteLine($"Med cone old way {oldMedConeCalc}.  New way {newMedConeCalc}");

            var oldLargeConeCalc = ComputeCostOldWay(largeCone);
            var newLargeConeCalc = ComputeCostNewWay(largeCone);

            Console.WriteLine($"Large cone old way {oldLargeConeCalc}.  New way {newLargeConeCalc}");

            Console.WriteLine("");

            var smConeNested = ComputeCostNewWayNestedSwitches(smallCone);
            var mdConeNested = ComputeCostNewWayNestedSwitches(medCone);
            var lgConeNested = ComputeCostNewWayNestedSwitches(largeCone);
            Console.WriteLine($"Small cone nested way {smConeNested}");
            Console.WriteLine($"Medium cone nested way {mdConeNested}");
            Console.WriteLine($"Large cone nested way {lgConeNested}");

            Console.WriteLine("");
            /////////////////////////// REWARDS
            Console.WriteLine("Calculate rewards....");
            RewardsMember newbie = new RewardsMember { Purchases = new Purchase[2].ToList() };
            RewardsMember frequent = new RewardsMember { Purchases = new Purchase[5].ToList() };
            RewardsMember regular = new RewardsMember { Purchases = new Purchase[10].ToList() };

            Console.WriteLine($"Newbie member should calculate discount 10 cents.  Actual {ComputeRewardDiscount(newbie)}");
            Console.WriteLine($"Frequent member should calculate discount 50 cents.  Actual {ComputeRewardDiscount(frequent)}");
            Console.WriteLine($"Regular member should calculate discount 2 dollars.  Actual {ComputeRewardDiscount(regular)}");

            Console.ReadLine();
        }

        public static decimal ComputeCostOldWay(object iceCream)
        {
            if (iceCream is Sundae)
            {
                var s = (Sundae)iceCream;
                switch (s.Size)
                {
                    case Size.Small:
                        return 2.5m;

                    case Size.Medium:
                        return 3.5m;

                    case Size.Large:
                        return 4.5m;

                    default:
                        // uh?
                        return 0m;
                }
            }
            else if (iceCream is IceCreamCone)
            {
                var cone = (IceCreamCone)iceCream;
                switch (cone.Size)
                {
                    case Size.Small:
                        return 1.5m;

                    case Size.Medium:
                        return 2.5m;

                    case Size.Large:
                        return 3.5m;

                    default:
                        // uh?
                        return 0m;
                }
            }

            throw new ArgumentException(
                message: "Ice cream is not recognized",
                paramName: nameof(iceCream));
        }

        public static decimal ComputeCostNewWay(object iceCream)
        {
            return iceCream switch
            {
                Sundae { Size: Size.Small } => 2.5m,
                Sundae { Size: Size.Medium } => 3.5m,
                Sundae { Size: Size.Large } => 4.5m,

                IceCreamCone { Size: Size.Small } => 1.5m,
                IceCreamCone { Size: Size.Medium } => 2.5m,
                IceCreamCone { Size: Size.Large } => 3.5m,

                { } => throw new ArgumentException(message: "Unrecognized product", paramName: nameof(iceCream)),
                null => throw new ArgumentNullException(nameof(iceCream))
            };
        }

        public static decimal ComputeCostNewWayNestedSwitches(object iceCream)
        {
            return iceCream switch
            {
                Sundae c => c.Size switch
                {
                    Size.Small => 2.5m,
                    Size.Medium => 3.5m,
                    Size.Large => 4.5m,
                    _ => throw new ArgumentException(message: "Unrecognized size of ice cream", paramName: nameof(iceCream)),
                },

                IceCreamCone c => c.Size switch
                {
                    Size.Small => 1.5m,
                    Size.Medium => 2.5m,
                    Size.Large => 3.5m,
                    _ => throw new ArgumentException(message: "Unrecognized size of ice cream", paramName: nameof(iceCream)),
                },

                // we don't recognize the type
                { } => throw new ArgumentException(message: "Unrecognized product", paramName: nameof(iceCream)),

                // the type is null
                null => throw new ArgumentNullException(nameof(iceCream))
            };
        }

        public static decimal ComputeRewardDiscount(RewardsMember rewardsMember)
        {
            return rewardsMember switch
            {
                RewardsMember newbie when rewardsMember.Purchases?.Count < 3 => .1m,
                RewardsMember frequent when rewardsMember.Purchases?.Count >= 3 && rewardsMember.Purchases?.Count < 10 => .5m,
                RewardsMember regular when rewardsMember.Purchases?.Count >= 10 => 2m,
                null => throw new ArgumentNullException(nameof(rewardsMember)),
                _ => 0,
            };
        }
    }
}

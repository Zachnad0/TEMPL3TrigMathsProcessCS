// GLOBAL USING DIRECTIVES IN C# 10+ ???!!!???!!!???!!!???!!!

namespace L3TrigMathsProcessTEMP
{
	public class Program
	{
		private const int LarryCycleCount = 10;
		private static List<NumRange> _intersectedRanges = new();

		public static void Main()
		{
			string output = "START ===================\n";
			List<NumRange> larryRanges = new(), samRanges = new();

			// Start with getting ranges of Larry and Sam (at dist over 15) for CycleCount
			for (int n = 0; n < LarryCycleCount; n++)
			{
				larryRanges.Add(new NumRange(
					5 * (2 * MathF.PI * n + 1.8235) / (2 * MathF.PI) + 1.5,
					5 * (2 * MathF.PI * (n + 1) - 1.8235) / (2 * MathF.PI) + 1.5
					));

			}
			for (int n = 0; n < Math.Ceiling(LarryCycleCount / 4d * 5d); n++) // Adjust for ratio
			{
				samRanges.Add(new NumRange(
					4 * (2 * MathF.PI * n + 2.3005) / (2 * MathF.PI),
					4 * (2 * MathF.PI * (n + 1) - 2.3005) / (2 * MathF.PI)
					));
			}

			// Larry's ranges cover more time, so compare Sam's ranges to it to compile resulting common ranges
			foreach (NumRange lRange in larryRanges)
			{
				// Find any ranges that intersect the current range, if at all
				IEnumerable<NumRange> nrr;
				nrr = samRanges.Where(sRange => lRange.IsInRange(sRange.Lower));
				double? lowerInRange = nrr.Any() ? nrr.First().Lower : null;
				nrr = samRanges.Where(sRange => lRange.IsInRange(sRange.Upper));
				double? upperInRange = nrr.Any() ? nrr.First().Upper : null;

				// Then make a new range which is the intersection range
				if (lowerInRange.HasValue || upperInRange.HasValue)
					_intersectedRanges.Add(new NumRange(lowerInRange ?? lRange.Lower, upperInRange ?? lRange.Upper));
			}

			_intersectedRanges.ForEach(range => output += range.ToString() + "\n");

			//foreach (NumRange range in larryRanges)
			//	output += range.ToString();
			//output += "\n\n";
			//foreach (NumRange range in samRanges)
			//	output += range.ToString();

			Console.WriteLine(output += "END ===================\n");
			CloseProtocol();
		}

		private static void CloseProtocol()
		{
			Console.WriteLine("Press any key to exit");
			Console.ReadKey();
		}

		private struct NumRange
		{
			public readonly double Lower, Upper;

			public NumRange(double lessThanX, double greaterThanX)
			{
				Lower = lessThanX;
				Upper = greaterThanX;
			}

			public bool IsInRange(double val) => val >= Lower && val <= Upper;

			public override string ToString() => $"({Lower} <= x <= {Upper}) ";
		}
	}
}
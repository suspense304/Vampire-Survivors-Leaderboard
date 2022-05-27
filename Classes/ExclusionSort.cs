using System;
using System.Collections.Generic;
using System.Linq;

namespace Vampire_Survivors_Leaderboard.Classes
{
	// This is totally a made up type of sorting. I am thinking of ways to make ranking not duplicate work over and over
	// like we currently have (ordering, grouping, etc.).
	// Basically, I want to perform the sort and squash at the same time. This means I need the general sorting algorithm of my choice
	// and that when I am building the sorted result, I am also excluding items that won't pass the comparison.
	// The sort algorithm I figure can be based on merge sort.
	// And then when left is compared to right instead of simply ordering them, it:
	//	- orders them if the key criteria is different
	//	- removes one of the items if the key criteria is the same; the item removed is the loser in the comparison criteria
	// This, of course, will be done immutably. Fuck mutable stuff.

	/// <summary>
	/// The merge sort implementation is a modification from https://www.c-sharpcorner.com/blogs/a-simple-merge-sort-implementation-c-sharp
	/// Honestly, I didn't feel like looking at pseudo code or relearning the algorithm because this is such a specialized modification.
	/// I want to focus on the modification rather than an already solved problem (the merge sort).
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TStat"></typeparam>
	public class ExclusionSort<T, TKey, TStat> where T : class
	{
		public ExclusionSort(Func<T, TKey> keySelector, Func<T, T, Func<T, TStat>, int> comparer)
		{
			this.keySelector = keySelector;
			this.comparer = comparer;
		}

		private Func<T, TKey> keySelector;
		private Func<T, T, Func<T, TStat>, int> comparer;

		public List<T> Sort(List<T> input, Func<T, TStat> statSelector)
		{
			if(input.Count <= 1)
			{
				return new List<T>(input);
			}

			var midPoint = input.Count / 2;
			var left = new List<T>(input.Take(midPoint));

			List<T> right = null;

			// Even elements means our right list can be the same length as left.
			// Odd elements means we need to add one to the midpoint to get the correct length
			// because the divide by two above would have been chopped.
			if(input.Count % 2 == 0)
			{
				right = new List<T>(input.Skip(midPoint).Take(midPoint));
			}
			else
			{
				right = new List<T>(input.Skip(midPoint).Take(midPoint + 1));
			}

			var sortedLeft = Sort(left, statSelector);
			var sortedRight = Sort(right, statSelector);

			return Merge(sortedLeft, sortedRight, statSelector);
		}

		protected List<T> Merge(List<T> left, List<T> right, Func<T, TStat> statSelector)
		{
			var resultLength = left.Count + right.Count;
			var result = Enumerable.Repeat<T>(null, resultLength).ToList();

			int idxLeft = 0;
			int idxRight = 0;
			int idxResult = 0;

			while(idxLeft < left.Count || idxRight < right.Count)
			{
				// If both lists still have elements
				if(idxLeft < left.Count && idxRight < right.Count)
				{
					// TODO: Should I extract the comparer out to be something injected in the constructor? Hmmm...
					var keyComparer = Comparer<TKey>.Default;
					var keyComparisonResult = keyComparer.Compare(this.keySelector(left[idxLeft]),  this.keySelector(right[idxRight]));
					var statComparisonResult = this.comparer(left[idxLeft], right[idxRight], statSelector);

					// Keys are the same and stat values are the same, so just pick one and increment past the other.
					if(keyComparisonResult == 0 && statComparisonResult == 0)
					{
						result[idxResult] = left[idxLeft];
						++idxLeft;
						++idxRight;
					}
					// Keys are the same but stat values are different; determine which is "greater than" and keep it.
					// TODO: Technically, I guess this should have a sort direction applied so it's not just arbitrarily "greater than."
					else if(keyComparisonResult == 0 && statComparisonResult != 0)
					{
						// Left is greater than right.
						if(statComparisonResult > 0)
						{
							result[idxResult] = left[idxLeft];							
						}
						// Right is greater than left
						else
						{
							result[idxResult] = right[idxRight];
						}

						// We increment both indices; one of them is eliminated since we're in the "same key" case
						++idxLeft;
						++idxRight;
					}
					// Keys are different and stat values are greater than or equal to each other,
					// so just pick one and increment past the other.
					// TODO: In this case, we should have a key sorter as well so we aren't picking arbitrarily.
					// For now, we'll go with left.
					else if (keyComparisonResult != 0 && statComparisonResult >= 0)
					{
						result[idxResult] = left[idxLeft];
						++idxLeft;
					}
					else
					{
						result[idxResult] = right[idxRight];
						++idxRight;
					}
				}
				// If only left list has elements
				else if(idxLeft < left.Count)
				{
					result[idxResult] = left[idxLeft];
					++idxLeft;
				}
				// If only right list has elements
				else if(idxRight < right.Count)
				{
					result[idxResult] = right[idxRight];
					++idxRight;
				}

				++idxResult;
			}

			return result;
		}
	}
}

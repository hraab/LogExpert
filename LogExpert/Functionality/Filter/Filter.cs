﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LogExpert
{
	delegate void FilterFx(FilterParams filterParams, List<int> filterResultLines, List<int> lastFilterResultLines, List<int> filterHitList);

	public class Filter
	{
		private const int PROGRESS_BAR_MODULO = 1000;
		private const int SPREAD_MAX = 50;

		private LogExpert.ColumnizerCallback _callback;
		private bool _shouldCancel = false;

		public Filter(LogExpert.ColumnizerCallback callback)
		{
			this._callback = callback;
			this.FilterResultLines = new List<int>();
			this.LastFilterLinesList = new List<int>();
			this.FilterHitList = new List<int>();
		}

		public List<int> FilterResultLines { get; private set; }

		public List<int> LastFilterLinesList { get; private set; }

		public List<int> FilterHitList { get; private set; }

		public bool ShouldCancel
		{
			get
			{
				return this._shouldCancel;
			}
			set
			{
				this._shouldCancel = value;
			}
		}

		public int DoFilter(FilterParams filterParams, int startLine, int maxCount, ProgressCallback progressCallback)
		{
			return DoFilter(filterParams, startLine, maxCount, this.FilterResultLines, this.LastFilterLinesList, this.FilterHitList, progressCallback);
		}

		private int DoFilter(FilterParams filterParams, int startLine, int maxCount, List<int> filterResultLines, List<int> lastFilterLinesList, List<int> filterHitList, ProgressCallback progressCallback)
		{
			int lineNum = startLine;
			int count = 0;
			int callbackCounter = 0;

			try
			{
				filterParams.Reset();
				while ((count++ < maxCount || filterParams.isInRange) && !this.ShouldCancel)
				{
					if (lineNum >= this._callback.GetLineCount())
					{
						return count;
					}
					string line = this._callback.GetLogLine(lineNum);
					if (line == null)
					{
						return count;
					}
					this._callback.LineNum = lineNum;
					if (Classes.DamerauLevenshtein.TestFilterCondition(filterParams, line, _callback))
					{
						AddFilterLine(lineNum, false, filterParams, filterResultLines, lastFilterLinesList, filterHitList);
					}
					lineNum++;
					callbackCounter++;
					if (lineNum % PROGRESS_BAR_MODULO == 0)
					{
						progressCallback(callbackCounter);
						callbackCounter = 0;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.logError("Exception while filtering. Please report to developer: \n\n" + ex + "\n\n" + ex.StackTrace);
				MessageBox.Show(null, "Exception while filtering. Please report to developer: \n\n" + ex + "\n\n" + ex.StackTrace, "LogExpert");
			}
			return count;
		}

		private void AddFilterLine(int lineNum, bool immediate, FilterParams filterParams, List<int> filterResultLines, List<int> lastFilterLinesList, List<int> filterHitList)
		{
			int count;
			filterHitList.Add(lineNum);
			IList<int> filterResult = GetAdditionalFilterResults(filterParams, lineNum, lastFilterLinesList);
			filterResultLines.AddRange(filterResult);
			count = filterResultLines.Count;
			lastFilterLinesList.AddRange(filterResult);
			if (lastFilterLinesList.Count > SPREAD_MAX * 2)
			{
				lastFilterLinesList.RemoveRange(0, lastFilterLinesList.Count - SPREAD_MAX * 2);
			}
		}

		/// <summary>
		///  Returns a list with 'additional filter results'. This is the given line number 
		///  and (if back spread and/or fore spread is enabled) some additional lines.
		///  This function doesn't check the filter condition! 
		/// </summary>
		/// <param name="filterParams"></param>
		/// <param name="lineNum"></param>
		/// <param name="checkList"></param>
		/// <returns></returns>
		private IList<int> GetAdditionalFilterResults(FilterParams filterParams, int lineNum, IList<int> checkList)
		{
			IList<int> resultList = new List<int>();

			if (filterParams.spreadBefore == 0 && filterParams.spreadBehind == 0)
			{
				resultList.Add(lineNum);
				return resultList;
			}

			// back spread
			for (int i = filterParams.spreadBefore; i > 0; --i)
			{
				if (lineNum - i > 0)
				{
					if (!resultList.Contains(lineNum - i) && !checkList.Contains(lineNum - i))
						resultList.Add(lineNum - i);
				}
			}
			// direct filter hit
			if (!resultList.Contains(lineNum) && !checkList.Contains(lineNum))
			{
				resultList.Add(lineNum);
			}
			// after spread
			for (int i = 1; i <= filterParams.spreadBehind; ++i)
			{
				if (lineNum + i < this._callback.GetLineCount())
				{
					if (!resultList.Contains(lineNum + i) && !checkList.Contains(lineNum + i))
						resultList.Add(lineNum + i);
				}
			}
			return resultList;
		}
	}
}
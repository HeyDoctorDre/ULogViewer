﻿using CarinaStudio.ULogViewer.Converters;
using CarinaStudio.ULogViewer.Logs;
using CarinaStudio.ULogViewer.Logs.Profiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CarinaStudio.ULogViewer.ViewModels
{
	/// <summary>
	/// Property of <see cref="DisplayableLog"/> to be shown on UI.
	/// </summary>
	class DisplayableLogProperty : BaseDisposable, INotifyPropertyChanged
	{
		// Static fields.
		static IList<string>? displayNames;


		// Fields.
		readonly IULogViewerApplication app;
		readonly string displayNameId;


		/// <summary>
		/// Initialize new <see cref="DisplayableLogProperty"/> instance.
		/// </summary>
		/// <param name="app">Application.</param>
		/// <param name="name">Name of property.</param>
		/// <param name="displayName">Name for displaying.</param>
		/// <param name="width">Width of UI field to show property in pixels.</param>
		public DisplayableLogProperty(IULogViewerApplication app, string name, string? displayName, int? width)
		{
			this.app = app;
			this.displayNameId = displayName ?? name;
			this.DisplayName = LogPropertyNameConverter.Default.Convert(this.displayNameId);
			this.ForegroundColor = LogPropertyForegroundColor.Level;
			this.Name = name;
			this.NameForLogProperty = name switch
			{
				nameof(DisplayableLog.BeginningTimeSpanString) => nameof(Log.BeginningTimeSpan),
				nameof(DisplayableLog.BeginningTimestampString) => nameof(Log.BeginningTimestamp),
				nameof(DisplayableLog.EndingTimeSpanString) => nameof(Log.EndingTimeSpan),
				nameof(DisplayableLog.EndingTimestampString) => nameof(Log.EndingTimestamp),
				nameof(DisplayableLog.LevelString) => nameof(Log.Level),
				nameof(DisplayableLog.ReadTimeString) => nameof(Log.ReadTime),
				nameof(DisplayableLog.TimeSpanString) => nameof(Log.TimeSpan),
				nameof(DisplayableLog.TimestampString) => nameof(Log.Timestamp),
				_ => name,
			};
			this.Width = width;
			app.StringsUpdated += this.OnApplicationStringsUpdated;
		}


		/// <summary>
		/// Initialize new <see cref="DisplayableLogProperty"/> instance.
		/// </summary>
		/// <param name="app">Application.</param>
		/// <param name="logProperty"><see cref="LogProperty"/> defined in <see cref="LogProfile"/>.</param>
		public DisplayableLogProperty(IULogViewerApplication app, LogProperty logProperty)
		{
			this.app = app;
			this.displayNameId = logProperty.DisplayName;
			this.Name = logProperty.Name switch
			{
				nameof(Log.BeginningTimeSpan) => nameof(DisplayableLog.BeginningTimeSpanString),
				nameof(Log.BeginningTimestamp) => nameof(DisplayableLog.BeginningTimestampString),
				nameof(Log.EndingTimeSpan) => nameof(DisplayableLog.EndingTimeSpanString),
				nameof(Log.EndingTimestamp) => nameof(DisplayableLog.EndingTimestampString),
				nameof(Log.Level) => nameof(DisplayableLog.LevelString),
				nameof(Log.ReadTime) => nameof(DisplayableLog.ReadTimeString),
				nameof(Log.TimeSpan) => nameof(DisplayableLog.TimeSpanString),
				nameof(Log.Timestamp) => nameof(DisplayableLog.TimestampString),
				_ => logProperty.Name,
			};
			this.NameForLogProperty = logProperty.Name;
			this.DisplayName = LogPropertyNameConverter.Default.Convert(this.displayNameId);
			this.ForegroundColor = logProperty.ForegroundColor;
			this.Quantifier = logProperty.Quantifier;
			this.SecondaryDisplayName = logProperty.SecondaryDisplayName;
			this.Width = logProperty.Width;
			app.StringsUpdated += this.OnApplicationStringsUpdated;
		}


		/// <summary>
		/// Get name for displaying.
		/// </summary>
		public string DisplayName { get; private set; }


		/// <summary>
		/// Get available display names.
		/// </summary>
		public static IList<string> DisplayNames
		{
			get
			{
				displayNames ??= new List<string>(Log.PropertyNames).Also(it =>
				{
					it.Add("Action");
					it.Add("Author");
					it.Add("Available");
					it.Add("Average");
					it.Add("BeginningValue");
					it.Add("Child");
					it.Add("Children");
					it.Add("Code");
					it.Add("Commit");
					it.Add("Content");
					it.Add("Count");
					it.Add("Cpu");
					it.Add("DestinationName");
					it.Add("Duration");
					it.Add("End");
					it.Add("EndingValue");
					it.Add("EndPoint");
					it.Add("ID");
					it.Add("Invalid");
					it.Add("Key");
					it.Add("Maximum");
					it.Add("Method");
					it.Add("Minimum");
					it.Add("Name");
					it.Add("Node");
					it.Add("Number");
					it.Add("Operation");
					it.Add("Parameter");
					it.Add("Parameter1");
					it.Add("Parameter10");
					it.Add("Parameter2");
					it.Add("Parameter3");
					it.Add("Parameter4");
					it.Add("Parameter5");
					it.Add("Parameter6");
					it.Add("Parameter7");
					it.Add("Parameter8");
					it.Add("Parameter9");
					it.Add("Parent");
					it.Add("Path");
					it.Add("RawData");
					it.Add("Referer");
					it.Add("RelativeBeginningTimestamp");
					it.Add("RelativeEndingTimestamp");
					it.Add("RelativeTimestamp");
					it.Add("Remaining");
					it.Add("Request");
					it.Add("Response");
					it.Add("Result");
					it.Add("ShortName");
					it.Add("Size");
					it.Add("Speed");
					it.Add("Start");
					it.Add("Status");
					it.Add("Sum");
					it.Add("Tag");
					it.Add("Type");
					it.Add("Total");
					it.Add("Unavailable");
					it.Add("Uri");
					it.Add("UserAgent");
					it.Add("Using");
					it.Add("Valid");
					it.Add("Value");
					it.Add("Value1");
					it.Add("Value10");
					it.Add("Value2");
					it.Add("Value3");
					it.Add("Value4");
					it.Add("Value5");
					it.Add("Value6");
					it.Add("Value7");
					it.Add("Value8");
					it.Add("Value9");
					it.Sort();
				}).AsReadOnly();
				return displayNames;
			}
		}


		// Dispose.
		protected override void Dispose(bool disposing)
		{
			this.app.StringsUpdated -= this.OnApplicationStringsUpdated;
		}


		/// <summary>
		/// Get foreground color.
		/// </summary>
		public LogPropertyForegroundColor ForegroundColor { get; }


		/// <summary>
		/// Name of property.
		/// </summary>
		public string Name { get; }


		/// <summary>
		/// Get name of property which is mapped to <see cref="Log"/>.
		/// </summary>
		public string NameForLogProperty { get; }


		// Called when application strings updated.
		void OnApplicationStringsUpdated(object? sender, EventArgs e)
		{
			var newDisplayName = LogPropertyNameConverter.Default.Convert(this.displayNameId);
			if (this.DisplayName == newDisplayName)
				return;
			this.DisplayName = newDisplayName;
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayName)));
		}


		/// <summary>
		/// Raised when property changed.
		/// </summary>
		public event PropertyChangedEventHandler? PropertyChanged;
		
		
		/// <summary>
		/// Quantifier to display on UI.
		/// </summary>
		public string? Quantifier { get; }
		
		
		/// <summary>
		/// Secondary name to display on UI.
		/// </summary>
		public string? SecondaryDisplayName { get; }


		/// <summary>
		/// Convert to <see cref="LogProperty"/>.
		/// </summary>
		/// <param name="resolveDisplayName">True to resolve display name to readable value.</param>
		/// <returns><see cref="LogProperty"/>.</returns>
		public LogProperty ToLogProperty(bool resolveDisplayName = true) =>
			new(this.NameForLogProperty, resolveDisplayName ? this.DisplayName : this.displayNameId, this.SecondaryDisplayName, this.Quantifier, this.ForegroundColor, this.Width);


		/// <summary>
		/// Width of UI field to show property in pixels.
		/// </summary>
		public int? Width { get; }
	}
}

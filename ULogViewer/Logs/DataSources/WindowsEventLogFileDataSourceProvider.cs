using CarinaStudio.Collections;
using System.Collections.Generic;

namespace CarinaStudio.ULogViewer.Logs.DataSources;

class WindowsEventLogFileDataSourceProvider : BaseLogDataSourceProvider
{
    // Constructor.
    public WindowsEventLogFileDataSourceProvider(IULogViewerApplication app) : base(app)
    { 
        this.SupportedSourceOptions = new HashSet<string>()
        {
            nameof(LogDataSourceOptions.FileName),
        }.AsReadOnly();
    }


    /// <inheritdoc/>
    protected override ILogDataSource CreateSourceCore(LogDataSourceOptions options) =>
        new WindowsEventLogFileDataSource(this, options);


    /// <inheritdoc/>
    public override string Name => "WindowsEventLogFile";


    /// <inheritdoc/>
    public override ISet<string> RequiredSourceOptions => this.SupportedSourceOptions;


    /// <inheritdoc/>
    public override ISet<string> SupportedSourceOptions { get; }
}
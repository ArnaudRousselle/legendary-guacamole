using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class ImportFile : WorkspaceQuery<ImportFileInput, ImportFileResult, ImportFileOutput>
{
    public override ImportFileOutput Map(Workspace workspace, ImportFileResult evt)
    {
        throw new NotImplementedException();
    }
}

public class ImportFileResult
{
}
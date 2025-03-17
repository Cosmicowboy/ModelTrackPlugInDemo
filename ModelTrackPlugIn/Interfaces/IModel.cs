using System;

namespace ModelTrackPlugIn.Interfaces
{
    internal interface IModel
    {
        string FullName { get; }
        string ModelDesignDate { get; }
        string FilePath { get; }
        DateTime ModelFileImportDate { get;}

    }
}
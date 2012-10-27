using System;

namespace PeerCentral.Domain
{
    /// <summary>
    /// Describes an accomplishment that the user wants 
    /// to brag about (i.e. share with their peers)
    /// </summary>
    public interface IBrag
    {
        IUser Author { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        DateTime SubmittedOn { get; set; }
    }
}
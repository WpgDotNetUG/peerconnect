using System;
using PeerCentral.Domain;

namespace PeerCentral.WebClient.Models
{
    public class User : IUser
    {
        public String Name { get; set; }

        public int? Id { get; set; }
    }

    public class Brag : IBrag
    {
        public int? Id { get; set; }
        public IUser Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime SubmittedOn { get; set; }
    }
}
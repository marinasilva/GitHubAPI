using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Way2Teste02.Models
{
    public class GitHubRepository
    {
        public int Id { get; set; }
        public string OpenIssues { get; set; }
        public int Watchers { get; set; }
        public DateTime? PushedAt { get; set; }
        public string Homepage { get; set; }
        public string SvnUrl { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string MirrorUrl { get; set; }
        public bool HasDownloads { get; set; }
        public string Url { get; set; }
        public bool HasIssues { get; set; }
        public string Language { get; set; }
        public bool Fork { get; set; }
        public string SshUrl { get; set; }
        public string HtmlUrl { get; set; }
        public int Forks { get; set; }
        public string CloneUrl { get; set; }
        public int Size { get; set; }
        public string GitUrl { get; set; }
        public bool Private { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool HasWiki { get; set; }
        public GitHubUser Owner { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
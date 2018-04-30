using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class Users
    {
        public Users()
        {
            Activity = new HashSet<Activity>();
            Attachments = new HashSet<Attachments>();
            Bookmarks = new HashSet<Bookmarks>();
            CommentLikes = new HashSet<CommentLikes>();
            Comments = new HashSet<Comments>();
            DocumentLikes = new HashSet<DocumentLikes>();
            DocumentText = new HashSet<DocumentText>();
            Documents = new HashSet<Documents>();
            FollowedProjects = new HashSet<FollowedProjects>();
            LastSeenDocuments = new HashSet<LastSeenDocuments>();
            Notifications = new HashSet<Notifications>();
            ProjectTeam = new HashSet<ProjectTeam>();
            QuickLinks = new HashSet<QuickLinks>();
            RefreshTokens = new HashSet<RefreshTokens>();
            TemplateText = new HashSet<TemplateText>();
            Templates = new HashSet<Templates>();
            UserCompetences = new HashSet<UserCompetences>();
            UserRoles = new HashSet<UserRoles>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public DateTime? DateRegistered { get; set; }

        public NotificationsRead NotificationsRead { get; set; }
        public ICollection<Activity> Activity { get; set; }
        public ICollection<Attachments> Attachments { get; set; }
        public ICollection<Bookmarks> Bookmarks { get; set; }
        public ICollection<CommentLikes> CommentLikes { get; set; }
        public ICollection<Comments> Comments { get; set; }
        public ICollection<DocumentLikes> DocumentLikes { get; set; }
        public ICollection<DocumentText> DocumentText { get; set; }
        public ICollection<Documents> Documents { get; set; }
        public ICollection<FollowedProjects> FollowedProjects { get; set; }
        public ICollection<LastSeenDocuments> LastSeenDocuments { get; set; }
        public ICollection<Notifications> Notifications { get; set; }
        public ICollection<ProjectTeam> ProjectTeam { get; set; }
        public ICollection<QuickLinks> QuickLinks { get; set; }
        public ICollection<RefreshTokens> RefreshTokens { get; set; }
        public ICollection<TemplateText> TemplateText { get; set; }
        public ICollection<Templates> Templates { get; set; }
        public ICollection<UserCompetences> UserCompetences { get; set; }
        public ICollection<UserRoles> UserRoles { get; set; }
    }
}

using System;
using kms.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace kms.Data
{
    public partial class KMSDBContext : DbContext
    {
        public KMSDBContext(DbContextOptions<KMSDBContext> options) : base(options) {}

        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<Attachments> Attachments { get; set; }
        public virtual DbSet<Bookmarks> Bookmarks { get; set; }
        public virtual DbSet<CommentLikes> CommentLikes { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Competences> Competences { get; set; }
        public virtual DbSet<DocumentLikes> DocumentLikes { get; set; }
        public virtual DbSet<Documents> Documents { get; set; }
        public virtual DbSet<DocumentText> DocumentText { get; set; }
        public virtual DbSet<FollowedProjects> FollowedProjects { get; set; }
        public virtual DbSet<InviteTokens> InviteTokens { get; set; }
        public virtual DbSet<LastSeenDocuments> LastSeenDocuments { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<NotificationsRead> NotificationsRead { get; set; }
        public virtual DbSet<NotificationTypes> NotificationTypes { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<ProjectPermissions> ProjectPermissions { get; set; }
        public virtual DbSet<ProjectRolePermissions> ProjectRolePermissions { get; set; }
        public virtual DbSet<ProjectRoles> ProjectRoles { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<ProjectTeam> ProjectTeam { get; set; }
        public virtual DbSet<QuickLinks> QuickLinks { get; set; }
        public virtual DbSet<RefreshTokens> RefreshTokens { get; set; }
        public virtual DbSet<RolePermissions> RolePermissions { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Templates> Templates { get; set; }
        public virtual DbSet<TemplateText> TemplateText { get; set; }
        public virtual DbSet<TemplateTypes> TemplateTypes { get; set; }
        public virtual DbSet<UserCompetences> UserCompetences { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.ToTable("activity");

                entity.Property(e => e.ActivityId).HasColumnName("activity_id");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.Meta)
                    .HasColumnName("meta")
                    .HasColumnType("json");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.TimeFired).HasColumnName("time_fired");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Activity)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("fk_activity_projects_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Activity)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_activity_users_1");
            });

            modelBuilder.Entity<Attachments>(entity =>
            {
                entity.HasKey(e => e.AttachmentId);

                entity.ToTable("attachments");

                entity.Property(e => e.AttachmentId).HasColumnName("attachment_id");

                entity.Property(e => e.DocumentId).HasColumnName("document_id");

                entity.Property(e => e.Link).HasColumnName("link");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.TimeCreated).HasColumnName("time_created");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.DocumentId)
                    .HasConstraintName("fk_attachments_documents_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_attachments_users_1");
            });

            modelBuilder.Entity<Bookmarks>(entity =>
            {
                entity.HasKey(e => e.BookmarkId);

                entity.ToTable("bookmarks");

                entity.Property(e => e.BookmarkId).HasColumnName("bookmark_id");

                entity.Property(e => e.DocumentId).HasColumnName("document_id");

                entity.Property(e => e.TimeCreated).HasColumnName("time_created");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.Bookmarks)
                    .HasForeignKey(d => d.DocumentId)
                    .HasConstraintName("fk_bookmarks_documents_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bookmarks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_bookmarks_users_1");
            });

            modelBuilder.Entity<CommentLikes>(entity =>
            {
                entity.HasKey(e => e.CommentLikeId);

                entity.ToTable("comment_likes");

                entity.Property(e => e.CommentLikeId).HasColumnName("comment_like_id");

                entity.Property(e => e.CommentId).HasColumnName("comment_id");

                entity.Property(e => e.TimeCreated).HasColumnName("time_created");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.CommentLikes)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("fk_comment_likes_comments_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CommentLikes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_comment_likes_users_1");
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(e => e.CommentId);

                entity.ToTable("comments");

                entity.Property(e => e.CommentId)
                    .HasColumnName("comment_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.DocumentId).HasColumnName("document_id");

                entity.Property(e => e.TimeCreated).HasColumnName("time_created");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.DocumentId)
                    .HasConstraintName("fk_comments_documents_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_comments_users_1");
            });

            modelBuilder.Entity<Competences>(entity =>
            {
                entity.HasKey(e => e.CompetenceId);

                entity.ToTable("competences");

                entity.Property(e => e.CompetenceId)
                    .HasColumnName("competence_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<DocumentLikes>(entity =>
            {
                entity.HasKey(e => e.DocumentLikeId);

                entity.ToTable("document_likes");

                entity.Property(e => e.DocumentLikeId).HasColumnName("document_like_id");

                entity.Property(e => e.DocumentId).HasColumnName("document_id");

                entity.Property(e => e.TimeCreated).HasColumnName("time_created");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.DocumentLikes)
                    .HasForeignKey(d => d.DocumentId)
                    .HasConstraintName("fk_document_likes_documents_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DocumentLikes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_document_likes_users_1");
            });

            modelBuilder.Entity<Documents>(entity =>
            {
                entity.HasKey(e => e.DocumentId);

                entity.ToTable("documents");

                entity.HasIndex(e => e.Slug)
                    .HasName("document_slug")
                    .IsUnique();

                entity.Property(e => e.DocumentId).HasColumnName("document_id");

                entity.Property(e => e.CreatorId).HasColumnName("creator_id");

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.IsDraft).HasColumnName("is_draft");

                entity.Property(e => e.ParentDocumentId).HasColumnName("parent_document_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasColumnName("slug");

                entity.Property(e => e.Subtitle).HasColumnName("subtitle");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_documents_users_1");

                entity.HasOne(d => d.ParentDocument)
                    .WithMany(p => p.InverseParentDocument)
                    .HasForeignKey(d => d.ParentDocumentId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_documents_documents_1");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("fk_documents_projects_1");
            });

            modelBuilder.Entity<DocumentText>(entity =>
            {
                entity.ToTable("document_text");

                entity.Property(e => e.DocumentTextId).HasColumnName("document_text_id");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.DocumentId).HasColumnName("document_id");

                entity.Property(e => e.EditorId).HasColumnName("editor_id");

                entity.Property(e => e.IsActual).HasColumnName("is_actual");

                entity.Property(e => e.QuillDelta)
                    .HasColumnName("quill_delta")
                    .HasColumnType("json");

                entity.Property(e => e.TimeUpdated).HasColumnName("time_updated");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.DocumentText)
                    .HasForeignKey(d => d.DocumentId)
                    .HasConstraintName("fk_document_text_documents_1");

                entity.HasOne(d => d.Editor)
                    .WithMany(p => p.DocumentText)
                    .HasForeignKey(d => d.EditorId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_document_text_users_1");
            });

            modelBuilder.Entity<FollowedProjects>(entity =>
            {
                entity.ToTable("followed_projects");

                entity.Property(e => e.FollowedProjectsId).HasColumnName("followed_projects_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.TimeCreated).HasColumnName("time_created");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.FollowedProjects)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("fk_followed_projects_projects_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FollowedProjects)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_followed_projects_users_1");
            });

            modelBuilder.Entity<InviteTokens>(entity =>
            {
                entity.HasKey(e => e.InviteTokenId);

                entity.ToTable("invite_tokens");

                entity.Property(e => e.InviteTokenId).HasColumnName("invite_token_id");

                entity.Property(e => e.TimeCreated).HasColumnName("time_created");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnName("token");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.InviteTokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_invite_tokens_users_1");
            });

            modelBuilder.Entity<LastSeenDocuments>(entity =>
            {
                entity.ToTable("last_seen_documents");

                entity.Property(e => e.LastSeenDocumentsId).HasColumnName("last_seen_documents_id");

                entity.Property(e => e.DocumentId).HasColumnName("document_id");

                entity.Property(e => e.TimeCreated)
                    .HasColumnName("time_created")
                    .HasColumnType("date");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.LastSeenDocuments)
                    .HasForeignKey(d => d.DocumentId)
                    .HasConstraintName("fk_last_seen_documents_documents_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LastSeenDocuments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_last_seen_documents_users_1");
            });

            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.HasKey(e => e.NotificationId);

                entity.ToTable("notifications");

                entity.Property(e => e.NotificationId).HasColumnName("notification_id");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.Meta)
                    .HasColumnName("meta")
                    .HasColumnType("json");

                entity.Property(e => e.NotificationTypeSlug)
                    .IsRequired()
                    .HasColumnName("notification_type_slug");

                entity.Property(e => e.TimeFired).HasColumnName("time_fired");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.NotificationTypeSlugNavigation)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.NotificationTypeSlug)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_notifications_notification_types_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_notifications_users_1");
            });

            modelBuilder.Entity<NotificationsRead>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("notifications_read");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.TimeRead).HasColumnName("time_read");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.NotificationsRead)
                    .HasForeignKey<NotificationsRead>(d => d.UserId)
                    .HasConstraintName("fk_notifications_read_users_1");
            });

            modelBuilder.Entity<NotificationTypes>(entity =>
            {
                entity.HasKey(e => e.NotificationTypeSlug);

                entity.ToTable("notification_types");

                entity.Property(e => e.NotificationTypeSlug)
                    .HasColumnName("notification_type_slug")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Permissions>(entity =>
            {
                entity.HasKey(e => e.PermissionSlug);

                entity.ToTable("permissions");

                entity.Property(e => e.PermissionSlug)
                    .HasColumnName("permission_slug")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<ProjectPermissions>(entity =>
            {
                entity.HasKey(e => e.ProjectPermissionSlug);

                entity.ToTable("project_permissions");

                entity.Property(e => e.ProjectPermissionSlug)
                    .HasColumnName("project_permission_slug")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<ProjectRolePermissions>(entity =>
            {
                entity.HasKey(e => new { e.ProjectRoleId, e.ProjectPermissionSlug });

                entity.ToTable("project_role_permissions");

                entity.Property(e => e.ProjectRoleId).HasColumnName("project_role_id");

                entity.Property(e => e.ProjectPermissionSlug).HasColumnName("project_permission_slug");

                entity.HasOne(d => d.ProjectPermissionSlugNavigation)
                    .WithMany(p => p.ProjectRolePermissions)
                    .HasForeignKey(d => d.ProjectPermissionSlug)
                    .HasConstraintName("fk_role_permissions_permissions_1");

                entity.HasOne(d => d.ProjectRole)
                    .WithMany(p => p.ProjectRolePermissions)
                    .HasForeignKey(d => d.ProjectRoleId)
                    .HasConstraintName("fk_role_permissions_roles_1");
            });

            modelBuilder.Entity<ProjectRoles>(entity =>
            {
                entity.HasKey(e => e.ProjectRoleId);

                entity.ToTable("project_roles");

                entity.Property(e => e.ProjectRoleId).HasColumnName("project_role_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.System).HasColumnName("system");
            });

            modelBuilder.Entity<Projects>(entity =>
            {
                entity.HasKey(e => e.ProjectId);

                entity.ToTable("projects");

                entity.HasIndex(e => e.Slug)
                    .HasName("project_slug")
                    .IsUnique();

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.Avatar).HasColumnName("avatar");

                entity.Property(e => e.DateEnd)
                    .HasColumnName("date_end")
                    .HasColumnType("date");

                entity.Property(e => e.DateStart)
                    .HasColumnName("date_start")
                    .HasColumnType("date");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Goal).HasColumnName("goal");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsOpen).HasColumnName("is_open");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasColumnName("slug");
            });

            modelBuilder.Entity<ProjectTeam>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ProjectId });

                entity.ToTable("project_team");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.DateJoined).HasColumnName("date_joined");

                entity.Property(e => e.Position).HasColumnName("position");

                entity.Property(e => e.ProjectRoleId).HasColumnName("project_role_id");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectTeam)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("fk_project_team_projects_1");

                entity.HasOne(d => d.ProjectRole)
                    .WithMany(p => p.ProjectTeam)
                    .HasForeignKey(d => d.ProjectRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_project_team_project_roles_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProjectTeam)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_project_team_users_1");
            });

            modelBuilder.Entity<QuickLinks>(entity =>
            {
                entity.HasKey(e => e.QuickLinkId);

                entity.ToTable("quick_links");

                entity.Property(e => e.QuickLinkId).HasColumnName("quick_link_id");

                entity.Property(e => e.DocumentId).HasColumnName("document_id");

                entity.Property(e => e.ExternalLink).HasColumnName("external_link");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.QuickLinks)
                    .HasForeignKey(d => d.DocumentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_quick_links_documents_1");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.QuickLinks)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_quick_links_projects_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.QuickLinks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_quick_links_users_1");
            });

            modelBuilder.Entity<RefreshTokens>(entity =>
            {
                entity.HasKey(e => e.RefreshTokenId);

                entity.ToTable("refresh_tokens");

                entity.Property(e => e.RefreshTokenId).HasColumnName("refresh_token_id");

                entity.Property(e => e.Revoked).HasColumnName("revoked");

                entity.Property(e => e.TimeCreated).HasColumnName("time_created");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnName("token");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_refresh_tokens_users_1");
            });

            modelBuilder.Entity<RolePermissions>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.PermissionSlug });

                entity.ToTable("role_permissions");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.PermissionSlug).HasColumnName("permission_slug");

                entity.HasOne(d => d.PermissionSlugNavigation)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.PermissionSlug)
                    .HasConstraintName("fk_role_permissions_permissions_1");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("fk_role_permissions_roles_1");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("roles");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.System).HasColumnName("system");
            });

            modelBuilder.Entity<Templates>(entity =>
            {
                entity.HasKey(e => e.TemplateId);

                entity.ToTable("templates");

                entity.HasIndex(e => e.Slug)
                    .HasName("template_slug")
                    .IsUnique();

                entity.Property(e => e.TemplateId).HasColumnName("template_id");

                entity.Property(e => e.CreatorId).HasColumnName("creator_id");

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasColumnName("slug");

                entity.Property(e => e.TemplateTypeSlug)
                    .IsRequired()
                    .HasColumnName("template_type_slug");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Templates)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_templates_users_1");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Templates)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_templates_projects_1");

                entity.HasOne(d => d.TemplateTypeSlugNavigation)
                    .WithMany(p => p.Templates)
                    .HasForeignKey(d => d.TemplateTypeSlug)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_templates_template_types_1");
            });

            modelBuilder.Entity<TemplateText>(entity =>
            {
                entity.ToTable("template_text");

                entity.Property(e => e.TemplateTextId).HasColumnName("template_text_id");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.EditorId).HasColumnName("editor_id");

                entity.Property(e => e.IsActual).HasColumnName("is_actual");

                entity.Property(e => e.QuillDelta)
                    .HasColumnName("quill_delta")
                    .HasColumnType("json");

                entity.Property(e => e.TemplateId).HasColumnName("template_id");

                entity.Property(e => e.TimeUpdated).HasColumnName("time_updated");

                entity.HasOne(d => d.Editor)
                    .WithMany(p => p.TemplateText)
                    .HasForeignKey(d => d.EditorId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_template_text_users_1");

                entity.HasOne(d => d.Template)
                    .WithMany(p => p.TemplateText)
                    .HasForeignKey(d => d.TemplateId)
                    .HasConstraintName("fk_template_text_templates_1");
            });

            modelBuilder.Entity<TemplateTypes>(entity =>
            {
                entity.HasKey(e => e.TemplateTypeSlug);

                entity.ToTable("template_types");

                entity.Property(e => e.TemplateTypeSlug)
                    .HasColumnName("template_type_slug")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<UserCompetences>(entity =>
            {
                entity.HasKey(e => new { e.CompetenceId, e.UserId });

                entity.ToTable("user_competences");

                entity.Property(e => e.CompetenceId)
                    .HasColumnName("competence_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .ValueGeneratedOnAdd();

                entity.HasOne(d => d.Competence)
                    .WithMany(p => p.UserCompetences)
                    .HasForeignKey(d => d.CompetenceId)
                    .HasConstraintName("fk_user_competences_competences_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCompetences)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_user_competences_users_1");
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.ToTable("user_roles");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("fk_user_roles_roles_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_user_roles_users_1");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("users");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Avatar)
                    .HasColumnName("avatar")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.DateRegistered).HasColumnName("date_registered");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.Surname).HasColumnName("surname");
            });
        }
    }
}

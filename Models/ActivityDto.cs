using System;
using kms.Data.Entities;

namespace kms.Models
{
    public class ActivityDto
    {
        public ActivityDto(Activity activity)
        {
            if (activity != null) {
                this.ActivityId = activity.ActivityId;
                this.Content = activity.Content;
                this.TimeFired = activity.TimeFired;
                this.Meta = activity.Meta;

                if (activity.User != null) {
                    this.Issuer = new ProfileShortDto(activity.User);
                }

                if (activity.Project != null) {
                    ProjectName = activity.Project.Name;
                    ProjectSlug = activity.Project.Slug;
                    ProjectId = activity.Project.ProjectId;
                }
            }
        }
        public int ActivityId { get; set; }
        public string Content { get; set; }
        public DateTime TimeFired { get; set; }
        public string Meta { get; set; }

        public string ProjectName { get; set; }
        public string ProjectSlug { get; set; }
        public int ProjectId { get; set; }
        public ProfileShortDto Issuer { get; set; }
    }
}

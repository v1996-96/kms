using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using kms.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace kms.Data.Seed
{
    public class RoleSerializer {
        public string Name { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
    public static class DbSeeder
    {
        public static async Task EnsureSeeded(this KMSDBContext db) {
            var dirname = Path.Combine("Data", "Seed");
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new DefaultContractResolver{ NamingStrategy = new SnakeCaseNamingStrategy() };
            serializerSettings.Formatting = Formatting.Indented;

            await SeedTemplateTypes(db, dirname, serializerSettings);
            await SeedPermissions(db, dirname, serializerSettings);
            await SeedRoles(db, dirname, serializerSettings);
            await SeedProjectPermissions(db, dirname, serializerSettings);
            await SeedProjectRoles(db, dirname, serializerSettings);
        }

        private static async Task SeedTemplateTypes(KMSDBContext db, string dirname, JsonSerializerSettings serializerSettings) {
            var templateTypesJson = await File.ReadAllTextAsync(Path.Combine(dirname, "template_types.json"));
            var templateTypes = JsonConvert.DeserializeObject<List<TemplateTypes>>(templateTypesJson, serializerSettings);
            foreach (var item in templateTypes) {
                var existingType = await db.TemplateTypes.SingleOrDefaultAsync(t => t.TemplateTypeSlug == item.TemplateTypeSlug);
                if (existingType == null) {
                    db.TemplateTypes.Add(item);
                }
            }
            await db.SaveChangesAsync();
        }
        private static async Task SeedPermissions(KMSDBContext db, string dirname, JsonSerializerSettings serializerSettings) {
            var permissionsJson = await File.ReadAllTextAsync(Path.Combine(dirname, "permissions.json"));
            var permissions = JsonConvert.DeserializeObject<List<Permissions>>(permissionsJson, serializerSettings);
            foreach (var item in permissions) {
                var existingPermission = await db.Permissions.SingleOrDefaultAsync(p => p.PermissionSlug == item.PermissionSlug);
                if (existingPermission == null) {
                    db.Permissions.Add(item);
                }
            }
            await db.SaveChangesAsync();
        }
        private static async Task SeedProjectPermissions(KMSDBContext db, string dirname, JsonSerializerSettings serializerSettings) {
            var permissionsJson = await File.ReadAllTextAsync(Path.Combine(dirname, "project_permissions.json"));
            var permissions = JsonConvert.DeserializeObject<List<ProjectPermissions>>(permissionsJson, serializerSettings);
            foreach (var item in permissions) {
                var existingPermission = await db.ProjectPermissions.SingleOrDefaultAsync(p => p.ProjectPermissionSlug == item.ProjectPermissionSlug);
                if (existingPermission == null) {
                    db.ProjectPermissions.Add(item);
                }
            }
            await db.SaveChangesAsync();
        }
        private static async Task SeedRoles(KMSDBContext db, string dirname, JsonSerializerSettings serializerSettings) {
            var rolesJson = await File.ReadAllTextAsync(Path.Combine(dirname, "roles.json"));
            var roles = JsonConvert.DeserializeObject<List<RoleSerializer>>(rolesJson, serializerSettings);

            foreach (var role in roles) {
                var dbRole = await db.Roles.SingleOrDefaultAsync(r => r.Name == role.Name && r.System == true);
                if (dbRole == null) {
                    dbRole = new Roles{ Name = role.Name, System = true };
                    db.Roles.Add(dbRole);
                    await db.SaveChangesAsync();
                }

                foreach (var permission in role.Permissions) {
                    var dbPermission = await db.Permissions.SingleOrDefaultAsync(p => p.PermissionSlug == permission);
                    var dbRolePermission = await db.RolePermissions.SingleOrDefaultAsync(rp => rp.RoleId == dbRole.RoleId && rp.PermissionSlug == permission);

                    if (dbPermission == null) {
                        throw new Exception("Permission for role not found");
                    }

                    if (dbRolePermission == null) {
                        db.RolePermissions.Add(new RolePermissions{ RoleId = dbRole.RoleId, PermissionSlug = permission });
                    }
                }
                await db.SaveChangesAsync();
            }
        }
        private static async Task SeedProjectRoles(KMSDBContext db, string dirname, JsonSerializerSettings serializerSettings) {
            var rolesJson = await File.ReadAllTextAsync(Path.Combine(dirname, "project_roles.json"));
            var roles = JsonConvert.DeserializeObject<List<RoleSerializer>>(rolesJson, serializerSettings);

            foreach (var role in roles) {
                var dbRole = await db.ProjectRoles.SingleOrDefaultAsync(r => r.Name == role.Name && r.System == true);
                if (dbRole == null) {
                    dbRole = new ProjectRoles{ Name = role.Name, System = true };
                    db.ProjectRoles.Add(dbRole);
                    await db.SaveChangesAsync();
                }

                foreach (var permission in role.Permissions) {
                    var dbPermission = await db.ProjectPermissions.SingleOrDefaultAsync(p => p.ProjectPermissionSlug == permission);
                    var dbRolePermission = await db.ProjectRolePermissions.SingleOrDefaultAsync(rp => rp.ProjectRoleId == dbRole.ProjectRoleId && rp.ProjectPermissionSlug == permission);

                    if (dbPermission == null) {
                        throw new Exception("Permission for role not found");
                    }

                    if (dbRolePermission == null) {
                        db.ProjectRolePermissions.Add(new ProjectRolePermissions{ ProjectRoleId = dbRole.ProjectRoleId, ProjectPermissionSlug = permission });
                    }
                }
                await db.SaveChangesAsync();
            }
        }
    }
}

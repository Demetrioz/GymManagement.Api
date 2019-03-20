using System;
using System.Runtime;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace GymManagement.Migrations.Migrations.Sprint001
{
    [Migration(001)]
    public class Sprint_001 : Migration
    {
        public override void Up()
        {

            // Create Tables
            Create.Table("GymManagementLog")
                .WithColumn("LogId").AsInt32().PrimaryKey().Identity()
                .WithColumn("Message").AsString(int.MaxValue).Nullable()
                .WithColumn("Data").AsString(int.MaxValue).Nullable()
                .WithColumn("Created").AsDateTime().Nullable()
                .WithColumn("Modified").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable();

            Create.Table("Contact")
                .WithColumn("ContactId").AsInt32().PrimaryKey().Identity()
                .WithColumn("StatusId").AsInt32().Nullable()
                .WithColumn("SourceId").AsInt32().Nullable()
                .WithColumn("InterestId").AsInt32().Nullable()
                .WithColumn("FirstName").AsString(255).Nullable()
                .WithColumn("LastName").AsString(255).Nullable()
                .WithColumn("Phone").AsString(255).Nullable()
                .WithColumn("Email").AsString(255).Nullable()
                .WithColumn("LastContact").AsDateTime().Nullable()
                .WithColumn("TimesContacted").AsInt32().Nullable()
                .WithColumn("Converted").AsBoolean().Nullable()
                .WithColumn("DateConverted").AsDateTime().Nullable()
                .WithColumn("LeadNotes").AsString(int.MaxValue).Nullable()
                .WithColumn("NextAppointment").AsDateTime().Nullable()
                .WithColumn("Created").AsDateTime().Nullable()
                .WithColumn("Modified").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable();

            Create.Table("Interest")
                .WithColumn("InterestId").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(255).Nullable()
                .WithColumn("Label").AsString(255).Nullable()
                .WithColumn("Created").AsDateTime().Nullable()
                .WithColumn("Modified").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable();

            Create.Table("Location")
                .WithColumn("LocationId").AsInt32().PrimaryKey().Identity()
                .WithColumn("ParentLocationId").AsInt32().Nullable()
                .WithColumn("Name").AsString(255).Nullable()
                .WithColumn("Label").AsString(255).Nullable()
                .WithColumn("TypeId").AsInt32().Nullable()
                .WithColumn("Address").AsString(255).Nullable()
                .WithColumn("City").AsString(255).Nullable()
                .WithColumn("State").AsString(255).Nullable()
                .WithColumn("ZipCode").AsString(255).Nullable()
                .WithColumn("Created").AsDateTime().Nullable()
                .WithColumn("Modified").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable();

            Create.Table("Promotion")
                .WithColumn("PromotionId").AsInt32().PrimaryKey().Identity()
                .WithColumn("ContactId").AsInt32().Nullable()
                .WithColumn("TypeId").AsInt32().Nullable()
                .WithColumn("StatusId").AsInt32().Nullable()
                .WithColumn("Created").AsDateTime().Nullable()
                .WithColumn("Modified").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable();

            Create.Table("Source")
                .WithColumn("SourceId").AsInt32().PrimaryKey().Identity()
                .WithColumn("TypeId").AsInt32().Nullable()
                .WithColumn("Name").AsString(255).Nullable()
                .WithColumn("Label").AsString(255).Nullable()
                .WithColumn("Description").AsString(255).Nullable()
                .WithColumn("Created").AsDateTime().Nullable()
                .WithColumn("Modified").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable();

            Create.Table("Status")
                .WithColumn("StatusId").AsInt32().PrimaryKey().Identity()
                .WithColumn("Type").AsString(255).Nullable()
                .WithColumn("Name").AsString(255).Nullable()
                .WithColumn("Label").AsString(255).Nullable()
                .WithColumn("Description").AsString(255).Nullable()
                .WithColumn("Created").AsDateTime().Nullable()
                .WithColumn("Modified").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable();

            Create.Table("Type")
                .WithColumn("TypeId").AsInt32().PrimaryKey().Identity()
                .WithColumn("Category").AsString(255).Nullable()
                .WithColumn("Name").AsString(255).Nullable()
                .WithColumn("Label").AsString(255).Nullable()
                .WithColumn("Description").AsString(int.MaxValue).Nullable()
                .WithColumn("Created").AsDateTime().Nullable()
                .WithColumn("Modified").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable();

            Create.Table("Class")
                .WithColumn("ClassId").AsInt32().PrimaryKey().Identity()
                .WithColumn("TypeId").AsInt32().Nullable()
                .WithColumn("LocationId").AsInt32().Nullable()
                .WithColumn("Start").AsDateTime().Nullable()
                .WithColumn("Stop").AsDateTime().Nullable()
                .WithColumn("Attendance").AsString(int.MaxValue).Nullable()
                .WithColumn("MaxAttendance").AsInt32().Nullable()
                .WithColumn("Created").AsDateTime().Nullable()
                .WithColumn("Modified").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable();

            Create.Table("ClassSchedule")
                .WithColumn("ClassScheduleId").AsInt32().PrimaryKey().Identity()
                .WithColumn("ClassTypeId").AsInt32().Nullable()
                .WithColumn("Day").AsString(255).Nullable()
                .WithColumn("BeginTime").AsDateTime().Nullable()
                .WithColumn("EndTime").AsDateTime().Nullable()
                .WithColumn("Duration").AsInt32().Nullable()
                .WithColumn("Created").AsDateTime().Nullable()
                .WithColumn("Modified").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable();

            Create.Table("User")
                .WithColumn("UserId").AsInt32().PrimaryKey().Identity()
                .WithColumn("RoleId").AsInt32().Nullable()
                .WithColumn("ContactId").AsInt32().Nullable()
                .WithColumn("FirstName").AsString(255).Nullable()
                .WithColumn("LastName").AsString(255).Nullable()
                .WithColumn("Phone").AsString(255).Nullable()
                .WithColumn("Email").AsString(255).Nullable()
                .WithColumn("UserName").AsString(int.MaxValue).Nullable()
                .WithColumn("Password").AsString(int.MaxValue).Nullable()
                .WithColumn("ForcePasswordReset").AsBoolean().Nullable()
                .WithColumn("LastLogon").AsDateTime().Nullable()
                .WithColumn("PasswordExpiration").AsDateTime().Nullable()
                .WithColumn("Created").AsDateTime().Nullable()
                .WithColumn("Modified").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable();

            Create.Table("Roles")
                .WithColumn("RoleId").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(255).Nullable()
                .WithColumn("Label").AsString(255).Nullable()
                .WithColumn("Description").AsString(255).Nullable();

            Create.Table("Claims")
                .WithColumn("ClaimId").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(255).Nullable()
                .WithColumn("C").AsBoolean().Nullable()
                .WithColumn("R").AsBoolean().Nullable()
                .WithColumn("U").AsBoolean().Nullable()
                .WithColumn("D").AsBoolean().Nullable();

            Create.Table("UserRoleClaimMap")
                .WithColumn("UserRoleClaimMapId").AsInt32().PrimaryKey().Identity()
                .WithColumn("RoleId").AsInt32().Nullable()
                .WithColumn("ClaimId").AsInt32().Nullable();

            //Create initial table entries
            Insert.IntoTable("Roles")
                .Row(new { Name = "client", Label = "Client", Description = "A client / member of the gym" })
                .Row(new { Name = "instructor", Label = "Instructor", Description = "An instructor at the gym" })
                .Row(new { Name = "receptionist", Label = "Receptionist", Description = "A receptionist at the gym" })
                .Row(new { Name = "owner", Label = "Owner", Description = "The owner of the gym" });

            Insert.IntoTable("Claims")
                .Row(new {Name = "Leads", C = 1, R = 1, U = 1, D = 1 })
                .Row(new {Name = "Leads.Edit", C = 1, R = 1, U = 1, D = 0 })
                .Row(new {Name = "Members", C = 1, R = 1, U = 1, D = 1 })
                .Row(new {Name = "Members.Edit", C = 1, R = 1, U = 1, D = 0 })
                .Row(new {Name = "Classes", C = 1, R = 1, U = 1, D = 1 })
                .Row(new {Name = "Classes.Read", C = 0, R = 1, U = 0, D = 0 })
                .Row(new {Name = "Classes.Edit", C = 1, R = 1, U = 1, D = 0 })
                .Row(new {Name = "Schedule", C = 1, R = 1, U = 1, D = 1 })
                .Row(new {Name = "Schedule.Read", C = 0, R = 1, U = 0, D = 0 })
                .Row(new {Name = "Schedule.Edit", C = 1, R = 1, U = 1, D = 0 })
                .Row(new {Name = "Dashboard", C = 1, R = 1, U = 1, D = 1 });

            Insert.IntoTable("UserRoleClaimMap")
                .Row(new {RoleId = 1, ClaimId = 6 })
                .Row(new {RoleId = 1, ClaimId = 10 })
                .Row(new {RoleId = 2, ClaimId = 2 })
                .Row(new {RoleId = 2, ClaimId = 4 })
                .Row(new {RoleId = 2, ClaimId = 6 })
                .Row(new {RoleId = 2, ClaimId = 10 })
                .Row(new {RoleId = 3, ClaimId = 2 })
                .Row(new {RoleId = 3, ClaimId = 4 })
                .Row(new {RoleId = 3, ClaimId = 7 })
                .Row(new {RoleId = 3, ClaimId = 11 })
                .Row(new {RoleId = 4, ClaimId = 1 })
                .Row(new {RoleId = 4, ClaimId = 3 })
                .Row(new {RoleId = 4, ClaimId = 5 })
                .Row(new {RoleId = 4, ClaimId = 8 })
                .Row(new {RoleId = 4, ClaimId = 11 });

            // Create Initial Interests

            // Create Initial Locations

            // Create Initial Sources

            // Create Initial Statuses

            // Create Initial Types

            // Create Initial Class Schedule

        }

        public override void Down()
        {
            // Delete Tables
            Delete.Table("GymManagementLog");
            Delete.Table("Contact");
            Delete.Table("Interest");
            Delete.Table("Location");
            Delete.Table("Promotion");
            Delete.Table("Source");
            Delete.Table("Status");
            Delete.Table("Type");
            Delete.Table("Class");
            Delete.Table("ClassSchedule");
            Delete.Table("User");
            Delete.Table("Roles");
            Delete.Table("Claims");
            Delete.Table("UserRoleClaimMap");
        }
    }
}

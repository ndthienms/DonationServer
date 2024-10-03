﻿// <auto-generated />
using System;
using DonationAppDemo.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DonationAppDemo.Migrations
{
    [DbContext(typeof(DonationDbContext))]
    [Migration("20241002144129__2_ChangePrimaryKeyToAccount_PhoneNum")]
    partial class _2_ChangePrimaryKeyToAccount_PhoneNum
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.33")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DonationAppDemo.Models.Account", b =>
                {
                    b.Property<string>("PhoneNum")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("disabled")
                        .HasColumnType("bit");

                    b.HasKey("PhoneNum");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("DonationAppDemo.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("DonationAppDemo.Models.Campaign", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OrganiserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("StatusCampaignId")
                        .HasColumnType("int");

                    b.Property<string>("Target")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("TargetAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("disabled")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("OrganiserId");

                    b.HasIndex("StatusCampaignId");

                    b.ToTable("Campaign");
                });

            modelBuilder.Entity("DonationAppDemo.Models.CommentPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("PostId");

                    b.ToTable("CommentPost");
                });

            modelBuilder.Entity("DonationAppDemo.Models.Donation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("CampaignId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DonationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DonorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CampaignId");

                    b.HasIndex("DonorId");

                    b.ToTable("Donation");
                });

            modelBuilder.Entity("DonationAppDemo.Models.Donor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AvaSrc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AvaSrcPublicId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Donor");
                });

            modelBuilder.Entity("DonationAppDemo.Models.ImageCampaign", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CampaignId")
                        .HasColumnType("int");

                    b.Property<string>("ImageSrc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageSrcPublicId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StatusCampaignId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CampaignId");

                    b.HasIndex("StatusCampaignId");

                    b.ToTable("ImageCampaign");
                });

            modelBuilder.Entity("DonationAppDemo.Models.ImageCommentPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CommentPostId")
                        .HasColumnType("int");

                    b.Property<string>("ImageSrc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageSrcPublicId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CommentPostId");

                    b.ToTable("ImageCommentPost");
                });

            modelBuilder.Entity("DonationAppDemo.Models.ImagePost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ImageSrc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageSrcPublicId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("ImagePost");
                });

            modelBuilder.Entity("DonationAppDemo.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("NotificationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NotificationText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NotificationTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("NotificationType")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("DonationAppDemo.Models.Organiser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AcceptedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("AcceptedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AvaSrc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AvaSrcPublicId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CertificationSrc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CertificationSrcPublicId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Organiser");
                });

            modelBuilder.Entity("DonationAppDemo.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("CampaignId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrganiserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CampaignId");

                    b.HasIndex("OrganiserId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("DonationAppDemo.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PostDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("disabled")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("DonationAppDemo.Models.RateCampaign", b =>
                {
                    b.Property<int>("CampaignId")
                        .HasColumnType("int");

                    b.Property<int>("DonorId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Rate")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CampaignId", "DonorId");

                    b.HasIndex("DonorId");

                    b.ToTable("RateCampaign");
                });

            modelBuilder.Entity("DonationAppDemo.Models.StatusCampaign", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StatusCampaign");
                });

            modelBuilder.Entity("DonationAppDemo.Models.Admin", b =>
                {
                    b.HasOne("DonationAppDemo.Models.Account", "Account")
                        .WithMany("Admins")
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("DonationAppDemo.Models.Campaign", b =>
                {
                    b.HasOne("DonationAppDemo.Models.Organiser", "Organiser")
                        .WithMany("Campaigns")
                        .HasForeignKey("OrganiserId");

                    b.HasOne("DonationAppDemo.Models.StatusCampaign", "StatusCampaign")
                        .WithMany("Campaigns")
                        .HasForeignKey("StatusCampaignId");

                    b.Navigation("Organiser");

                    b.Navigation("StatusCampaign");
                });

            modelBuilder.Entity("DonationAppDemo.Models.CommentPost", b =>
                {
                    b.HasOne("DonationAppDemo.Models.Account", "Account")
                        .WithMany("CommentPosts")
                        .HasForeignKey("AccountId");

                    b.HasOne("DonationAppDemo.Models.Post", "Post")
                        .WithMany("CommentPosts")
                        .HasForeignKey("PostId");

                    b.Navigation("Account");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("DonationAppDemo.Models.Donation", b =>
                {
                    b.HasOne("DonationAppDemo.Models.Campaign", "Campaign")
                        .WithMany("Donations")
                        .HasForeignKey("CampaignId");

                    b.HasOne("DonationAppDemo.Models.Donor", "Donor")
                        .WithMany("Donations")
                        .HasForeignKey("DonorId");

                    b.Navigation("Campaign");

                    b.Navigation("Donor");
                });

            modelBuilder.Entity("DonationAppDemo.Models.Donor", b =>
                {
                    b.HasOne("DonationAppDemo.Models.Account", "Account")
                        .WithMany("Donors")
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("DonationAppDemo.Models.ImageCampaign", b =>
                {
                    b.HasOne("DonationAppDemo.Models.Campaign", "Campaign")
                        .WithMany("ImageCampaigns")
                        .HasForeignKey("CampaignId");

                    b.HasOne("DonationAppDemo.Models.StatusCampaign", "StatusCampaign")
                        .WithMany("ImageCampaigns")
                        .HasForeignKey("StatusCampaignId");

                    b.Navigation("Campaign");

                    b.Navigation("StatusCampaign");
                });

            modelBuilder.Entity("DonationAppDemo.Models.ImageCommentPost", b =>
                {
                    b.HasOne("DonationAppDemo.Models.CommentPost", "CommentPost")
                        .WithMany("ImageCommentPosts")
                        .HasForeignKey("CommentPostId");

                    b.Navigation("CommentPost");
                });

            modelBuilder.Entity("DonationAppDemo.Models.ImagePost", b =>
                {
                    b.HasOne("DonationAppDemo.Models.Post", "Post")
                        .WithMany("ImagePosts")
                        .HasForeignKey("PostId");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("DonationAppDemo.Models.Notification", b =>
                {
                    b.HasOne("DonationAppDemo.Models.Account", "Account")
                        .WithMany("Notifications")
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("DonationAppDemo.Models.Organiser", b =>
                {
                    b.HasOne("DonationAppDemo.Models.Account", "Account")
                        .WithMany("Organisers")
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("DonationAppDemo.Models.Payment", b =>
                {
                    b.HasOne("DonationAppDemo.Models.Campaign", "Campaign")
                        .WithMany("Payments")
                        .HasForeignKey("CampaignId");

                    b.HasOne("DonationAppDemo.Models.Organiser", "Organiser")
                        .WithMany("Payments")
                        .HasForeignKey("OrganiserId");

                    b.Navigation("Campaign");

                    b.Navigation("Organiser");
                });

            modelBuilder.Entity("DonationAppDemo.Models.Post", b =>
                {
                    b.HasOne("DonationAppDemo.Models.Account", "Account")
                        .WithMany("Posts")
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("DonationAppDemo.Models.RateCampaign", b =>
                {
                    b.HasOne("DonationAppDemo.Models.Campaign", "Campaign")
                        .WithMany("RateCampaigns")
                        .HasForeignKey("CampaignId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DonationAppDemo.Models.Donor", "Donor")
                        .WithMany("RateCampaigns")
                        .HasForeignKey("DonorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Campaign");

                    b.Navigation("Donor");
                });

            modelBuilder.Entity("DonationAppDemo.Models.Account", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("CommentPosts");

                    b.Navigation("Donors");

                    b.Navigation("Notifications");

                    b.Navigation("Organisers");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("DonationAppDemo.Models.Campaign", b =>
                {
                    b.Navigation("Donations");

                    b.Navigation("ImageCampaigns");

                    b.Navigation("Payments");

                    b.Navigation("RateCampaigns");
                });

            modelBuilder.Entity("DonationAppDemo.Models.CommentPost", b =>
                {
                    b.Navigation("ImageCommentPosts");
                });

            modelBuilder.Entity("DonationAppDemo.Models.Donor", b =>
                {
                    b.Navigation("Donations");

                    b.Navigation("RateCampaigns");
                });

            modelBuilder.Entity("DonationAppDemo.Models.Organiser", b =>
                {
                    b.Navigation("Campaigns");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("DonationAppDemo.Models.Post", b =>
                {
                    b.Navigation("CommentPosts");

                    b.Navigation("ImagePosts");
                });

            modelBuilder.Entity("DonationAppDemo.Models.StatusCampaign", b =>
                {
                    b.Navigation("Campaigns");

                    b.Navigation("ImageCampaigns");
                });
#pragma warning restore 612, 618
        }
    }
}

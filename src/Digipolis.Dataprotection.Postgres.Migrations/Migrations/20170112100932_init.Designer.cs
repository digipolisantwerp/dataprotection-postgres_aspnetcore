using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Digipolis.DataProtection.Postgres;

namespace Digipolis.Dataprotection.Postgres.Migrations.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20170112100932_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.2");

            modelBuilder.Entity("Digipolis.DataProtection.Postgres.KeyValuesCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AppId");

                    b.Property<Guid>("InstanceId");

                    b.Property<DateTime>("Timestamp");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("KeyCollections");
                });
        }
    }
}

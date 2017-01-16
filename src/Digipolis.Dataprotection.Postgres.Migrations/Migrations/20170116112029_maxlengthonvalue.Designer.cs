using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Digipolis.DataProtection.Postgres;

namespace Digipolis.Dataprotection.Postgres.Migrations.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20170116112029_maxlengthonvalue")]
    partial class maxlengthonvalue
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

                    b.Property<string>("Value")
                        .HasMaxLength(2000);

                    b.HasKey("Id");

                    b.ToTable("KeyCollections");
                });
        }
    }
}

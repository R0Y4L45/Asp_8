﻿// <auto-generated />
using Asp_8.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace App.Entities.Migrations;

[DbContext(typeof(BookStoreDbContext))]
partial class BookStoreDbContextModelSnapshot : ModelSnapshot
{
	protected override void BuildModel(ModelBuilder modelBuilder)
	{
#pragma warning disable 612, 618
		modelBuilder
			.HasAnnotation("ProductVersion", "6.0.21")
			.HasAnnotation("Relational:MaxIdentifierLength", 128);

		SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

		modelBuilder.Entity("Asp_8.Entites.Author", b =>
			{
				b.Property<int>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("int");

				SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

				b.Property<string>("Name")
					.IsRequired()
					.HasColumnType("nvarchar(max)");

				b.Property<string>("Surname")
					.IsRequired()
					.HasColumnType("nvarchar(max)");

				b.HasKey("Id");

				b.ToTable("Authors");
			});

		modelBuilder.Entity("Asp_8.Entites.Books", b =>
			{
				b.Property<int>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("int");

				SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

				b.Property<int>("AuthorId")
					.HasColumnType("int");

				b.Property<int>("CategoryId")
					.HasColumnType("int");

				b.Property<int>("Count")
					.HasColumnType("int");

				b.Property<string>("Description")
					.HasColumnType("nvarchar(max)");

				b.Property<string>("Name")
					.IsRequired()
					.HasColumnType("nvarchar(max)");

				b.Property<int>("PressId")
					.HasColumnType("int");

				b.Property<decimal>("Price")
					.HasColumnType("decimal(18,2)");

				b.Property<int>("ThemeId")
					.HasColumnType("int");

				b.HasKey("Id");

				b.HasIndex("AuthorId");

				b.HasIndex("CategoryId");

				b.HasIndex("PressId");

				b.HasIndex("ThemeId");

				b.ToTable("Books");
			});

		modelBuilder.Entity("Asp_8.Entites.Category", b =>
			{
				b.Property<int>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("int");

				SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

				b.Property<string>("Name")
					.IsRequired()
					.HasColumnType("nvarchar(max)");

				b.HasKey("Id");

				b.ToTable("Categories");
			});

		modelBuilder.Entity("Asp_8.Entites.Press", b =>
			{
				b.Property<int>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("int");

				SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

				b.Property<string>("Name")
					.IsRequired()
					.HasColumnType("nvarchar(max)");

				b.HasKey("Id");

				b.ToTable("Presses");
			});

		modelBuilder.Entity("Asp_8.Entites.Theme", b =>
			{
				b.Property<int>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("int");

				SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

				b.Property<string>("Name")
					.IsRequired()
					.HasColumnType("nvarchar(max)");

				b.HasKey("Id");

				b.ToTable("Themes");
			});

		modelBuilder.Entity("Asp_8.Entites.Books", b =>
			{
				b.HasOne("Asp_8.Entites.Author", "Author")
					.WithMany("Book")
					.HasForeignKey("AuthorId")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();

				b.HasOne("Asp_8.Entites.Category", "Category")
					.WithMany("Books")
					.HasForeignKey("CategoryId")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();

				b.HasOne("Asp_8.Entites.Press", "Press")
					.WithMany("Books")
					.HasForeignKey("PressId")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();

				b.HasOne("Asp_8.Entites.Theme", "Theme")
					.WithMany("Books")
					.HasForeignKey("ThemeId")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();

				b.Navigation("Author");

				b.Navigation("Category");

				b.Navigation("Press");

				b.Navigation("Theme");
			});

		modelBuilder.Entity("Asp_8.Entites.Author", b =>
			{
				b.Navigation("Book");
			});

		modelBuilder.Entity("Asp_8.Entites.Category", b =>
			{
				b.Navigation("Books");
			});

		modelBuilder.Entity("Asp_8.Entites.Press", b =>
			{
				b.Navigation("Books");
			});

		modelBuilder.Entity("Asp_8.Entites.Theme", b =>
			{
				b.Navigation("Books");
			});
#pragma warning restore 612, 618
	}
}

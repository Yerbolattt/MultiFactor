﻿namespace MultiFactor.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using MultiFactor.Data.Models;
    using MultiFactor.Data.Validations;

    public class PasswordConfiguration : IEntityTypeConfiguration<Password>
    {
        public void Configure(EntityTypeBuilder<Password> passsword)
        {
            passsword.HasOne(p => p.Quiz)
                .WithOne(q => q.Password)
                .HasForeignKey<Password>(p => p.QuizId);

            passsword.Property(q => q.Content)
               .HasMaxLength(DataValidation.Password.PasswordMaxLength)
               .IsRequired();

            passsword.HasIndex(x => x.Content)
                .IsUnique();
        }
    }
}

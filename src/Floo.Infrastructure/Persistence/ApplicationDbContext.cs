using Floo.Core.Entities.Cms.Contents;
using Floo.Core.Entities.Cms.Channels;
using Floo.Core.Entities.Cms.Comments;
using Floo.Core.Entities.Cms.SpecialColumns;
using Floo.Core.Entities.Cms.Tags;
using Floo.Core.Entities.Cms.Answers;
using Floo.Core.Entities.Cms.Articles;
using Floo.Core.Entities.Cms.Questions;
using Floo.Core.Entities.Identity;
using Floo.Core.Entities.Identity.Users;
using Floo.Core.Shared;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Floo.Core;

namespace Floo.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, long, Core.Entities.Identity.UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IPersistedGrantDbContext, IDbContext
    {
        private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;
        private readonly IIdentityContext _identityContext;

        public ApplicationDbContext(
             IOptions<OperationalStoreOptions> operationalStoreOptions,
            DbContextOptions<ApplicationDbContext> options,
            IIdentityContext identityContext) : base(options)
        {
            _operationalStoreOptions = operationalStoreOptions;
            _identityContext = identityContext;
        }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{PersistedGrant}"/>.
        /// </summary>
        public DbSet<PersistedGrant> PersistedGrants { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{DeviceFlowCodes}"/>.
        /// </summary>
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

        Task<int> IPersistedGrantDbContext.SaveChangesAsync() => base.SaveChangesAsync();

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            SetEntity<Content>(builder).ToTable("Contents") ;
            SetEntity<Channel>(builder).ToTable("Channels");
            SetEntity<Comment>(builder).ToTable("Comments");
            SetEntity<Tag>(builder).ToTable("Tags");
            SetEntity<Column>(builder).ToTable("Columns");
            SetEntity<Answer>(builder).ToTable("Answers");
            SetEntity<Article>(builder).ToTable("Articles");
            SetEntity<Question>(builder).ToTable("Questions");

            base.OnModelCreating(builder);


            builder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.ChangeTracker.DetectChanges();
            this.UpdateUpdatedProperty();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            this.ChangeTracker.DetectChanges();
            this.UpdateUpdatedProperty();
            return base.SaveChanges();
        }

        private EntityTypeBuilder<TEntity> SetEntity<TEntity>(ModelBuilder builder)
            where TEntity : class, IEntity<long>
        {
            var entityBuilder = builder.Entity<TEntity>();
            entityBuilder.HasKey(_ => _.Id);
            entityBuilder.HasQueryFilter(_ => !_.Deleted);

            return entityBuilder;
        }

        private void UpdateUpdatedProperty()
        {
            var modifiedSourceInfo = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            var userId = this._identityContext.UserId ?? 0;

            foreach (var entry in modifiedSourceInfo)
            {
                UpdateProperty(entry, nameof(IEntity.UpdatedBy), userId);
                UpdateProperty(entry, nameof(IEntity.UpdatedAtUtc), DateTimeOffset.Now.UtcDateTime);
            }
        }

        private void UpdateProperty<TValue>(EntityEntry entityEntry, string propertyName, TValue value)
        {
            if (entityEntry.Metadata.FindProperty(propertyName) != null)
            {
                entityEntry.Property(propertyName).CurrentValue = value;
                entityEntry.Property(propertyName).IsModified = true;
            }
        }
    }
}
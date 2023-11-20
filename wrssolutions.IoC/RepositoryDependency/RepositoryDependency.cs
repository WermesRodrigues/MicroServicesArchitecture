
using wrssolutions.Data.Entity;
using wrssolutions.Data.EntityContext;
using wrssolutions.Data.Repository.Dapper;
using wrssolutions.Data.Repository.Dapper.Interface;
using wrssolutions.Domain.MongoEntities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace wrssolutions.IoC.Dependency
{
    public static class RepositoryDependency
    {

        /// <summary>
        /// AddRepositories (Databases)
        /// AddRepositories (Dapper)
        /// </summary>
        /// <param name="conn">Connection Database</param>
        public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            string conn = configuration["KeyVault:DefaultConnection"]!;

            //Database (Entity)
            #region Database (Entity)
            services.AddDbContext<EFContext>(opt => opt.UseSqlServer(conn, providerOptions => providerOptions.EnableRetryOnFailure()));
            #endregion


            //Database (Dapper)
            #region Database (Dapper)
            services.AddSingleton<IDppRepository, DppRepository>(_ => new DppRepository(conn));
            #endregion

            //NOSQL MongoDB
            #region NOSQL MongoDB
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
            var mongoDbSettings = configuration.GetSection(nameof(MongoDBSettings)).Get<MongoDBSettings>();

            services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                return new MongoClient(mongoDbSettings!.ConnectionString);
            });

            var mongoSettings = configuration.GetSection(nameof(MongoDBSettings));
            var settings = configuration.GetSection(nameof(MongoDBSettings)).Get<MongoDBSettings>();

            services.AddSingleton<MongoDBSettings>(settings!);

            services.AddTransient<IMongoContext, MongoContext>(serviceProvider =>
            {
                return new MongoContext(settings!.ConnectionString, settings.DatabaseName);
            });
            #endregion
        }

    }
}

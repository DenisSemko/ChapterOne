using BLL.Services.Abstract;
using BLL.Services.Concrete;
using DAL.Repository.Abstract;
using DAL.Repository.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<ICombinationRepository, CombinationRepository>();
            services.AddScoped<IBookTypeRepository, BookTypeRepository>();
            services.AddScoped<IRateRepository, RateRepository>();
            services.AddScoped<IBookCollectionRepository, BookCollectionRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICollectionRepository, CollectionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IUserAuthService, UserAuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<IStatisticService, StatisticService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IBooksSearchingService, BooksSearchingService>();
            services.AddScoped<ICombinationService, CombinationService>();
            services.AddScoped<IFileContentTypeService, FileContentTypeService>();
            services.AddScoped<IBookTypeService, BookTypeService>();
            services.AddScoped<IRateService, RateService>();
            services.AddScoped<IBookCollectionService, BookCollectionService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICollectionService, CollectionService>();

            return services;
        }
    }
}

using eTranslate.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace eTranslate
{
    public static class eTranslate
    {
        private static IeTranslate? FInstance;

        /// <summary>
        /// An extension method to be used in Blazor Apps to add eTranslate service
        /// </summary>
        /// <param name="Services">The IServiceCollection</param>
        /// <param name="TranslationFileFullPath">The full path to the translation file</param>
        /// <param name="DefaultLanguage">The language to initialize the library. Default value is en-US</param>
        /// <exception cref="Exception">Occurs an exception if you pass emptystring to TranslationFileFullPath or if there's not an HttpClient service available</exception>
        public static void AddeTranslateSingletonService(this IServiceCollection Services, string TranslationFileFullPath, string DefaultLanguage = "en-US")
        {
            if (string.IsNullOrWhiteSpace(TranslationFileFullPath))
                throw new Exception("It's impossible to initialize eTranslate because it was not provided a valid Translation File Full path.");

            var serviceDescriptor = Services.FirstOrDefault(x => x.ServiceType == typeof(HttpClient));

            if (serviceDescriptor != null)
            {
                if (serviceDescriptor.Lifetime == ServiceLifetime.Scoped)
                {
                    Services.AddScoped<IeTranslate>(provider =>
                    {
                        return new eTranslate4CSharp(TranslationFileFullPath, DefaultLanguage, provider.GetService<HttpClient>());
                    });
                }
                else if (serviceDescriptor.Lifetime == ServiceLifetime.Singleton)
                {
                    Services.AddSingleton<IeTranslate>(provider =>
                    {
                        return new eTranslate4CSharp(TranslationFileFullPath, DefaultLanguage, provider.GetService<HttpClient>());
                    });
                }
            }
            else
                throw new Exception("Service HttpClient was not found. Impossible to create service eTranslate without it.");
        }

        /// <summary>
        /// Variable to access the interface methods of the eTranslate library
        /// </summary>
        public static IeTranslate? ETranslate
        {
            get
            {
                return FInstance;
            }
        }

        /// <summary>
        /// Method to initialize the eTranslate singleton service on the app
        /// </summary>
        /// <param name="TranslationFileFullPath">The full path to the translation file</param>
        /// <param name="DefaultLanguage">The language to initialize the library. Default value is en-US</param>
        /// <returns></returns>
        /// <exception cref="Exception">Occurs an exception if you pass emptystring to TranslationFileFullPath</exception>
        public static IeTranslate Init_eTranslate(string TranslationFileFullPath, string DefaultLanguage = "en-US")
        {
            if (string.IsNullOrWhiteSpace(TranslationFileFullPath))
                throw new Exception("It's impossible to initialize eTranslate because it was not provided a valid Translation File Full path.");

            if (FInstance == null)
                FInstance = new eTranslate4CSharp(TranslationFileFullPath, DefaultLanguage);
            return FInstance;
        }
    }
}

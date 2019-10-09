namespace Libraary.Common
{
    public static class DataConstants
    {
        public static class Address
        {
            public const string CountryName = "Country";

            public const string TownName = "Town";

            public const string StreetName = "Street";

            public const string ZipName = "Zip";

            public const int CountryMaxLength = 100;

            public const int CountryMinimumLength = 2;

            public const int TownMaxLength = 100;

            public const int TownMinimumLength = 2;

            public const int StreetMaxLength = 100;

            public const int StreetMinimumLength = 6;
        }

        public static class Author
        {
            public const string DisplayFirstName = "First name";

            public const string DisplayLastName = "Last name";

            public const string NationalityName = "Nationality";

            public const int FirstNameMaxLength = 25;

            public const int FirstNameMinimumLength = 2;

            public const int LastNameMaxLength = 25;

            public const int LastNameMinimumLength = 2;

            public const int NationalityMaxLength = 25;

            public const int NationalityMinimumLength = 2;
        }

        public static class Book
        {
            public const string DisplayArticle = "Article";

            public const string DisplaySummary = "Summary";

            public const int ArticleMaxLength = 60;

            public const int ArticleMinimumLength = 5;

            public const int SummaryMaxLength = 5000;

            public const int SummaryMinimumLength = 100;
        }

        public static class Library
        {
            public const string DisplayLibrary = "Library";

            public const string DisplayAddress = "Address";

            public const string DisplayPhone = "Phone";

            public const string DisplayEmail = "Email";

            public const int LibraryNameMaxLength = 100;

            public const int LibraryNameMinimumLength = 2;
        }

        public static class Publisher
        {
            public const string DisplayPublisher = "Publisher";

            public const string DisplayUrlAddress = "Url address";

            public const int PublisherNameMaxLength = 50;

            public const int PublisherNameMinimumLength = 5;
        }

        public static class Librarian
        {
            public const string DisplayEmail = "Email";
        }
    }
}

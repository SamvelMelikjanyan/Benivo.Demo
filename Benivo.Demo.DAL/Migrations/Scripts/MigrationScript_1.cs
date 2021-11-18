using Benivo.Demo.DAL.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Benivo.Demo.DAL.Migrations.Scripts
{
    internal class MigrationScript_1 : IMigrationScript
    {
        public void OnDown(MigrationBuilder migrationBuilder)
        {}

        public void OnUp(MigrationBuilder migrationBuilder)
        {
            InsertEmploymentTypes(migrationBuilder);
            InsertJobCategories(migrationBuilder);
            InsertCountries(migrationBuilder);
            InsertCities(migrationBuilder);
            InsertCompanies(migrationBuilder);
            InsertJobAnnouncements(migrationBuilder);
        }

        private void InsertEmploymentTypes(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"SET IDENTITY_INSERT EmployementTypes ON

                                   INSERT INTO EmployementTypes(Id, [Name])
                                   VALUES (1, 'Full Time'), 
                                          (2, 'Part Time'), 
                                          (3, 'Contractor'),
                                          (4, 'Intern'), 
                                          (5, 'Seasonal / Temp')

                                   SET IDENTITY_INSERT EmployementTypes OFF");
        }

        private void InsertJobCategories(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"SET IDENTITY_INSERT JobCategories ON

                                   INSERT INTO JobCategories(Id, [Name], ParentId)
                                   VALUES 
                                   (1, 'IT & Networking', null), 
                                   (2, 'Database Administration', 1),  
                                   (3, 'DevOps Engineering', 1),

                                   (4, 'Sales & Marketing', null),
                                   (5, 'Business Development', 4),
                                   (6, 'Campaign Management', 4)

                                  SET IDENTITY_INSERT JobCategories OFF");
        }

        private void InsertCountries(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"SET IDENTITY_INSERT Countries ON

                                   INSERT INTO Countries(Id, [Name])
                                   VALUES (1, 'Armenia'), (2, 'USA'), (3, 'Germany')

                                   SET IDENTITY_INSERT Countries OFF");
        }

        private void InsertCities(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"SET IDENTITY_INSERT Cities ON

                                   INSERT INTO Cities(Id, [Name], CountryId)
                                   VALUES (1, 'Yerevan', 1), (2, 'Gyumri', 1), 
                                          (3, 'San Francisco', 2), (4, 'Delawer', 2),
                                          (5, 'Berlin', 3), (6, 'Munich', 3)

                                   SET IDENTITY_INSERT Cities OFF");
        }

        private void InsertCompanies(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"SET IDENTITY_INSERT Companies ON

                                   INSERT INTO Companies(Id, [Name], [Description])
                                   VALUES (1, 'Benivo', 'Benivo is a HR-tech company assisting talent managers and recruiters in the process of relocation and preboarding candidates and new hires in a new city.'), 
                                          (2, 'Dell', 'Dell is an American multinational computer technology company that develops, sells, repairs, and supports computers and related products and services, and is owned by its parent company of Dell Technologies.'), 
                                          (3, 'Microsoft', 'Microsoft Corporation is an American multinational technology corporation which produces computer software, consumer electronics, personal computers, and related services.')

                                   SET IDENTITY_INSERT Companies OFF");
        }

        private void InsertJobAnnouncements(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO JobAnnouncements(Title, [Description], ExpirationDate, EmploymentTypeId, CityId, CompanyId, JobCategoryId)
                                   VALUES 
                                   ('Senior Database Administrator', 'The Senior Database Administrator (DBA) will work with IT teams: Reporting, Pricing and Analytics, and IT Infrastructure to develop an understanding of data and information needs, identify solutions and improve the quality of service while providing support through the logical and physical design of databases.', '2021-12-17', 1, 1, 1, 2),
                                   ('Senior DevOps Engineer', 'Also known as senior developers, senior DevOps engineers oversee teams of junior software developers. Their duties include advising on the alignment of operations with information systems, writing code and scripts, and ensuring the seamless deployment of software.', '2022-01-10', 1, 3, 2, 3),
                                   ('Middle Marketing Specialist', 'Marketing specialists help develop, execute, and monitor marketing programs across a variety of channels. Their work includes researching the market, analyzing trends to help define the organization''s marketing strategy, and providing advice as to how to best reach the target market. Depending on the role, marketing specialists may also help with the coordination of events such as trade shows or conferences.', null, 2, 5, 3, 4)");
        }
    }
}

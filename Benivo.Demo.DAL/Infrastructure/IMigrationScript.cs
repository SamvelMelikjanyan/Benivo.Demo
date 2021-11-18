using Microsoft.EntityFrameworkCore.Migrations;

namespace Benivo.Demo.DAL.Infrastructure
{
    internal interface IMigrationScript
    {
        void OnUp(MigrationBuilder migrationBuilder);

        void OnDown(MigrationBuilder migrationBuilder);
    }
}

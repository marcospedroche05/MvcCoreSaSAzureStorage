using Azure.Data.Tables;
using Azure.Data.Tables.Sas;

namespace ApiTokensAzureStorage.Services
{
    public class ServiceSaSToken
    {
        private TableClient tableAlumnos;
        public ServiceSaSToken(IConfiguration configuration)
        {
            string azureKeys =
                configuration.GetValue<string>("AzureKeys:StorageAccount");
            TableServiceClient tableService =
                new TableServiceClient(azureKeys);
            this.tableAlumnos = tableService.GetTableClient("alumnos");
        }

        public string GenerateToken(string curso)
        {
            //NECESITAMOS LOS PERMISOS DE ACCESO
            TableSasPermissions permisos = TableSasPermissions.Read;
            //EL ACCESO AL TOKEN ESTA DELIMITADO POR UN TIEMPO DETERMINADO
            TableSasBuilder builder = this.tableAlumnos.GetSasBuilder
                (permisos, DateTime.UtcNow.AddMinutes(30));
            //EL ACCESO A LOS DATOS ES MEDIANTE ROWKEY O PARTITION KEY.
            //SON STRING Y VAN DE FORMA ALFABETICA
            builder.PartitionKeyStart = curso;
            builder.PartitionKeyEnd = curso;
            //YA TENEMOS EL TOKEN QUE ES UN ACCESO MEDIANTE URI
            Uri uriToken = this.tableAlumnos.GenerateSasUri(builder);
            string token = uriToken.AbsoluteUri;
            return token;
        }
    }
}

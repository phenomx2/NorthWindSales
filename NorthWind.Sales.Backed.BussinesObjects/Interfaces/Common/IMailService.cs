namespace NorthWind.Sales.Backed.BusinessObjects.Interfaces.Common;

public interface IMailService
{
    ValueTask SendEmailToAdministrator(string subject, string value);
}

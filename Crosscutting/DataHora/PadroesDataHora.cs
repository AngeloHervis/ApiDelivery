namespace Crosscutting.DataHora;

public static class PadroesDataHora
{
    public static DateTime Amanha => DateTime.UtcNow.Date.AddDays(1);
    public static DateTime Hoje => DateTime.UtcNow.Date;
    public static DateTime Ontem => DateTime.UtcNow.Date.AddDays(-1);
    public static DateTime Agora => DateTime.UtcNow;
}
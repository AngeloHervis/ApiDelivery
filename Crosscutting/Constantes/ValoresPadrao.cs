namespace Crosscutting.Constantes;

public static class ValoresPadrao
{
    public static readonly TimeSpan RegexTimeout = TimeSpan.FromMilliseconds(100);
    public static readonly TimeSpan QuartzStartDelay = TimeSpan.FromSeconds(10);
    
    public const long TamanhoMaximoRequisicao = 73400320L;
    public const long TamanhoMaximoLog = 20971520L;
    
    public const string UsuarioDoSistema = "SYSTEM";
    public const string UsuarioAnonimo = "Anonimo";
}
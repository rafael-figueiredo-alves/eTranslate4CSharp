namespace Sample
{
    public class Teste
    {
        public static TValor Valor => new TValor();
    }

    public class TValor
    {
        public TOutroValor OutroValor => new TOutroValor();
        public string Texto = "Oi";
    }

    public class TOutroValor
    {
        public TEsteValor EsteValor => new TEsteValor();
    }

    public class TEsteValor
    {
        public string Texto = "Teste.Valor.OutroValor.EsteValor";
    }
}

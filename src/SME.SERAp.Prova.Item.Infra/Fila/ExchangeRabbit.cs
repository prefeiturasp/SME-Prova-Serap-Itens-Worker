namespace SME.SERAp.Prova.Item.Infra.Fila
{
    public static class ExchangeRabbit
    {
        public static string Log => "EnterpriseApplicationLog";
        public static string SerapEstudanteItem => "serap.estudante.item.workers";
        public static string SerapEstudanteItemDeadLetter => "serap.estudante.item.workers.deadletter";
        public static int SerapDeadLetterTtl => 10 * 60 * 1000; /*10 Min * 60 Seg * 1000 milisegundos = 10 minutos em milisegundos*/
    }
}

using System.ComponentModel;

namespace ByteInsights.Enums
{
    public enum ModerationType 
    {

        [Description("Political propaganda")]
        Political,

        [Description("Offensive language")]
        Language,

        [Description("Drug references")]
        Drugs,

        [Description("Threatening speech")]
        Threatening,

        [Description("Sexual Content")]
        Sexual,

        [Description("Hate Speech")]
        HateSpeech,

        [Description("Targeted shaming")]
        Shaming

    }
}

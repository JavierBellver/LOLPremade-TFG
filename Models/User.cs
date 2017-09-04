using lolpremade.Utils;
using lolpremade.Utils.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lolpremade.Models
{
    public sealed class Role //Safe Enum Pattern
    {
        public static readonly Role ADC = new Role("ADC");
        public static readonly Role Support = new Role("Support");
        public static readonly Role Jungle = new Role("Jungle");
        public static readonly Role MID = new Role("Mid");
        public static readonly Role TOP = new Role("Top");
        public static readonly List<Role> ListOfRoles = new List<Role>
        {
            ADC,
            Support,
            Jungle,
            MID,
            TOP
        };

        public Role(string val)
        {
            Value = val;
        }

        public string Value { get; private set; }
    }

    public sealed class Rank
    {
        public static readonly Rank Unranked = new Rank("UNRANKED");
        public static readonly Rank BronzeV = new Rank("BRONZE V");
        public static readonly Rank BronzeIV = new Rank("BRONZE IV");
        public static readonly Rank BronzeIII = new Rank("BRONZE III");
        public static readonly Rank BronzeII = new Rank("BRONZE II");
        public static readonly Rank BronzeI = new Rank("BRONZE I");
        public static readonly Rank SilverV = new Rank("SILVER V");
        public static readonly Rank SilverIV = new Rank("SILVER IV");
        public static readonly Rank SilverIII = new Rank("SILVER III");
        public static readonly Rank SilverII = new Rank("SILVER II");
        public static readonly Rank SilverI = new Rank("SILVER I");
        public static readonly Rank GoldV = new Rank("GOLD V");
        public static readonly Rank GoldIV = new Rank("GOLD IV");
        public static readonly Rank GoldIII = new Rank("GOLD III");
        public static readonly Rank GoldII = new Rank("GOLD II");
        public static readonly Rank GoldI = new Rank("GOLD I");
        public static readonly Rank PlatinumV = new Rank("PLATINUM V");
        public static readonly Rank PlatinumIV = new Rank("PLATINUM IV");
        public static readonly Rank PlatinumIII = new Rank("PLATINUM III");
        public static readonly Rank PlatinumII = new Rank("PLATINUM II");
        public static readonly Rank PlatinumI = new Rank("PLATINUM I");
        public static readonly Rank DiamondV = new Rank("DIAMOND V");
        public static readonly Rank DiamondIV = new Rank("DIAMOND IV");
        public static readonly Rank DiamondIII = new Rank("DIAMOND III");
        public static readonly Rank DiamondII = new Rank("DIAMOND II");
        public static readonly Rank DiamondI = new Rank("DIAMOND I");
        public static readonly Rank Master = new Rank("MASTER");
        public static readonly Rank Challenger = new Rank("CHALLENGER");
        public static readonly List<Rank> ListOfRanks = new List<Rank>
        {
            Unranked,BronzeV,BronzeIV,BronzeIII,BronzeII,BronzeI,
            SilverV,SilverIV,SilverIII,SilverII,SilverI,
            GoldV,GoldIV,GoldIII,GoldII,GoldI,
            PlatinumV,PlatinumIV,PlatinumIII,PlatinumII,PlatinumI,
            DiamondV,DiamondIV,DiamondIII,DiamondII,DiamondI,
            Master,
            Challenger
        };

        public Rank(string val)
        {
            Value = val;
        }

        public string Value { get; private set; }
    }

    public sealed class PlayRegion
    {
        public static readonly PlayRegion BR = new PlayRegion("Brazil");
        public static readonly PlayRegion EUNE = new PlayRegion("Europe Nordic & East");
        public static readonly PlayRegion EUWEST = new PlayRegion("Europe West");
        public static readonly PlayRegion LAN = new PlayRegion("Latin America North");
        public static readonly PlayRegion LAS = new PlayRegion("Latin America South");
        public static readonly PlayRegion OCE = new PlayRegion("Oceania");
        public static readonly PlayRegion RUS = new PlayRegion("Russia");
        public static readonly PlayRegion TR = new PlayRegion("Turkey");
        public static readonly PlayRegion JP = new PlayRegion("Japan");
        public static readonly PlayRegion SEA = new PlayRegion("South East Asia");
        public static readonly PlayRegion KR = new PlayRegion("Republic of Korea");
        public static readonly PlayRegion NA = new PlayRegion("North America");
        public static readonly List<PlayRegion> ListOfPlayRegions = new List<PlayRegion>
        {
            BR,EUNE,EUWEST,LAN,LAS,OCE,RUS,TR,JP,SEA,KR,NA
        };

        public PlayRegion(string val)
        {
            Value = val;
        }

        public string Value { get; private set; }
    }

    public sealed class Country
    {
        public static Country Afghanistan = new Country("Afghanistan");
        public static Country AlandIslands = new Country("Åland Islands");
        public static Country Albania = new Country("Albania");
        public static Country Algeria = new Country("Algeria");
        public static Country AmericanSamoa = new Country("American Samoa");
        public static Country Andorra = new Country("Andorra");
        public static Country Angola = new Country("Angola");
        public static Country Anguilla = new Country("Anguilla");
        public static Country Antarctica = new Country("Antarctica");
        public static Country AntiguaAndBarbuda = new Country("Antigua and Barbuda");
        public static Country Argentina = new Country("Argentina");
        public static Country Armenia = new Country("Armenia");
        public static Country Aruba = new Country("Aruba");
        public static Country Australia = new Country("Australia");
        public static Country Austria = new Country("Austria");
        public static Country Azerbaijan = new Country("Azerbaijan");
        public static Country Bahamas = new Country("Bahamas");
        public static Country Bahrain = new Country("Bahrain");
        public static Country Bangladesh = new Country("Bangladesh");
        public static Country Barbados = new Country("Barbados");
        public static Country Belarus = new Country("Belarus");
        public static Country Belgium = new Country("Belgium");
        public static Country Belize = new Country("Belize");
        public static Country Benin = new Country("Benin");
        public static Country Bermuda = new Country("Bermuda");
        public static Country Bhutan = new Country("Bhutan");
        public static Country Bolivia = new Country("Bolivia");
        public static Country BosniaHerzegovina = new Country("Bosnia and Herzegovina");
        public static Country Botswana = new Country("Botswana");
        public static Country BouvetIsland = new Country("Bouvet Island");
        public static Country Brazil = new Country("Brazil");
        public static Country BruneiDarussalam = new Country("Brunei Darussalam");
        public static Country Bulgaria = new Country("Bulgaria");
        public static Country BurkinaFaso = new Country("Burkina Faso");
        public static Country Burundi = new Country("Burundi");
        public static Country Cambodia = new Country("Cambodia");
        public static Country Cameroon = new Country("Cameroon");
        public static Country Canada = new Country("Canada");
        public static Country CaymanIslands = new Country("Cayman Islands");
        public static Country CapeVerde = new Country("Cape Verde");
        public static Country CentralAfricanRepublic = new Country("Central African Republic");
        public static Country Chad = new Country("Chad");
        public static Country Chile = new Country("Chile");
        public static Country China = new Country("China");
        public static Country Colombia = new Country("Colombia");
        public static Country Comoros = new Country("Comoros");
        public static Country Congo = new Country("Congo");
        public static Country CookIslands = new Country("Cook Islands");
        public static Country CostaRica = new Country("Costa Rica");
        public static Country MarbleCoast = new Country("Côte d'Ivoire");
        public static Country Croatia = new Country("Croatia");
        public static Country Cuba = new Country("Cuba");
        public static Country Curaçao = new Country("Curaçao");
        public static Country Cyprus = new Country("Cyprus");
        public static Country CzechRepublic = new Country("Czech Republic");
        public static Country Denmark = new Country("Denmark");
        public static Country Djibouti = new Country("Djibouti");
        public static Country Dominica = new Country("Dominica");
        public static Country DominicanRepublic = new Country("Dominican Republic");
        public static Country Ecuador = new Country("Ecuador");
        public static Country Egypt = new Country("Egypt");
        public static Country ElSalvador = new Country("El Salvador");
        public static Country EquatorialGuinea = new Country("Equatorial Guinea");
        public static Country Eritrea = new Country("Eritrea");
        public static Country Estonia = new Country("Estonia");
        public static Country Ethiopia = new Country("Ethiopia");
        public static Country Malvinas = new Country("Falkland Islands (Malvinas)");
        public static Country Faroe = new Country("Faroe Islands");
        public static Country Fiji = new Country("Fiji");
        public static Country Finland = new Country("Finland");
        public static Country France = new Country("France");
        public static Country Gabon = new Country("Gabon");
        public static Country Gambia = new Country("Gambia");
        public static Country Georgia = new Country("Georgia");
        public static Country Germany = new Country("Germany");
        public static Country Ghana = new Country("Ghana");
        public static Country Gibraltar = new Country("Gibraltar");
        public static Country Greece = new Country("Greece");
        public static Country Greenland = new Country("Greenland");
        public static Country Grenada = new Country("Grenada");
        public static Country Guadeloupe = new Country("Guadeloupe");
        public static Country Guam = new Country("Guam");
        public static Country Guatemala = new Country("Guatemala");
        public static Country Guernsey = new Country("Guernsey");
        public static Country Guinea = new Country("Guinea");
        public static Country GuineaBissau = new Country("Guinea-Bissau");
        public static Country Guyana = new Country("Guyana");
        public static Country Haiti = new Country("Haiti");
        public static Country Honduras = new Country("Honduras");
        public static Country HongKong = new Country("Hong Kong");
        public static Country Hungary = new Country("Hungary");
        public static Country Iceland = new Country("Iceland");
        public static Country India = new Country("India");
        public static Country Indonesia = new Country("Indonesia");
        public static Country Iran = new Country("Iran");
        public static Country Iraq = new Country("Iraq");
        public static Country Ireland = new Country("Ireland");
        public static Country IsleofMan = new Country("Isle of Man");
        public static Country Israel = new Country("Israel");
        public static Country Italy = new Country("Italy");
        public static Country Jamaica = new Country("Jamaica");
        public static Country Japan = new Country("Japan");
        public static Country Jersey = new Country("Jersey");
        public static Country Jordan = new Country("Jordan");
        public static Country Kazakhstan = new Country("Kazakhstan");
        public static Country Kenya = new Country("Kenya");
        public static Country Kiribati = new Country("Kiribati");
        public static Country SouthKorea = new Country("South Korea");
        public static Country Kuwait = new Country("Kuwait");
        public static Country Kyrgyzstan = new Country("Kyrgyzstan");
        public static Country Lao = new Country("Lao");
        public static Country Latvia = new Country("Latvia");
        public static Country Lebanon = new Country("Lebanon");
        public static Country Lesotho = new Country("Lesotho");
        public static Country Liberia = new Country("Liberia");
        public static Country Libya = new Country("Libya");
        public static Country Liechtenstein = new Country("Liechtenstein");
        public static Country Lithuania = new Country("Lithuania");
        public static Country Luxembourg = new Country("Luxembourg");
        public static Country Macao = new Country("Macao");
        public static Country Macedonia = new Country("Macedonia");
        public static Country Madagascar = new Country("Madagascar");
        public static Country Malawi = new Country("Malawi");
        public static Country Malaysia = new Country("Malaysia");
        public static Country Maldives = new Country("Maldives");
        public static Country Mali = new Country("Mali");
        public static Country Malta = new Country("Malta");
        public static Country MarshallIslands = new Country("Marshall Islands");
        public static Country Martinique = new Country("Martinique");
        public static Country Mauritania = new Country("Mauritania");
        public static Country Mauritius = new Country("Mauritius");
        public static Country Mayotte = new Country("Mayotte");
        public static Country Mexico = new Country("Mexico");
        public static Country Micronesia = new Country("Micronesia");
        public static Country Moldova = new Country("Moldova");
        public static Country Monaco = new Country("Monaco");
        public static Country Mongolia = new Country("Mongolia");
        public static Country Montenegro = new Country("Montenegro");
        public static Country Montserrat = new Country("Montserrat");
        public static Country Morocco = new Country("Morocco");
        public static Country Mozambique = new Country("Mozambique");
        public static Country Myanmar = new Country("Myanmar");
        public static Country Namibia = new Country("Namibia");
        public static Country Nauru = new Country("Nauru");
        public static Country Nepal = new Country("Nepal");
        public static Country Netherlands = new Country("Netherlands");
        public static Country NewCaledonia = new Country("New Caledonia");
        public static Country NewZealand = new Country("New Zealand");
        public static Country Nicaragua = new Country("Nicaragua");
        public static Country Niger = new Country("Niger");
        public static Country Nigeria = new Country("Nigeria");
        public static Country Niue = new Country("Niue");
        public static Country NorfolkIsland = new Country("Norfolk Island");
        public static Country NorthernMarianaIslands = new Country("Northern Mariana Islands");
        public static Country Norway = new Country("Norway");
        public static Country Oman = new Country("Oman");
        public static Country Pakistan = new Country("Pakistan");
        public static Country Palau = new Country("Palau");
        public static Country Palestinian = new Country("Palestinian");
        public static Country Panama = new Country("Panama");
        public static Country PapuaNewGuinea = new Country("Papua New Guinea");
        public static Country Paraguay = new Country("Paraguay");
        public static Country Peru = new Country("Peru");
        public static Country Philippines = new Country("Philippines");
        public static Country Pitcairn = new Country("Pitcairn");
        public static Country Poland = new Country("Poland");
        public static Country Portugal = new Country("Portugal");
        public static Country PuertoRico = new Country("Puerto Rico");
        public static Country Qatar = new Country("Qatar");
        public static Country Réunion = new Country("Réunion");
        public static Country Romania = new Country("Romania");
        public static Country Russia = new Country("Russia");
        public static Country Rwanda = new Country("Rwanda");
        public static Country Samoa = new Country("Samoa");
        public static Country SanMarino = new Country("San Marino");
        public static Country SaoTomeAndPrincipe = new Country("Sao Tome and Principe");
        public static Country SaudiArabia = new Country("Saudi Arabia");
        public static Country Senegal = new Country("Senegal");
        public static Country Serbia = new Country("Serbia");
        public static Country Seychelles = new Country("Seychelles");
        public static Country SierraLeone = new Country("Sierra Leone");
        public static Country Singapore = new Country("Singapore");
        public static Country SintMaarten = new Country("Sint Maarten (Dutch part)");
        public static Country Slovakia = new Country("Slovakia");
        public static Country Slovenia = new Country("Slovenia");
        public static Country SolomonIslands = new Country("Solomon Islands");
        public static Country Somalia = new Country("Somalia");
        public static Country SouthAfrica = new Country("South Africa");
        public static Country SouthSudan = new Country("South Sudan");
        public static Country Spain = new Country("Spain");
        public static Country SriLanka = new Country("Sri Lanka");
        public static Country Sudan = new Country("Sudan");
        public static Country Suriname = new Country("Suriname");
        public static Country Swaziland = new Country("Swaziland");
        public static Country Sweden = new Country("Sweden");
        public static Country Switzerland = new Country("Switzerland");
        public static Country SyrianArabRepublic = new Country("Syrian Arab Republic");
        public static Country Taiwan = new Country("Taiwan");
        public static Country Tajikistan = new Country("Tajikistan");
        public static Country Tanzania = new Country("Tajikistan");
        public static Country Thailand = new Country("Tajikistan");
        public static Country TimorLeste = new Country("Timor-Leste");
        public static Country Togo = new Country("Togo");
        public static Country Tokelau = new Country("Tokelau");
        public static Country Tonga = new Country("Tonga");
        public static Country TrinidadTobago = new Country("Trinidad and Tobago");
        public static Country Tunisia = new Country("Tunisia");
        public static Country Turkey = new Country("Turkey");
        public static Country Turkmenistan = new Country("Turkmenistan");
        public static Country Tuvalu = new Country("Tuvalu");
        public static Country Uganda = new Country("Uganda");
        public static Country Ukraine = new Country("Ukraine");
        public static Country UnitedArabEmirates = new Country("United Arab Emirates");
        public static Country UnitedKingdom = new Country("United Kingdom");
        public static Country UnitedStates = new Country("United States");
        public static Country Uruguay = new Country("Uruguay");
        public static Country Uzbekistan = new Country("Uzbekistan");
        public static Country Vanuatu = new Country("Vanuatu");
        public static Country Venezuela = new Country("Venezuela");
        public static Country Vietnam = new Country("Vietnam");
        public static Country WesternSahara = new Country("Western Sahara");
        public static Country Yemen = new Country("Yemen");
        public static Country Zambia = new Country("Zambia");
        public static Country Zimbabwe = new Country("Zimbabwe");

        public static readonly List<Country> ListOfCountries = new List<Country>
        {
            Afghanistan,AlandIslands,Albania,Algeria,AmericanSamoa,Andorra,Angola,Anguilla,Antarctica,AntiguaAndBarbuda,Argentina,Armenia,Aruba,Australia,Austria,Azerbaijan,
            Bahamas,Bahrain,Bangladesh,Barbados,Belarus,Belgium,Belize,Benin,Bermuda,Bhutan,Bolivia,BosniaHerzegovina,Botswana,BouvetIsland,Brazil,BruneiDarussalam,Bulgaria,BurkinaFaso,Burundi,
            Cambodia,Cameroon,Canada,CaymanIslands,CapeVerde,CentralAfricanRepublic,Chad,Chile,China,Colombia,Comoros,Congo,CookIslands,CostaRica,MarbleCoast,Croatia,Cuba,Curaçao,Cyprus,CzechRepublic,
            Denmark,Djibouti,Dominica,DominicanRepublic,Ecuador,Egypt,ElSalvador,EquatorialGuinea,Eritrea,Estonia,Ethiopia,Malvinas,Faroe,Fiji,Finland,France,Gabon,Gambia,Georgia,Germany,Ghana,Gibraltar,
            Greece,Greenland,Grenada,Guadeloupe,Guam,Guatemala,Guernsey,Guinea,GuineaBissau,Guyana,Haiti,Honduras,HongKong,Hungary,Iceland,India,Indonesia,Iran,Iraq,Ireland,IsleofMan,Israel,Italy,Jamaica,
            Japan,Jersey,Jordan,Kazakhstan,Kenya,Kiribati,SouthKorea,Kuwait,Kyrgyzstan,Lao,Latvia,Lebanon,Lesotho,Liberia,Libya,Liechtenstein,Lithuania,Luxembourg,Macao,Macedonia,Madagascar,Malawi,Malaysia,
            Maldives,Mali,Malta,MarshallIslands,Martinique,Mauritania,Mauritius,Mayotte,Mexico,Micronesia,Moldova,Monaco,Mongolia,Montenegro,Montserrat,Morocco,Mozambique,Myanmar,Namibia,Nauru,Nepal,Netherlands,
            NewCaledonia,NewZealand,Nicaragua,Niger,Nigeria,Niue,NorfolkIsland,NorthernMarianaIslands,Norway,Oman,Pakistan,Palau,Palestinian,Panama,PapuaNewGuinea,Paraguay,Peru,Philippines,Pitcairn,Poland,Portugal,
            PuertoRico,Qatar,Réunion,Romania,Russia,Rwanda,Samoa,SanMarino,SaoTomeAndPrincipe,SaudiArabia,Senegal,Serbia,Seychelles,SierraLeone,Singapore,SintMaarten,Slovakia,Slovenia,SolomonIslands,Somalia,SouthAfrica,
            SouthAfrica,SouthSudan,Spain,SriLanka,Sudan,Suriname,Swaziland,Sweden,Switzerland,SyrianArabRepublic,Taiwan,Tajikistan,Tanzania,Thailand,TimorLeste,Togo,Tokelau,Tonga,TrinidadTobago,Tunisia,Turkey,Turkmenistan,
            Tuvalu,Uganda,Ukraine,UnitedArabEmirates,UnitedKingdom,UnitedStates,Uruguay,Uzbekistan,Vanuatu,Venezuela,Vietnam,WesternSahara,Yemen,Zambia,Zimbabwe
        };

        public Country(string val)
        {
            Value = val;
        }

        public string Value { get; set; }
    }

    public sealed class Language
    {
        public static readonly Language Afrikaans = new Language("Afrikaans");
        public static readonly Language Albanian = new Language("Albanian");
        public static readonly Language Amharic = new Language("Amharic");
        public static readonly Language Arabic = new Language("Arabic");
        public static readonly Language Armenian = new Language("Armenian");
        public static readonly Language Azerbaijani = new Language("Azerbaijani");
        public static readonly Language Basque = new Language("Basque");
        public static readonly Language Belarusian = new Language("Belarusian");
        public static readonly Language Bengali = new Language("Bengali");
        public static readonly Language Bosnian = new Language("Bosnian");
        public static readonly Language Bulgarian = new Language("Bulgarian");
        public static readonly Language Catalan = new Language("Catalan");
        public static readonly Language Cebuano = new Language("Cebuano");
        public static readonly Language Chichewa = new Language("Chichewa");
        public static readonly Language Chinese = new Language("Chinese");
        public static readonly Language Corsican = new Language("Corsican");
        public static readonly Language Croatian = new Language("Croatian");
        public static readonly Language Czech = new Language("Czech");
        public static readonly Language Danish = new Language("Danish");
        public static readonly Language Dutch = new Language("Dutch");
        public static readonly Language English = new Language("English");
        public static readonly Language Esperanto = new Language("Esperanto");
        public static readonly Language Estonian = new Language("Estonian");
        public static readonly Language Filipino = new Language("Filipino");
        public static readonly Language Finnish = new Language("Finnish");
        public static readonly Language French = new Language("French");
        public static readonly Language Frisian = new Language("Frisian");
        public static readonly Language Galician = new Language("Galician");
        public static readonly Language German = new Language("German");
        public static readonly Language Greek = new Language("Greek");
        public static readonly Language Gujarati = new Language("Gujarati");
        public static readonly Language HaitianCreole = new Language("Haitian Creole");
        public static readonly Language Hausa = new Language("Hausa");
        public static readonly Language Hawaiian = new Language("Hawaiian");
        public static readonly Language Hebrew = new Language("Hebrew");
        public static readonly Language Hindi = new Language("Hindi");
        public static readonly Language Hmong = new Language("Hmong");
        public static readonly Language Hungarian = new Language("Hungarian");
        public static readonly Language Icelandic = new Language("Icelandic");
        public static readonly Language Igbo = new Language("Igbo");
        public static readonly Language Indonesian = new Language("Indonesian");
        public static readonly Language Irish = new Language("Irish");
        public static readonly Language Italian = new Language("Italian");
        public static readonly Language Japanese = new Language("Japanese");
        public static readonly Language Javanese = new Language("Javanese");
        public static readonly Language Kannada = new Language("Kannada");
        public static readonly Language Kazakh = new Language("Kazakh");
        public static readonly Language Khmer = new Language("Khmer");
        public static readonly Language Korean = new Language("Korean");
        public static readonly Language Kurdish = new Language("Kurdish (Kurmanji)");
        public static readonly Language Kyrgyz = new Language("Kyrgyz");
        public static readonly Language Lao = new Language("Lao");
        public static readonly Language Latin = new Language("Latin");
        public static readonly Language Latvian = new Language("Latvian");
        public static readonly Language Lithuanian = new Language("Lithuanian");
        public static readonly Language Luxembourgish = new Language("Luxembourgish");
        public static readonly Language Macedonian = new Language("Macedonian");
        public static readonly Language Malagasy = new Language("Malagasy");
        public static readonly Language Malay = new Language("Malay");
        public static readonly Language Malayalam = new Language("Malayalam");
        public static readonly Language Maltese = new Language("Maltese");
        public static readonly Language Maori = new Language("Maori");
        public static readonly Language Marathi = new Language("Marathi");
        public static readonly Language Mongolian = new Language("Mongolian");
        public static readonly Language Myanmar = new Language("Myanmar (Burmese)");
        public static readonly Language Nepali = new Language("Nepali");
        public static readonly Language Norwegian = new Language("Norwegian");
        public static readonly Language Pashto = new Language("Pashto");
        public static readonly Language Persian = new Language("Persian");
        public static readonly Language Polish = new Language("Polish");
        public static readonly Language Portuguese = new Language("Portuguese");
        public static readonly Language Punjabi = new Language("Punjabi");
        public static readonly Language Romanian = new Language("Romanian");
        public static readonly Language Russian = new Language("Russian");
        public static readonly Language Samoan = new Language("Samoan");
        public static readonly Language Gaelic = new Language("Scots Gaelic");
        public static readonly Language Serbian = new Language("Serbian");
        public static readonly Language Sesotho = new Language("Sesotho");
        public static readonly Language Shona = new Language("Shona");
        public static readonly Language Sindhi = new Language("Sindhi");
        public static readonly Language Sinhala = new Language("Sinhala");
        public static readonly Language Slovak = new Language("Slovak");
        public static readonly Language Slovenian = new Language("Slovenian");
        public static readonly Language Somali = new Language("Somali");
        public static readonly Language Spanish = new Language("Spanish");
        public static readonly Language Sundanese = new Language("Sundanese");
        public static readonly Language Swahili = new Language("Swahili");
        public static readonly Language Swedish = new Language("Swedish");
        public static readonly Language Tajik = new Language("Tajik");
        public static readonly Language Tamil = new Language("Tamil");
        public static readonly Language Telugu = new Language("Telugu");
        public static readonly Language Thai = new Language("Thai");
        public static readonly Language Turkish = new Language("Turkish");
        public static readonly Language Ukrainian = new Language("Ukrainian");
        public static readonly Language Urdu = new Language("Urdu");
        public static readonly Language Uzbek = new Language("Uzbek");
        public static readonly Language Vietnamese = new Language("Vietnamese");
        public static readonly Language Welsh = new Language("Welsh");
        public static readonly Language Xhosa = new Language("Xhosa");
        public static readonly Language Yiddish = new Language("Yiddish");
        public static readonly Language Yoruba = new Language("Yoruba");
        public static readonly Language Zulu = new Language("Zulu");
        public static readonly List<Language> ListOfLanguages = new List<Language>
        {
            Afrikaans,Albanian,Amharic,Arabic,Armenian,Azerbaijani,Basque,Belarusian,Bengali,Bosnian,Bulgarian,Catalan,Cebuano,Chichewa,Chinese,Corsican,Croatian,Czech,Danish,Dutch,English,Esperanto,Estonian,Filipino,Finnish,French,
            Frisian,Galician,German,Greek,Gujarati,HaitianCreole,Hausa,Hawaiian,Hebrew,Hindi,Hmong,Hungarian,Icelandic,Igbo,Indonesian,Irish,Italian,Japanese,Kannada,Kazakh,Khmer,Korean,Kurdish,Kyrgyz,Lao,Latin,Latvian,Lithuanian,Luxembourgish,
            Macedonian,Malagasy,Malay,Malayalam,Maltese,Maori,Marathi,Mongolian,Myanmar,Nepali,Norwegian,Pashto,Persian,Polish,Portuguese,Punjabi,Romanian,Russian,Samoan,Gaelic,Serbian,Sesotho,Shona,Sindhi,Sinhala,Slovak,Slovenian,Somali,Spanish,
            Sundanese,Swahili,Swedish,Tajik,Tamil,Telugu,Thai,Turkish,Ukrainian,Urdu,Uzbek,Vietnamese,Welsh,Xhosa,Yiddish,Yoruba,Zulu
        };

        public Language(string val)
        {
            Value = val;
        }

        public string Value { get; set; }
    }

    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }
        public string Rank { get; set; }
        [ForeignKey("UserTeamRefId")]
        public int PertainingTeam { get; set; }
        public UserPositionInTeam PositionOnTeam { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public string PlayRegion { get; set; }
        public string Level { get; set; }
        string userAvatar = "images/add_friend_article_banner.jpg";
        public string UserAvatar { get { return this.userAvatar; } set { this.userAvatar = value; } }
    }
}
